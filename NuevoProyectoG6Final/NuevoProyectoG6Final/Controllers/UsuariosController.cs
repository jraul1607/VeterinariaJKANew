using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Vet.DAL;

public class UsuariosController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsuariosController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var usuarios = await _userManager.Users.ToListAsync();
        return View(usuarios);
    }

    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _userManager.FindByIdAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _userManager.FindByIdAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, ApplicationUser usuario)
    {
        if (id != usuario.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var userToUpdate = await _userManager.FindByIdAsync(id);
                userToUpdate.Nombre = usuario.Nombre;
                userToUpdate.PrimerApellido = usuario.PrimerApellido;
                userToUpdate.SegundoApellido = usuario.SegundoApellido;
                // Actualizar otras propiedades según sea necesario

                var result = await _userManager.UpdateAsync(userToUpdate);
                if (!result.Succeeded)
                {
                    // Manejar errores de actualización
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(usuario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(usuario);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ApplicationUser usuario)
    {
        if (ModelState.IsValid)
        {
            var result = await _userManager.CreateAsync(usuario);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Manejar errores de creación de usuario
            }
        }
        return View(usuario);
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _userManager.FindByIdAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var usuario = await _userManager.FindByIdAsync(id);
        if (usuario != null)
        {
            var result = await _userManager.DeleteAsync(usuario);
            if (!result.Succeeded)
            {
                // Manejar errores de eliminación de usuario
            }
        }
        return RedirectToAction(nameof(Index));
    }

    private bool UserExists(string id)
    {
        return _userManager.FindByIdAsync(id) != null;
    }
}










