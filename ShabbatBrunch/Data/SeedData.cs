using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShabbatBrunch.Data;
using ShabbatBrunch.Models;
using System;
using System.Linq;
using ShabbatBrunch.Enums;

namespace ShabbatBrunch.Data
{
    public static class SeedData
    {
        public static void Initialize(ShabbatBrunchContext context, UserManager<IdentityUser> userManager)
        {
            context.Database.EnsureCreated();
            if (context.Carte.Any() || context.CarteItems.Any() || context.Reservations.Any() || context.Newsletter.Any())
            {
                return; // Database has already been seeded
            }

            // Categories
            var categories = new Carte[]
            {
                new Carte { Categorie = Categorie.Sucré},
                new Carte { Categorie = Categorie.Sucré },
                new Carte { Categorie = Categorie.Boissons },
                new Carte { Categorie = Categorie.Extras}
            };

            foreach (var category in categories)
            {
                context.Carte.Add(category);
            }

            context.SaveChanges();

            // Items
          var items = new CarteItem[]
{
    // Articles pour la catégorie "Sucre"
    new CarteItem
    {
        Nom = "Baklava",
        Description = "Pâtisserie aux noix et au miel",
        Prix = "4.99",
        Categorie = Categorie.Sucré,
        CreatedDate = DateTime.Now,
        Image = "baklava.jpg",
        Allergenes = Allergenes.Gluten | Allergenes.FruitsACoques
    },
    new CarteItem
    {
        Nom = "Mhalbi",
        Description = "Dessert au riz et à l'eau de fleur d'oranger",
        Prix = "5.99",
        Categorie = Categorie.Sucré,
        CreatedDate = DateTime.Now,
        Image = "mhalbi.jpg",
        Allergenes = Allergenes.Aucun // Exemple avec aucun allergène
    },
    new CarteItem
    {
        Nom = "Namoura",
        Description = "Gâteau de semoule au yaourt",
        Prix = "3.99",
        Categorie = Categorie.Sucré,
        CreatedDate = DateTime.Now,
        Image = "Namoura.jpg",
        Allergenes = Allergenes.Gluten | Allergenes.Lait
    },
    new CarteItem
    {
        Nom = "Atayef",
        Description = "Petits pancakes farcis",
        Prix = "6.99",
        Categorie = Categorie.Sucré,
        CreatedDate = DateTime.Now,
        Image = "Atayef.jpg",
        Allergenes = Allergenes.Aucun // Exemple avec aucun allergène
    },

    // Articles pour la catégorie "Sale"
    new CarteItem
    {
        Nom = "Shawarma",
        Description = "Viande marinée grillée",
        Prix = "9.99",
        Categorie = Categorie.Salé,
        CreatedDate = DateTime.Now,
        Image = "carteBrunch.jpg",
        Allergenes = Allergenes.Aucun // Exemple avec aucun allergène
    },
    new CarteItem
    {
        Nom = "Kebab",
        Description = "Brochettes de viande grillée",
        Prix = "8.99",
        Categorie = Categorie.Salé,
        CreatedDate = DateTime.Now,
        Image = "carteBrunch.jpg",
        Allergenes = Allergenes.Aucun // Exemple avec aucun allergène
    },
    new CarteItem
    {
        Nom = "Couscous",
        Description = "Semoule de blé avec légumes et viande",
        Prix = "10.99",
        Categorie = Categorie.Salé,
        CreatedDate = DateTime.Now,
        Image = "carteBrunch.jpg",
        Allergenes = Allergenes.Gluten
    },
    new CarteItem
    {
        Nom = "Merguez",
        Description = "Saucisses épicées",
        Prix = "7.99",
        Categorie = Categorie.Salé,
        CreatedDate = DateTime.Now,
        Image = "carteBrunch.jpg",
        Allergenes = Allergenes.Aucun // Exemple avec aucun allergène
    },

    // Articles pour la catégorie "Boissons"
    new CarteItem
    {
        Nom = "Thé à la menthe",
        Description = "Thé vert à la menthe",
        Prix = "2.99",
        Categorie = Categorie.Boissons,
        CreatedDate = DateTime.Now,
        Image = "carteBrunch.jpg",
        Allergenes = Allergenes.Aucun // Exemple avec aucun allergène
    },
    new CarteItem
    {
        Nom = "Jus d'orange frais",
        Description = "Jus d'orange pressé à la main",
        Prix = "3.99",
        Categorie = Categorie.Boissons,
        CreatedDate = DateTime.Now,
        Image = "carteBrunch.jpg",
        Allergenes = Allergenes.Aucun // Exemple avec aucun allergène
    },
    new CarteItem
    {
        Nom = "Limonade maison",
        Description = "Limonade fraîche faite maison",
        Prix = "4.99",
        Categorie = Categorie.Boissons,
        CreatedDate = DateTime.Now,
        Image = "carteBrunch.jpg",
        Allergenes = Allergenes.Aucun // Exemple avec aucun allergène
    },
    new CarteItem
    {
        Nom = "Café arabe",
        Description = "Café fort et aromatique",
        Prix = "2.49",
        Categorie = Categorie.Boissons,
        CreatedDate = DateTime.Now,
        Image = "carteBrunch.jpg",
        Allergenes = Allergenes.Aucun // Exemple avec aucun allergène
    },

    // Articles pour la catégorie "Extras"
    new CarteItem
    {
        Nom = "Houmous",
        Description = "Purée de pois chiches",
        Prix = "5.99",
        Categorie = Categorie.Extras,
        CreatedDate = DateTime.Now,
        Image = "carteBrunch.jpg",
        Allergenes = Allergenes.Soja // Exemple avec allergène "Soja"
    },
    new CarteItem
    {
        Nom = "Falafel",
        Description = "Boulettes de pois chiches",
        Prix = "6.99",
        Categorie = Categorie.Extras,
        CreatedDate = DateTime.Now,
        Image = "carteBrunch.jpg",
        Allergenes = Allergenes.Aucun // Exemple avec aucun allergène
    },
    new CarteItem
    {
        Nom = "Taboulé",
        Description = "Salade de persil et de tomates",
        Prix = "7.99",
        Categorie = Categorie.Extras,
        CreatedDate = DateTime.Now,
        Image = "carteBrunch.jpg",
        Allergenes = Allergenes.Moutarde // Exemple avec allergène "Moutarde"
    },
    new CarteItem
    {
        Nom = "Fattoush",
        Description = "Salade méditerranéenne",
        Prix = "8.99",
        Categorie = Categorie.Extras,
        CreatedDate = DateTime.Now,
        Image = "carteBrunch.jpg",
        Allergenes = Allergenes.Aucun // Exemple avec aucun allergène
    },
};

        
        // Reste du code...
    


            foreach (var item in items)
            {
                context.CarteItems.Add(item);
            }

           
            // Contacts
           

            // User
            var user = new IdentityUser
            {
                UserName = "admin@example.com",
                NormalizedUserName = "admin@example.com",
                Email = "admin@example.com",
                NormalizedEmail = "admin@example.com",
                EmailConfirmed = true,
            };
            var user2 = new IdentityUser
            {
                UserName = "admino@example.com",
                NormalizedUserName = "admino@example.com",
                Email = "admino@example.com",
                NormalizedEmail = "admino@example.com",
                EmailConfirmed = true,
            };
            var user3 = new IdentityUser
            {
                UserName = "admina@example.com",
                NormalizedUserName = "admina@example.com",
                Email = "admina@example.com",
                NormalizedEmail = "admina@example.com",
                EmailConfirmed = true,
            };
            
            // Set your desired password
            userManager.CreateAsync(user, "Admin@123").Wait(); 
            userManager.CreateAsync(user2, "Lille@123").Wait();
            userManager.CreateAsync(user3, "Latum@123").Wait();
            // Newsletter
            var newsletters = new NewsletterModel[]
            {
                new NewsletterModel { Mail = "newsletter1@example.com" },
                new NewsletterModel { Mail = "newsletter2@example.com" },
                new NewsletterModel { Mail = "newsletter3@example.com" },
                // Ajoutez autant d'éléments que vous le souhaitez
            };

            foreach (var newsletter in newsletters)
            {
                context.Newsletter.Add(newsletter);
            }
            context.SaveChanges();
        }
    }
}