using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuevoProyectoG6Final.Utils;
using Vet.DAL;

namespace NuevoProyectoG6Final.Controllers
{
    public class VacunaMascotasController : Controller
    {
        private readonly VetContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public VacunaMascotasController(VetContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: VacunaMascotas
        public async Task<IActionResult> Index(string busquedaMascotaVacuna)
        {
            //Obtener usuario loggueado
            var identidad = User.Identity as ClaimsIdentity;
            var usuarioLoggueadoTask = RolesUtils.ObtenerUsuarioLogueado(_userManager, new ClaimsPrincipal(identidad));
            usuarioLoggueadoTask.Wait();
            var usuarioLoggueado = usuarioLoggueadoTask.Result;

            var mascotas = _context.VacunaMascotas
            .Include(v => v.Mascota)
            .Include(v => v.Vacuna)
            .AsQueryable();

            if (User.IsInRole("User"))
            {
                mascotas = mascotas
                    .Where(m => m.Mascota.DuenoId == usuarioLoggueado.Id)
                    .AsQueryable();
            }


            if (!string.IsNullOrEmpty(busquedaMascotaVacuna))
            {
                mascotas = mascotas.Where(m => m.Mascota.Nombre.Contains(busquedaMascotaVacuna));
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

        // GET: VacunaMascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacunaMascota = await _context.VacunaMascotas
                .Include(v => v.Mascota)
                .Include(v => v.Vacuna)
                .FirstOrDefaultAsync(m => m.IdVacunaMascota == id);
            if (vacunaMascota == null)
            {
                return NotFound();
            }

            return View(vacunaMascota);
        }

        // GET: VacunaMascotas/Create
        public IActionResult Create()
        {
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Nombre");
            ViewData["IdVacuna"] = new SelectList(_context.Vacunas, "IdVacuna", "Nombre");
            return View();
        }

        // POST: VacunaMascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVacunaMascota,IdVacuna,IdMascota,FechaVacuna")] VacunaMascota vacunaMascota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacunaMascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Nombre", vacunaMascota.IdMascota);
            ViewData["IdVacuna"] = new SelectList(_context.Vacunas, "IdVacuna", "Nombre", vacunaMascota.IdVacuna);
            return View(vacunaMascota);
        }

        // GET: VacunaMascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacunaMascota = await _context.VacunaMascotas.FindAsync(id);
            if (vacunaMascota == null)
            {
                return NotFound();
            }
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Nombre", vacunaMascota.IdMascota);
            ViewData["IdVacuna"] = new SelectList(_context.Vacunas, "IdVacuna", "Nombre", vacunaMascota.IdVacuna);
            return View(vacunaMascota);
        }

        // POST: VacunaMascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVacunaMascota,IdVacuna,IdMascota,FechaVacuna")] VacunaMascota vacunaMascota)
        {
            if (id != vacunaMascota.IdVacunaMascota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacunaMascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacunaMascotaExists(vacunaMascota.IdVacunaMascota))
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
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Nombre", vacunaMascota.IdMascota);
            ViewData["IdVacuna"] = new SelectList(_context.Vacunas, "IdVacuna", "Nombre", vacunaMascota.IdVacuna);
            return View(vacunaMascota);
        }

        // GET: VacunaMascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacunaMascota = await _context.VacunaMascotas
                .Include(v => v.Mascota)
                .Include(v => v.Vacuna)
                .FirstOrDefaultAsync(m => m.IdVacunaMascota == id);
            if (vacunaMascota == null)
            {
                return NotFound();
            }

            return View(vacunaMascota);
        }

        // POST: VacunaMascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacunaMascota = await _context.VacunaMascotas.FindAsync(id);
            if (vacunaMascota != null)
            {
                _context.VacunaMascotas.Remove(vacunaMascota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacunaMascotaExists(int id)
        {
            return _context.VacunaMascotas.Any(e => e.IdVacunaMascota == id);
        }
    }
}
