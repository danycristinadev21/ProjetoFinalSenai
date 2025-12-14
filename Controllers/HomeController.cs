using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PapelArt.Models;
using Microsoft.AspNetCore.Authorization;

namespace PapelArt.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Tela inicial com logo (liberada)
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();   // Views/Home/Index.cshtml
    }

    // Menu - só acessa depois do login
    public IActionResult Menu()
    {
        return View();   // Views/Home/Menu.cshtml
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
