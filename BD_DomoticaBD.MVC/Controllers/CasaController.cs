using Biblioteca;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        IEnumerable<Casa> casas;
        if (User.IsInRole("Admin"))
            casas = await Ado.ObtenerTodasLasCasasAsync();
        else
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 
            casas = await Ado.ObtenerCasasPorUsuarioAsync(idUsuario);
        }
        return View(casas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detalle(int id)
    {
        var casa = await Ado.ObtenerCasaDetalleAsync(id);
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

        // Suponiendo que ya tienes el idUsuario del usuario logueado
        var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        // Crear la casa y asociarla al usuario
        await Ado.AltaCasaYAsociarUsuarioAsync(casa, idUsuario);
        
        return RedirectToAction(nameof(Index));
    }


    [HttpPost("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await Ado.EliminarCasaAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
