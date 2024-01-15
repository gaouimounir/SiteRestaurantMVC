using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShabbatBrunch.Data;
using ShabbatBrunch.Models;

namespace ShabbatBrunch
{
    [Authorize]
    public class AdminNewsletterController : Controller
    {
        private readonly ShabbatBrunchContext _context;

        public AdminNewsletterController(ShabbatBrunchContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Affiche la liste des abonnés à la newsletter.
        /// </summary>
        /// <returns>Vue avec la liste des abonnés.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Newsletter.ToListAsync());
        }

        /// <summary>
        /// Récupère les détails d'un abonné spécifique en fonction de l'ID.
        /// </summary>
        /// <param name="id">ID de l'abonné.</param>
        /// <returns>Vue avec les détails de l'abonné.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletterModel = await _context.Newsletter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsletterModel == null)
            {
                return NotFound();
            }

            return View(newsletterModel);
        }

        /// <summary>
        /// Affiche le formulaire de création d'un nouvel abonné à la newsletter.
        /// </summary>
        /// <returns>Vue de création.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Traite la soumission du formulaire de création après la validation côté serveur.
        /// </summary>
        /// <param name="newsletterModel">Objet NewsletterModel avec les données du formulaire.</param>
        /// <returns>Vue de création ou redirection vers l'index en cas de succès.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Mail")] NewsletterModel newsletterModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsletterModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsletterModel);
        }

        /// <summary>
        /// Affiche le formulaire d'édition pour un abonné spécifique en fonction de l'ID.
        /// </summary>
        /// <param name="id">ID de l'abonné.</param>
        /// <returns>Vue d'édition avec les détails de l'abonné.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletterModel = await _context.Newsletter.FindAsync(id);
            if (newsletterModel == null)
            {
                return NotFound();
            }
            return View(newsletterModel);
        }

        /// <summary>
        /// Traite la soumission du formulaire d'édition après la validation côté serveur.
        /// </summary>
        /// <param name="id">ID de l'abonné.</param>
        /// <param name="newsletterModel">Objet NewsletterModel avec les données du formulaire.</param>
        /// <returns>Vue d'édition ou redirection vers l'index en cas de succès.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mail")] NewsletterModel newsletterModel)
        {
            if (id != newsletterModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsletterModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsletterModelExists(newsletterModel.Id))
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
            return View(newsletterModel);
        }

        /// <summary>
        /// Affiche la page de confirmation de suppression pour un abonné spécifique en fonction de l'ID.
        /// </summary>
        /// <param name="id">ID de l'abonné.</param>
        /// <returns>Vue de confirmation de suppression.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletterModel = await _context.Newsletter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsletterModel == null)
            {
                return NotFound();
            }

            return View(newsletterModel);
        }

        /// <summary>
        /// Supprime de manière définitive un abonné à la newsletter après confirmation.
        /// </summary>
        /// <param name="id">ID de l'abonné.</param>
        /// <returns>Redirection vers l'index après la suppression.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsletterModel = await _context.Newsletter.FindAsync(id);
            if (newsletterModel != null)
            {
                _context.Newsletter.Remove(newsletterModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Vérifie l'existence d'un abonné à la newsletter en fonction de l'ID.
        /// </summary>
        /// <param name="id">ID de l'abonné.</param>
        /// <returns>Booléen indiquant si l'abonné existe.</returns>
        private bool NewsletterModelExists(int id)
        {
            return _context.Newsletter.Any(e => e.Id == id);
        }
    }
}
