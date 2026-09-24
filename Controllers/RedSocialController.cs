using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp_redSocial.Models;

namespace tp_redSocial.Controllers;

public class RedSocialController : Controller
{
    public IActionResult Index()
    {
        if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString("NombreUsuario")))
        {
            return RedirectToAction(nameof(Bienvenida));
        }

        return View();
    }

    public IActionResult Registrarse()
    {
        return View();
    }

    public IActionResult Bienvenida()
    {
        if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString("NombreUsuario")))
        {
            return View();
        }
        else
        {
            return RedirectToAction(nameof(Index));
        }
    }
    
    [HttpPost]
    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult ValidarUsuario(string nombre, string apellido, string nombreUsuario, string contraseña)
    {
        Usuario usuario = new Usuario
        {
            Nombre = nombre,
            Apellido = apellido,
            NombreUsuario = nombreUsuario,
            Contraseña = contraseña
        };

        if (!Usuario.ValidarDatosRegistro(usuario.Nombre, usuario.Apellido, usuario.NombreUsuario, usuario.Contraseña))
        {
            return View("Registrarse");
        }

        if (Bd.FijarseSiExisteUsuario(nombreUsuario))
        {
            ViewBag.ErrorMessage = "El nombre de usuario ya existe. Por favor, elija otro.";
            return View("Registrarse");
        }

        Bd.AgregarUsuario(usuario);

        HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario);
        HttpContext.Session.SetString("Nombre", usuario.Nombre);
        HttpContext.Session.SetString("Apellido", usuario.Apellido);

        return RedirectToAction(nameof(Bienvenida));
    }

    [HttpPost]
    public IActionResult IniciarSesion(string nombreUsuario, string contraseña)
    {
        Usuario usuario = Bd.ObtenerUsuario(nombreUsuario, contraseña);

        if (usuario != null)
        {
            HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario);
            HttpContext.Session.SetString("Nombre", usuario.Nombre);
            HttpContext.Session.SetString("Apellido", usuario.Apellido);
            return RedirectToAction(nameof(Bienvenida));
        }

        ViewBag.ErrorMessage = "Nombre de usuario o contraseña incorrectos.";
        return View("Index");
    }
}