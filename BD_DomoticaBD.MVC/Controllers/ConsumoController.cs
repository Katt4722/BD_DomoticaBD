using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Biblioteca;
using System.Security.Claims;

namespace BD_DomoticaBD.MVC.Controllers;
    
[Authorize]
[Route("[controller]")]
public class ConsumoController : Controller
{
    private readonly ILogger<ConsumoController> _logger;
    private readonly IAdo _ado;

    public ConsumoController(ILogger<ConsumoController>logger ,IAdo ado)
    {
        _logger = logger;
        _ado = ado;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IEnumerable<Consumo> consumos;

        var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        consumos = await _ado.ObtenerConsumosPorUsuarioAsync(idUsuario);

        return View(consumos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detalle(int id)
    {
        var consumo = await _ado.ObtenerConsumoConElectrodomesticoAsync(id);
        if (consumo == null) return NotFound();
        return View(consumo);
    }

    [HttpGet("Alta")]
    public IActionResult Alta()
    {
        return View();
    }


    [HttpPost("Alta")]
    public async Task<IActionResult> Alta(Consumo consumo)
    {
        if (!ModelState.IsValid) return View(consumo);
        await _ado.AltaConsumoAsync(consumo);
        return RedirectToAction("Index");
    }

    [HttpPost("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _ado.EliminarConsumoAsync(id);
        return RedirectToAction("Index");
    }
}