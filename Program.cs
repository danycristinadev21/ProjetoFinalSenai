using Microsoft.EntityFrameworkCore;
using PapelArt.Models;
using PapelArt.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Usar o provedor MySQL
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString), // Detecta a versão do seu MySQL
        mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
    );
});

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PapelArtContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))));
    

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

