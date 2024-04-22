using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuevoProyectoG6Final.Utils;
using Vet.DAL;

namespace NuevoProyectoG6Final.Controllers
{
    public class MascotasController : Controller
    {
        private readonly VetContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MascotasController(VetContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Mascotas
        public async Task<IActionResult> Index(string busquedaMascota)
        {

            //Obtener usuario logueado
            var identidad = User.Identity as ClaimsIdentity;
            var usuarioLoggueadoTask = RolesUtils.ObtenerUsuarioLogueado(_userManager, new ClaimsPrincipal(identidad));
            usuarioLoggueadoTask.Wait();
            var usuarioLoggueado = usuarioLoggueadoTask.Result;

            var mascotas = _context.Mascotas
            .Include(m => m.Dueno)
            .Include(m => m.RazaMascota)
            .ThenInclude(r => r.TipoMascota)
            .AsQueryable();

            if (User.IsInRole("User"))
            {
                mascotas = mascotas
                    .Where(m => m.Dueno.Id == usuarioLoggueado.Id)
                    .AsQueryable();
            }

            if (!string.IsNullOrEmpty(busquedaMascota))
            {
                mascotas = mascotas.Where(m => m.Nombre.Contains(busquedaMascota));
            }

            var vetContext = await mascotas.ToListAsync();

            if (vetContext.Count == 0)
            {
                ViewBag.NoResultados = true;
            }
            else
            {
                ViewBag.NoResultados = false;
            }

            return View(vetContext);
        }

        // GET: Mascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .Include(m => m.RazaMascota)
                .Include(m => m.Dueno)
                .FirstOrDefaultAsync(m => m.IdMascota == id);

            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // GET: Mascotas/Create
        public IActionResult Create()
        {
            ViewData["IdRazaMascota"] = new SelectList(_context.RazaMascotas, "IdRazaMascota", "Nombre");
            ViewData["Usuarios"] = new SelectList(_context.ApplicationUser, "Id", "Nombre");
            ViewData["TiposMascota"] = new SelectList(_context.TipoMascotas, "IdTipoMascota", "Nombre");

            var identidad = User.Identity as ClaimsIdentity;
            var usuarioLoggueadoTask = RolesUtils.ObtenerUsuarioLogueado(_userManager, new ClaimsPrincipal(identidad));
            usuarioLoggueadoTask.Wait();
            var usuarioLoggueado = usuarioLoggueadoTask.Result;
            ViewData["DuenoNombre"] = usuarioLoggueado.Nombre;

            return View();
        }

        // POST: Mascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMascota,Nombre,IdTipoMascota,IdRazaMascota,Genero,Edad,Peso,DuenoId,Imagen,Imagen2")] Mascota mascota, IFormFile imagenFile)
        {
            mascota.FechaCreacion = DateTime.Now.Date + DateTime.Now.TimeOfDay;
            mascota.FechaModificacion = DateTime.Now.Date + DateTime.Now.TimeOfDay;
            mascota.Estado = true;

            //Obtener usuario logueado y asignarlo como UsuarioCreacionId
            var identidad = User.Identity as ClaimsIdentity;
            var usuarioLoggueadoTask = RolesUtils.ObtenerUsuarioLogueado(_userManager, new ClaimsPrincipal(identidad));
            usuarioLoggueadoTask.Wait();
            var usuarioLoggueado = usuarioLoggueadoTask.Result;
            mascota.UsuarioCreacionId = usuarioLoggueado.Id;

            //If usuario no es admin, asignar el usuario loggeado al duenoId
            if (!RolesUtils.UsuarioLogueadoEsRol(identidad, "Admin") || !RolesUtils.UsuarioLogueadoEsRol(identidad, "Veterinario"))
            {
                mascota.DuenoId = usuarioLoggueado.Id;
            }

            if (ModelState.IsValid)
            {
                if (imagenFile != null && imagenFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagenFile.CopyToAsync(memoryStream);
                        mascota.Imagen2 = memoryStream.ToArray();
                    }
                }
                _context.Add(mascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRazaMascota"] = new SelectList(_context.RazaMascotas, "IdRazaMascota", "Nombre", mascota.IdRazaMascota);
            ViewData["Usuarios"] = new SelectList(_context.ApplicationUser, "Id", "Nombre", mascota.UsuarioCreacionId);
            ViewData["TiposMascota"] = new SelectList(_context.TipoMascotas, "IdTipoMascota", "Nombre", mascota.IdTipoMascota);
            return View(mascota);
        }

        // GET: Mascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }

            var duenosTask = RolesUtils.ObtenerUsuariosPorRol(_roleManager, _userManager, "User");
            duenosTask.Wait();
            var duenos = duenosTask.Result;
            
            ViewData["IdRazaMascota"] = new SelectList(_context.RazaMascotas, "IdRazaMascota", "Nombre", mascota.IdRazaMascota);
            ViewData["Usuarios"] = new SelectList(_context.ApplicationUser, "Id", "Nombre", mascota.UsuarioCreacionId);
            ViewData["TiposMascota"] = new SelectList(_context.TipoMascotas, "IdTipoMascota", "Nombre", mascota.IdTipoMascota);
            ViewData["Duenos"] = new SelectList(duenos, "Id", "Nombre");
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMascota,Nombre,IdTipoMascota,IdRazaMascota,Genero,Edad,Peso,UsuarioCreacionId,DuenoId,Estado,Imagen")] Mascota mascota)
        {
            if (id != mascota.IdMascota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalMascota = await _context.Mascotas.AsNoTracking().FirstOrDefaultAsync(m => m.IdMascota == id);

                    mascota.FechaCreacion = originalMascota.FechaCreacion;
                    mascota.FechaModificacion = DateTime.Now.Date + DateTime.Now.TimeOfDay;

                    _context.Update(mascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    return NotFound();

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRazaMascota"] = new SelectList(_context.RazaMascotas, "IdRazaMascota", "Nombre", mascota.IdRazaMascota);
            ViewData["Usuarios"] = new SelectList(_context.ApplicationUser, "Id", "Nombre", mascota.UsuarioCreacionId);
            ViewData["TiposMascota"] = new SelectList(_context.TipoMascotas, "IdTipoMascota", "Nombre", mascota.IdTipoMascota);
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }

            mascota.Estado = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota != null)
            {
                mascota.Estado = false;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
