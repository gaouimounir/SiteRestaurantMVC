using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShabbatBrunch.Data;
using ShabbatBrunch.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShabbatBrunchContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ShabbatBrunchContext")
                      ?? throw new InvalidOperationException("Connection string 'ShabbatBrunchContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ShabbatBrunchContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ShabbatBrunchContext>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    SeedData.Initialize(dbContext, userManager);  // Pass both the ShabbatBrunchContext and UserManager<IdentityUser>
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();