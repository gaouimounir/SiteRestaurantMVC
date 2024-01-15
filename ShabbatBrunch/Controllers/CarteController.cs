using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShabbatBrunch.Data;
using ShabbatBrunch.Models;
using ShabbatBrunch.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShabbatBrunch.Controllers
{
    public class CarteController : Controller
    {
        private readonly ShabbatBrunchContext _context;

        public CarteController(ShabbatBrunchContext context)
        {
            _context = context;

            // Initialise les catégories dans ViewData lors de la création de l'instance du contrôleur.
            SetCategoriesInViewData();
        }

        /// <summary>
        /// Affiche la page d'index des articles de la carte avec la possibilité de filtrer par catégorie.
        /// </summary>
        /// <param name="categorie">Catégorie à filtrer.</param>
        /// <returns>Vue de la page d'index avec la liste d'articles filtrée par catégorie.</returns>
        public async Task<IActionResult> Index(string categorie)
        {
            // Initialise une requête IQueryable pour les articles de la carte.
            IQueryable<CarteItem> query = _context.CarteItems.AsQueryable();

            // Filtrer par catégorie si une catégorie est spécifiée.
            if (!string.IsNullOrEmpty(categorie))
            {
                // Utilisez la valeur enum directement pour la comparaison.
                var selectedCategorie = Enum.Parse<Categorie>(categorie);
                query = query.Where(ci => ci.Categorie == selectedCategorie);
            }

            // Récupère la liste des articles après le filtrage.
            var articles = await query.ToListAsync();

            // Passe la catégorie sélectionnée à la vue pour l'affichage.
            ViewData["SelectedCategorie"] = categorie;

            return View(articles);
        }

        /// <summary>
        /// Initialise les catégories dans ViewData pour être utilisées dans les vues.
        /// </summary>
        private void SetCategoriesInViewData()
        {
            ViewData["Categories"] = Enum.GetValues(typeof(Categorie))
                .Cast<Categorie>()
                .Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = c.ToString()
                })
                .ToList();
        }

        /// <summary>
        /// Affiche les détails d'un article de la carte.
        /// </summary>
        /// <param name="id">Identifiant de l'article.</param>
        /// <returns>Vue des détails de l'article.</returns>
        public IActionResult Details(int id)
        {
            // Recherche l'article par son identifiant.
            var article = _context.CarteItems.FirstOrDefault(ci => ci.Id == id);

            // Retourne une vue NotFound si l'article n'est pas trouvé.
            if (article == null)
            {
                return NotFound();
            }

            // Affiche la vue des détails de l'article.
            return View(article);
        }
    }
}
