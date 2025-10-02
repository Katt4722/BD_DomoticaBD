using Biblioteca;
using Microsoft.AspNetCore.Mvc;

namespace BD_DomoticaBD.MVC.Controllers;

    [Route("[controller]")]
    public class ElectrodomesticoController : Controller
    {
    private readonly ILogger<ElectrodomesticoController> _logger;
    private readonly IAdo Ado;

    public ElectrodomesticoController(ILogger<ElectrodomesticoController> logger, IAdo ado)
    {
        _logger = logger;
        Ado = ado;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var electros = await Ado.ObtenerTodosLosElectrodomesticosAsync();
        return View(electros);
    }
    
    // Detalle
    [HttpGet("{id}")]
    public async Task<IActionResult> Detalle(int id)
    {
        var electro = await Ado.ObtenerElectrodomesticoAsync(id);
        if (electro == null)
            return NotFound();
        return View(electro);
    }

    // Alta 
    [HttpGet("Alta")]
    public async Task<IActionResult> Alta(int idCasa = 1)
    {
        var casas = await Ado.ObtenerTodasLasCasasAsync();
        ViewBag.Casas = casas;
        var modelo = new Electrodomestico
        {
            IdCasa = idCasa,
            Nombre = "",
            Tipo = "",
            Ubicacion = "",
            Apagado = false
        };
        return View(modelo);
}

    [HttpPost("Alta")]
    public async Task<IActionResult> Alta(Electrodomestico electrodomestico)
    {
        if (!ModelState.IsValid)
            return View(electrodomestico);

        await Ado.AltaElectrodomesticoAsync(electrodomestico);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await Ado.EliminarElectrodomesticoAsync(id);
        return RedirectToAction("Index", "Home");
    }

    }
