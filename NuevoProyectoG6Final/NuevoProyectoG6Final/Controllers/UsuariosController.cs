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
using Microsoft.AspNetCore.Mvc.Rendering;
using NuevoProyectoG6Final.Utils;
using System.Security.Claims;
using SQLitePCL;

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

    //public IActionResult Create()
    //{
    //    return View();
    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create(ApplicationUser usuario)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var result = await _userManager.CreateAsync(usuario);
    //        if (result.Succeeded)
    //        {
    //            return RedirectToAction(nameof(Index));
    //        }
    //        else
    //        {
    //            // Manejar errores de creación de usuario
    //        }
    //    }
    //    return View(usuario);
    //}

    // GET: Mascotas/Create
    //public IActionResult Create()
    //{
    //    //ViewData["IdRazaMascota"] = new SelectList(_userManager.Users.Nombre, "IdRazaMascota", "Nombre");
    //    //ViewData["Usuarios"] = new SelectList(_context.ApplicationUser, "Id", "Nombre");
    //    //ViewData["TiposMascota"] = new SelectList(_context.TipoMascotas, "IdTipoMascota", "Nombre");

    //    //var identidad = User.Identity as ClaimsIdentity;
    //    //var usuarioLoggueadoTask = RolesUtils.ObtenerUsuarioLogueado(_userManager, new ClaimsPrincipal(identidad));
    //    //usuarioLoggueadoTask.Wait();
    //    //var usuarioLoggueado = usuarioLoggueadoTask.Result;
    //    //ViewData["DuenoNombre"] = usuarioLoggueado.Nombre;

    //    return View();
    //}

    //// POST: Mascotas/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("Nombre,PrimerApellido,SegundoApellido,Email,Imagen2,UltimaFechaConexion,Imagen2, Estado")] ApplicationUser usuarios, IFormFile imagenFile)
    //{
        
    //    usuarios.UltimaFechaConexion = DateTime.Now.Date + DateTime.Now.TimeOfDay;
    //    usuarios.Estado = true;

    //    //Obtener usuario logueado y asignarlo como UsuarioCreacionId
    //    //var identidad = User.Identity as ClaimsIdentity;
    //    //var usuarioLoggueadoTask = RolesUtils.ObtenerUsuarioLogueado(_userManager, new ClaimsPrincipal(identidad));
    //    //usuarioLoggueadoTask.Wait();
    //    //var usuarioLoggueado = usuarioLoggueadoTask.Result;
    //    //mascota.UsuarioCreacionId = usuarioLoggueado.Id;

    //    ////If usuario no es admin, asignar el usuario loggeado al duenoId
    //    //if (!RolesUtils.UsuarioLogueadoEsRol(identidad, "Admin") || !RolesUtils.UsuarioLogueadoEsRol(identidad, "Veterinario"))
    //    //{
    //    //    mascota.DuenoId = usuarioLoggueado.Id;
    //    //}

    //    if (ModelState.IsValid)
    //    {
    //        if (imagenFile != null && imagenFile.Length > 0)
    //        {
    //            using (var memoryStream = new MemoryStream())
    //            {
    //                await imagenFile.CopyToAsync(memoryStream);
    //                usuarios.Imagen2 = memoryStream.ToArray();
    //            }
    //        }
    //        ApplicationUser.Add(usuarios);
    //        await usuarios.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    //ViewData["IdRazaMascota"] = new SelectList(_context.RazaMascotas, "IdRazaMascota", "Nombre", mascota.IdRazaMascota);
    //    //ViewData["Usuarios"] = new SelectList(_context.ApplicationUser, "Id", "Nombre", mascota.UsuarioCreacionId);
    //    //ViewData["TiposMascota"] = new SelectList(_context.TipoMascotas, "IdTipoMascota", "Nombre", mascota.IdTipoMascota);
    //    return View(usuarios);
    //}






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










