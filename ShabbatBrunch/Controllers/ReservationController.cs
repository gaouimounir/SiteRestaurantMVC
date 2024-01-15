using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShabbatBrunch.Data;
using ShabbatBrunch.Models;

namespace ShabbatBrunch.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly ShabbatBrunchContext _context;

        public ReservationController(ILogger<ReservationController> logger, ShabbatBrunchContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Affiche la page des réservations.
        /// </summary>
        /// <returns>Vue de la page des réservations.</returns>
        public async Task<IActionResult> Index()
        {
            return View();
        }

        /// <summary>
        /// Crée une nouvelle réservation.
        /// </summary>
        /// <param name="reservation">Objet de réservation à créer.</param>
        /// <returns>Redirige vers la page des réservations en cas de succès, sinon reste sur la page actuelle.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Telephone,NbCouvert,Date,Heure,Commentaire")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                if (!IsDateReservationOK(reservation)) return View(nameof(Index));

                _context.Add(reservation);
                await _context.SaveChangesAsync();
                TempData["SuccessReservationMessage"] = "Réservation bien créée!";
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index));
        }

        /// <summary>
        /// Vérifie si la date et l'heure de la réservation sont valides.
        /// </summary>
        /// <param name="reservation">Objet de réservation à vérifier.</param>
        /// <returns>True si la date et l'heure sont valides, sinon False.</returns>
        private bool IsDateReservationOK(Reservation reservation)
        {
            if (reservation.Date == DateOnly.FromDateTime(DateTime.Now) && reservation.Heure < TimeOnly.FromDateTime(DateTime.Now))
            {
                ModelState.AddModelError(nameof(reservation.Heure), "Impossible de réserver à une heure antérieure dans la journée");
                return false;
            }

            if (reservation.Date < DateOnly.FromDateTime(DateTime.Now))
            {
                ModelState.AddModelError(nameof(reservation.Date), "Impossible de réserver à une date antérieure");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gère les erreurs.
        /// </summary>
        /// <returns>Vue de la page d'erreur.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
