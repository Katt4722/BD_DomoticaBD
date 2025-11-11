using Biblioteca;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BD_DomoticaBD.MVC.Controllers;


[Authorize]
[Route("[controller]")]
public class CasaController : Controller
{
    private readonly ILogger<CasaController> _logger;
    private readonly IAdo Ado;

    public CasaController(ILogger<CasaController> logger, IAdo ado)
    {
        _logger = logger;
        Ado = ado;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var casas = await Ado.ObtenerTodasLasCasasAsync();
        return View(casas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detalle(int id)
    {
        var casa = await Ado.ObtenerCasaAsync(id);
        if (casa == null)
            return NotFound();
        return View(casa);
    }

    [HttpGet("Alta")]
    public IActionResult Alta()
    {
        var modelo = new Casa
        {
            Direccion = ""
        };
        return View(modelo);
    }

    [HttpPost("Alta")]
    public async Task<IActionResult> Alta(Casa casa)
    {
        if (!ModelState.IsValid)
            return View(casa);

        await Ado.AltaCasaAsync(casa);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await Ado.EliminarCasaAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
