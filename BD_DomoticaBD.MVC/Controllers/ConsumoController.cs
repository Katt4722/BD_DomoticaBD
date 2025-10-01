using Microsoft.AspNetCore.Mvc;
using Biblioteca;

namespace BD_DomoticaBD.MVC.Controllers;
public class ConsumoController : Controller
{
    private readonly ILogger<ConsumoController> _logger;
    private readonly IAdo _ado;

    public ConsumoController(ILogger<ConsumoController>logger ,IAdo ado)
    {
        _logger = logger;
        _ado = ado;
    }

    public async Task<IActionResult> Index()
    {
        var consumos = await _ado.ObtenerTodosLosConsumosAsync();
        return View(consumos);
    }

    public async Task<IActionResult> Detalle(int id)
    {
        var consumo = await _ado.ObtenerConsumoAsync(id);
        if (consumo == null) return NotFound();
        return View(consumo);
    }

    public IActionResult Alta()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Alta(Consumo consumo)
    {
        if (!ModelState.IsValid) return View(consumo);
        await _ado.AltaConsumoAsync(consumo);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _ado.EliminarConsumoAsync(id);
        return RedirectToAction("Index");
    }
}