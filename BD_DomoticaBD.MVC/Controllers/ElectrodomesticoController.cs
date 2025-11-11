using BD_DomoticaBD.MVC.ViewModels;
using Biblioteca;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace BD_DomoticaBD.MVC.Controllers;

[Authorize]
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
        // Obtener las casas para poblar el SelectList
        var casas = await Ado.ObtenerTodasLasCasasAsync();

        var modelo = new ViewModelElectrodomestico(casas)
        {
            Electrodomestico = new Electrodomestico
            {
                IdCasa = idCasa,
                Nombre = "",
                Tipo = "",
                Ubicacion = "",
            }
        };

        return View(modelo);
    }

    [HttpPost("Alta")]
    public async Task<IActionResult> Alta(ViewModelElectrodomestico modelo)
    {
        if (!ModelState.IsValid)
        {
            var casas = await Ado.ObtenerTodasLasCasasAsync();
            modelo.DireccionesList = new SelectList(casas, nameof(Casa.IdCasa), nameof(Casa.Direccion));
            return View(modelo);
        }

        await Ado.AltaElectrodomesticoAsync(modelo.Electrodomestico);

        return RedirectToAction(nameof(Index));  // Redirigir a la lista de electrodomésticos
    }

    [HttpPost("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await Ado.EliminarElectrodomesticoAsync(id);
        return RedirectToAction("Index", "Home");
    }

}
