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
    public class PadecimientoMascotasController : Controller
    {
        private readonly VetContext _context;

        public PadecimientoMascotasController(VetContext context)
        {
            _context = context;
        }

        // GET: PadecimientoMascotas
        public async Task<IActionResult> Index(string busquedaMascotaPadecimiento)
        {
            var mascotas = _context.PadecimientoMascotas
            .Include(p => p.Mascota)
            .Include(p => p.Padecimiento)
            .AsQueryable();

            if (!string.IsNullOrEmpty(busquedaMascotaPadecimiento))
            {
                mascotas = mascotas.Where(m => m.Mascota.Nombre.Contains(busquedaMascotaPadecimiento));
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




        // GET: PadecimientoMascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var padecimientoMascota = await _context.PadecimientoMascotas
                .Include(p => p.Mascota)
                .Include(p => p.Padecimiento)
                .FirstOrDefaultAsync(m => m.IdPadecimientoMascota == id);
            if (padecimientoMascota == null)
            {
                return NotFound();
            }

            return View(padecimientoMascota);
        }

        // GET: PadecimientoMascotas/Create
        public IActionResult Create()
        {
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Nombre");
            ViewData["IdPadecimiento"] = new SelectList(_context.Padecimientos, "IdPadecimiento", "Nombre");
            return View();
        }

        // POST: PadecimientoMascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPadecimientoMascota,IdMascota,IdPadecimiento")] PadecimientoMascota padecimientoMascota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(padecimientoMascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Nombre", padecimientoMascota.IdMascota);
            ViewData["IdPadecimiento"] = new SelectList(_context.Padecimientos, "IdPadecimiento", "Nombre", padecimientoMascota.IdPadecimiento);
            return View(padecimientoMascota);
        }

        // GET: PadecimientoMascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var padecimientoMascota = await _context.PadecimientoMascotas.FindAsync(id);
            if (padecimientoMascota == null)
            {
                return NotFound();
            }
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Nombre", padecimientoMascota.IdMascota);
            ViewData["IdPadecimiento"] = new SelectList(_context.Padecimientos, "IdPadecimiento", "Nombre", padecimientoMascota.IdPadecimiento);
            return View(padecimientoMascota);
        }

        // POST: PadecimientoMascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPadecimientoMascota,IdMascota,IdPadecimiento")] PadecimientoMascota padecimientoMascota)
        {
            if (id != padecimientoMascota.IdPadecimientoMascota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(padecimientoMascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PadecimientoMascotaExists(padecimientoMascota.IdPadecimientoMascota))
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
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Nombre", padecimientoMascota.IdMascota);
            ViewData["IdPadecimiento"] = new SelectList(_context.Padecimientos, "IdPadecimiento", "Nombre", padecimientoMascota.IdPadecimiento);
            return View(padecimientoMascota);
        }

        // GET: PadecimientoMascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var padecimientoMascota = await _context.PadecimientoMascotas
                .Include(p => p.Mascota)
                .Include(p => p.Padecimiento)
                .FirstOrDefaultAsync(m => m.IdPadecimientoMascota == id);
            if (padecimientoMascota == null)
            {
                return NotFound();
            }

            return View(padecimientoMascota);
        }

        // POST: PadecimientoMascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var padecimientoMascota = await _context.PadecimientoMascotas.FindAsync(id);
            if (padecimientoMascota != null)
            {
                _context.PadecimientoMascotas.Remove(padecimientoMascota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PadecimientoMascotaExists(int id)
        {
            return _context.PadecimientoMascotas.Any(e => e.IdPadecimientoMascota == id);
        }
    }
}
