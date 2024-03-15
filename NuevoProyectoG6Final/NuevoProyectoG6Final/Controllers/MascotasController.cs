using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vet.DAL;

namespace NuevoProyectoG6Final.Controllers
{
    public class MascotasController : Controller
    {
        private readonly VetContext _context;

        public MascotasController(VetContext context)
        {
            _context = context;
        }

        // GET: Mascotas
        public async Task<IActionResult> Index()
        {
            var vetContext = _context.Mascotas.Include(m => m.RazaMascota).ThenInclude(r => r.TipoMascota).Include(m => m.Usuario);
            return View(await vetContext.ToListAsync());
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
                .Include(m => m.Usuario)
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
            ViewData["IdUsuarioCreacion"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");

            var tiposMascota = _context.TipoMascotas.ToList();
            ViewData["TiposMascota"] = new SelectList(tiposMascota, "IdTipoMascota", "Nombre");

            return View();
        }

        // POST: Mascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMascota,Nombre,IdTipoMascota,IdRazaMascota,Genero,Edad,Peso,IdUsuarioCreacion,IdUsuario,FechaCreacion,FechaModificacion,Estado,Imagen")] Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRazaMascota"] = new SelectList(_context.RazaMascotas, "IdRazaMascota", "Nombre", mascota.IdRazaMascota);
            ViewData["IdUsuarioCreacion"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", mascota.IdUsuarioCreacion);
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
            ViewData["IdRazaMascota"] = new SelectList(_context.RazaMascotas, "IdRazaMascota", "Nombre", mascota.IdRazaMascota);
            ViewData["IdUsuarioCreacion"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", mascota.IdUsuarioCreacion);

            ViewData["TiposMascota"] = new SelectList(_context.TipoMascotas, "IdTipoMascota", "Nombre", mascota.IdTipoMascota);
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMascota,Nombre,IdTipoMascota,IdRazaMascota,Genero,Edad,Peso,IdUsuarioCreacion,IdUsuario,FechaCreacion,FechaModificacion,Estado,Imagen")] Mascota mascota)
        {
            if (id != mascota.IdMascota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MascotaExists(mascota.IdMascota))
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
            ViewData["IdRazaMascota"] = new SelectList(_context.RazaMascotas, "IdRazaMascota", "Nombre", mascota.IdRazaMascota);
            ViewData["IdUsuarioCreacion"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", mascota.IdUsuarioCreacion);
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

            var mascota = await _context.Mascotas
                .Include(m => m.RazaMascota)
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.IdMascota == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota != null)
            {
                _context.Mascotas.Remove(mascota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MascotaExists(int id)
        {
            return _context.Mascotas.Any(e => e.IdMascota == id);
        }
    }
}
