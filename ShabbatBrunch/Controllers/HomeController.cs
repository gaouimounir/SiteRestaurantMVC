using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShabbatBrunch.Models;

namespace ShabbatBrunch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Affiche la page d'accueil.
        /// </summary>
        /// <returns>Vue de la page d'accueil.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Affiche la page d'erreur.
        /// </summary>
        /// <returns>Vue de la page d'erreur avec les détails de l'erreur.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}