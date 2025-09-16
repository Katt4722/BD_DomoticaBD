using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BD_DomoticaBD.MVC.Models;
using Biblioteca.Persistencia.Dapper;
using Biblioteca;
using MySqlConnector;
using System.Data;

namespace BD_DomoticaBD.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAdo Ado;

    public HomeController(ILogger<HomeController> logger, IAdo ado)
    {
        _logger = logger;
        Ado = ado;
    }

    // public IActionResult Index()
    // {
    //     var casa = Ado.ObtenerCasa(1);
    //     var lista = casa?.Electros?? new List<Electrodomestico>();
    //     return View(casa);
    // }
    
    public async Task<IActionResult> Index()
    {
        var casa = await Ado.ObtenerCasaAsync(1);
        var lista = casa?.Electros?? new List<Electrodomestico>();
        return View(casa);
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
