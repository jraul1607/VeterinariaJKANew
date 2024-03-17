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
    public class RazaMascotasController : Controller
    {
        private readonly VetContext _context;

        public RazaMascotasController(VetContext context)
        {
            _context = context;
        }

        // GET: RazaMascotas
        public async Task<IActionResult> Index(string busquedaRazaMascota)
        {
            var RazaMascota = _context.RazaMascotas
                .Include(r => r.TipoMascota)
                .AsQueryable();
            if (!string.IsNullOrEmpty(busquedaRazaMascota))
            {
                RazaMascota = RazaMascota.Where(m => m.Nombre.Contains(busquedaRazaMascota));
            }
            var vetContext = await RazaMascota.ToListAsync();

            if (vetContext.Count == 0)
            {
                ViewBag.NoResultados = true;
            }
            else
            {
                ViewBag.NoResultados = false;
            }

            return View(vetContext);
            // var vetContext = _context.RazaMascotas.Include(r => r.TipoMascota);
            // return View(await vetContext.ToListAsync());
        }

        // GET: RazaMascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var razaMascota = await _context.RazaMascotas
                .Include(r => r.TipoMascota)
                .FirstOrDefaultAsync(m => m.IdRazaMascota == id);
            if (razaMascota == null)
            {
                return NotFound();
            }

            return View(razaMascota);
        }

        // GET: RazaMascotas/Create
        public IActionResult Create()
        {
            ViewData["IdTipoMascota"] = new SelectList(_context.TipoMascotas, "IdTipoMascota", "Nombre");
            return View();
        }

        // POST: RazaMascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRazaMascota,IdTipoMascota,Nombre")] RazaMascota razaMascota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(razaMascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoMascota"] = new SelectList(_context.TipoMascotas, "IdTipoMascota", "Nombre", razaMascota.IdTipoMascota);
            return View(razaMascota);
        }

        // GET: RazaMascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var razaMascota = await _context.RazaMascotas.FindAsync(id);
            if (razaMascota == null)
            {
                return NotFound();
            }
            ViewData["IdTipoMascota"] = new SelectList(_context.TipoMascotas, "IdTipoMascota", "Nombre", razaMascota.IdTipoMascota);
            return View(razaMascota);
        }

        // POST: RazaMascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRazaMascota,IdTipoMascota,Nombre")] RazaMascota razaMascota)
        {
            if (id != razaMascota.IdRazaMascota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(razaMascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RazaMascotaExists(razaMascota.IdRazaMascota))
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
            ViewData["IdTipoMascota"] = new SelectList(_context.TipoMascotas, "IdTipoMascota", "Nombre", razaMascota.IdTipoMascota);
            return View(razaMascota);
        }

        // GET: RazaMascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var razaMascota = await _context.RazaMascotas
                .Include(r => r.TipoMascota)
                .FirstOrDefaultAsync(m => m.IdRazaMascota == id);
            if (razaMascota == null)
            {
                return NotFound();
            }

            return View(razaMascota);
        }

        // POST: RazaMascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var razaMascota = await _context.RazaMascotas.FindAsync(id);
            if (razaMascota != null)
            {
                _context.RazaMascotas.Remove(razaMascota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RazaMascotaExists(int id)
        {
            return _context.RazaMascotas.Any(e => e.IdRazaMascota == id);
        }
    }
}
