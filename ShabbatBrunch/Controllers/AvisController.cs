using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShabbatBrunch.Data;
using ShabbatBrunch.Models;

namespace ShabbatBrunch.Controllers
{
    public class AvisController : Controller
    {
        private readonly ILogger<AvisController> _logger;
        private readonly ShabbatBrunchContext _context;

        public AvisController(ILogger<AvisController> logger, ShabbatBrunchContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Affiche la page d'index des avis.
        /// </summary>
        /// <returns>Vue de la page d'index des avis.</returns>
        public async Task<IActionResult> Index()
        {
            return View();
        }

        /// <summary>
        /// Traite la soumission du formulaire de création d'un avis après la validation côté serveur.
        /// </summary>
        /// <param name="avis">Objet Avis avec les données du formulaire.</param>
        /// <returns>Vue de la page d'index des avis ou redirection vers l'index en cas de succès.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Auteur,Contenu,Note,DateAvis")] Avis avis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avis);
                avis.DateAvis = DateTime.Now; 
                await _context.SaveChangesAsync();
                TempData["SuccessAvisMessage"] = "Avis bien créé!";
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index));
        }

        /// <summary>
        /// Gère les erreurs et affiche la page d'erreur.
        /// </summary>
        /// <returns>Vue de la page d'erreur.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
