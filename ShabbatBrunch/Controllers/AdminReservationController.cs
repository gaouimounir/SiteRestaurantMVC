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
    public class AdminReservationController : Controller
    {
        private readonly ShabbatBrunchContext _context;

        public AdminReservationController(ShabbatBrunchContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Affiche la liste des réservations.
        /// </summary>
        /// <returns>Vue avec la liste des réservations.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservations.ToListAsync());
        }

        /// <summary>
        /// Récupère les détails d'une réservation spécifique en fonction de l'ID.
        /// </summary>
        /// <param name="id">ID de la réservation.</param>
        /// <returns>Vue avec les détails de la réservation.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        /// <summary>
        /// Affiche le formulaire de création d'une nouvelle réservation.
        /// </summary>
        /// <returns>Vue de création.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Traite la soumission du formulaire de création après la validation côté serveur.
        /// </summary>
        /// <param name="reservation">Objet Reservation avec les données du formulaire.</param>
        /// <returns>Vue de création ou redirection vers l'index en cas de succès.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Telephone,NbCouvert,Date,Heure,Commentaire")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        /// <summary>
        /// Affiche le formulaire d'édition pour une réservation spécifique en fonction de l'ID.
        /// </summary>
        /// <param name="id">ID de la réservation.</param>
        /// <returns>Vue d'édition avec les détails de la réservation.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        /// <summary>
        /// Traite la soumission du formulaire d'édition après la validation côté serveur.
        /// </summary>
        /// <param name="id">ID de la réservation.</param>
        /// <param name="reservation">Objet Reservation avec les données du formulaire.</param>
        /// <returns>Vue d'édition ou redirection vers l'index en cas de succès.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Telephone,NbCouvert,Date,Heure,Commentaire")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            return View(reservation);
        }

        /// <summary>
        /// Affiche la page de confirmation de suppression pour une réservation spécifique en fonction de l'ID.
        /// </summary>
        /// <param name="id">ID de la réservation.</param>
        /// <returns>Vue de confirmation de suppression.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        /// <summary>
        /// Supprime de manière définitive une réservation après confirmation.
        /// </summary>
        /// <param name="id">ID de la réservation.</param>
        /// <returns>Redirection vers l'index après la suppression.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Vérifie l'existence d'une réservation en fonction de l'ID.
        /// </summary>
        /// <param name="id">ID de la réservation.</param>
        /// <returns>Booléen indiquant si la réservation existe.</returns>
        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
