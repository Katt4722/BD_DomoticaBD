using Biblioteca;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BD_DomoticaBD.MVC.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAdo _ado;

    public AccountController(ILogger<AccountController> logger, IAdo ado)
    {
        _logger = logger;
        _ado = ado;
    }

    [AllowAnonymous]
    [HttpGet("Login")]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(string correo, string contrasenia, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasenia))
        {
            ModelState.AddModelError(string.Empty, "Debe completar correo y contraseña.");
            return View();
        }

        var usuario = await _ado.UsuarioPorPassAsync(correo, contrasenia);
        if (usuario == null)
        {
            ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nombre ?? usuario.Correo),
            new Claim(ClaimTypes.Email, usuario.Correo),
            new Claim(ClaimTypes.Role, usuario.EsAdmin ? "Admin" : "User")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties { IsPersistent = true };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Electrodomestico");
    }
    
    [AllowAnonymous]
    [HttpGet("Register")]
    public IActionResult Register(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register(string nombre, string correo, string contrasenia, string telefono, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasenia))
        {
            ModelState.AddModelError(string.Empty, "Complete los campos requeridos.");
            return View();
        }

        var nuevo = new Biblioteca.Usuario
        {
            Nombre = nombre,
            Correo = correo,
            Contrasenia = contrasenia,
            Telefono = telefono
        };

        await _ado.AltaUsuarioAsync(nuevo);

        // Inicia la sesión automáticamente usando cookies
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, nuevo.Nombre ?? string.Empty),
            new Claim(ClaimTypes.Email, nuevo.Correo ?? string.Empty),
            new Claim(ClaimTypes.NameIdentifier, nuevo.IdUsuario.ToString()),
            new Claim(ClaimTypes.Role, nuevo.EsAdmin ? "Admin" : "User")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        // Redirigir al returnUrl si es local, si no llevar a Home/Index
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}