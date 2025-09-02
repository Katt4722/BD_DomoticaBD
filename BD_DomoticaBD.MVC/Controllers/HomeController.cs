using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BD_DomoticaBD.MVC.Models;
using Biblioteca.Persistencia.Dapper;
using Biblioteca;

namespace BD_DomoticaBD.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var lista = new List<Electrodomestico>
        {
            new Electrodomestico { IdElectrodomestico = 1, IdCasa = 1, Nombre = "Lavarropa", Tipo = "Lavado", Ubicacion = "Lavadero", Encendido = false, Apagado = true },
            new Electrodomestico { IdElectrodomestico = 2, IdCasa = 1, Nombre = "Heladera", Tipo = "Refrigeración", Ubicacion = "Cocina", Encendido = true, Apagado = false }
        };
        return View(lista);
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
