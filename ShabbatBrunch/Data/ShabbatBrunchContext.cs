using Microsoft.EntityFrameworkCore;
using ShabbatBrunch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ShabbatBrunch.Data
{
    public class ShabbatBrunchContext : IdentityDbContext<IdentityUser>
    {
        public ShabbatBrunchContext(DbContextOptions<ShabbatBrunchContext> options) : base(options)
        {
        }
        public DbSet<Reservation> Reservations { get; set; } = default!;
        public DbSet<CarteItem> CarteItems { get; set; } = default!;
        public DbSet<NewsletterModel> Newsletter { get; set; }
        public DbSet<Carte> Carte { get; set; }
        public DbSet<Avis> Avis { get; set; }
    }
}