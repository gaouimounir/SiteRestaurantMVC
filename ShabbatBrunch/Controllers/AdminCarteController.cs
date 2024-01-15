using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShabbatBrunch.Data;
using ShabbatBrunch.Enums;
using ShabbatBrunch.Models;

namespace ShabbatBrunch
{
    [Authorize]
    public class AdminCarteController : Controller
    {
        private readonly ShabbatBrunchContext _context;

        public AdminCarteController(ShabbatBrunchContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Affiche la liste des éléments de la carte.
        /// </summary>
        /// <returns>Vue avec la liste des éléments de la carte.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarteItems.AsNoTracking().ToListAsync());
        }

        /// <summary>
        /// Récupère les détails d'un élément spécifique de la carte en fonction de l'ID.
        /// </summary>
        /// <param name="id">ID de l'élément.</param>
        /// <returns>Vue avec les détails de l'élément de la carte.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carteItem = await _context.CarteItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carteItem == null)
            {
                return NotFound();
            }

            return View(carteItem);
        }

        /// <summary>
        /// Affiche le formulaire de création d'un nouvel élément de la carte et traite la soumission du formulaire, y compris la validation de l'image.
        /// </summary>
        /// <param name="carteItem">Objet CarteItem pour les données du formulaire.</param>
        /// <param name="image">IFormFile pour l'image.</param>
        /// <returns>Vue de création ou redirection vers l'index en cas de succès.</returns>
        public async Task<IActionResult> Create(CarteItem carteItem, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                // Validation de l'image
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(image.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("Image", "Seules les images avec les extensions .jpg, .jpeg, .png et .gif sont autorisées.");
                    // Gérer l'erreur ici
                    return View();
                }

                // Chemin de stockage de l'image
                var fileName = Path.GetFileName(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\carte", fileName);

                // Copie asynchrone du fichier
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                carteItem.Image = fileName;
            }

            // Préparez la liste des catégories
            var categorieList = Enum.GetValues(typeof(Categorie))
                .Cast<Categorie>()
                .Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() })
                .ToList();

            ViewData["Categories"] = new SelectList(categorieList, "Value", "Text");

            // Logique supplémentaire pour sauvegarder la carteItem dans la base de données, par exemple.

            return View();
        }

        /// <summary>
        /// Traite la soumission du formulaire de création après la validation côté serveur.
        /// </summary>
        /// <param name="carteItem">Objet CarteItem avec les données du formulaire.</param>
        /// <returns>Vue de création ou redirection vers l'index en cas de succès.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Nom,Description,Image,Prix,Categorie,CreatedDate,ModifiedDate")] CarteItem carteItem)
        {
            if (ModelState.IsValid)
            {
                // Ajouter la date de création
                carteItem.CreatedDate = DateTime.Now;

                _context.Add(carteItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(carteItem);
        }

        /// <summary>
        /// Affiche le formulaire d'édition pour un élément spécifique de la carte en fonction de l'ID.
        /// </summary>
        /// <param name="id">ID de l'élément.</param>
        /// <returns>Vue d'édition avec les détails de l'élément.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            // Préparez la liste des catégories
            var categorieList = Enum.GetValues(typeof(Categorie))
                .Cast<Categorie>()
                .Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() })
                .ToList();

            ViewData["Categories"] = new SelectList(categorieList, "Value", "Text");

            if (id == null)
            {
                return NotFound();
            }

            var carteItem = await _context.CarteItems.FindAsync(id);
            if (carteItem == null)
            {
                return NotFound();
            }

            return View(carteItem);
        }

        /// <summary>
        /// Traite la soumission du formulaire d'édition après la validation côté serveur.
        /// </summary>
        /// <param name="id">ID de l'élément.</param>
        /// <param name="carteItem">Objet CarteItem avec les données du formulaire.</param>
        /// <returns>Vue d'édition ou redirection vers l'index en cas de succès.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Nom,Description,Image,Prix,Categorie,CreatedDate,ModifiedDate,Allergenes")] CarteItem carteItem)
        {
            if (id != carteItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Mettre à jour la date de modification
                    carteItem.ModifiedDate = DateTime.Now;

                    _context.Update(carteItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarteItemExists(carteItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(carteItem);
        }

        /// <summary>
        /// Affiche la page de confirmation de suppression pour un élément spécifique de la carte en fonction de l'ID.
        /// </summary>
        /// <param name="id">ID de l'élément.</param>
        /// <returns>Vue de confirmation de suppression.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carteItem = await _context.CarteItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carteItem == null)
            {
                return NotFound();
            }

            return View(carteItem);
        }

        /// <summary>
        /// Supprime de manière définitive un élément de la carte après confirmation.
        /// </summary>
        /// <param name="id">ID de l'élément.</param>
        /// <returns>Redirection vers l'index après la suppression.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carteItem = await _context.CarteItems.FindAsync(id);
            if (carteItem != null)
            {
                _context.CarteItems.Remove(carteItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Vérifie l'existence d'un élément de la carte en fonction de l'ID.
        /// </summary>
        /// <param name="id">ID de l'élément.</param>
        /// <returns>Booléen indiquant si l'élément existe.</returns>
        private bool CarteItemExists(int id)
        {
            return _context.CarteItems.Any(e => e.Id == id);
        }
    }
}
