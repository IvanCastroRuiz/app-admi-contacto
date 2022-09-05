using AppContactos.Data; // 1 Instruccion
using AppContactos.Models; // 2 Instruccion
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // 3 Instruccion
using System.Diagnostics;

namespace AppContactos.Controllers;

public class HomeController : Controller
{
    private readonly AppDBContext _contexto; // 4 Instruccion
    public HomeController(AppDBContext contexto) // 5 Instruccion
    {
       _contexto = contexto;
    }
    [HttpGet]
    public async Task<IActionResult> Index() // 6 Instruccion
    {
        return View(await _contexto.Contacto.ToListAsync());
    }

    // Crear

    [HttpGet]
    public IActionResult Crear() // 7 Instruccion
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Contacto contacto) // 7 Instruccion
    {
        if (ModelState.IsValid)
        {
            _contexto.Contacto.Add(contacto);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    // Editar
    [HttpGet]
    public IActionResult Editar(int? id) 
    {
        if(id == null)
        {
            return NotFound();
        }

        var usuario = _contexto.Contacto.Find(id);

        if(usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Contacto contacto) //
    {
        if (ModelState.IsValid)
        {
            _contexto.Contacto.Update(contacto);
            
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }


    // Detalle
    [HttpGet]
    public IActionResult Detalle(int? id) 
    {
        if(id == null)
        {
            return NotFound();
        }

        var contacto = _contexto.Contacto.Find(id);

        if(contacto == null)
        {
            return NotFound();
        }

        return View(contacto);
    }

    // Borrar
    [HttpGet]
    public IActionResult Borrar(int? id) 
    {
        if(id == null)
        {
            return NotFound();
        }

        var contacto = _contexto.Contacto.Find(id);

        if(contacto == null)
        {
            return NotFound();
        }

        return View(contacto);
    }

    [HttpPost, ActionName("Borrar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BorrarUsuario(int? id)
    {
        var contacto = await _contexto.Contacto.FindAsync(id);
        if (contacto == null)
        {
            return View();
        }

        _contexto.Contacto.Remove(contacto);
        await _contexto.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
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
