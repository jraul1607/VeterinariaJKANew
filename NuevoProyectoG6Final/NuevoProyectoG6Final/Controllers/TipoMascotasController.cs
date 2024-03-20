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
    public class TipoMascotasController : Controller
    {
        private readonly VetContext _context;

        public TipoMascotasController(VetContext context)
        {
            _context = context;
        }

        // GET: TipoMascotas
        public async Task<IActionResult> Index(string busquedaTipoMascota)
        {
            var TipoMascota = _context.TipoMascotas.AsQueryable();

            if (!string.IsNullOrEmpty(busquedaTipoMascota))
            {
                TipoMascota = TipoMascota.Where(m => m.Nombre.Contains(busquedaTipoMascota));
            }
            var vetContext = await TipoMascota.ToListAsync();

            if (vetContext.Count == 0)
            {
                ViewBag.NoResultados = true;
            }
            else
            {
                ViewBag.NoResultados = false;
            }

            return View(vetContext);
            // return View(await _context.TipoMascotas.ToListAsync());
        }

        // GET: TipoMascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMascota = await _context.TipoMascotas
                .FirstOrDefaultAsync(m => m.IdTipoMascota == id);
            if (tipoMascota == null)
            {
                return NotFound();
            }

            return View(tipoMascota);
        }

        // GET: TipoMascotas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoMascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoMascota,Nombre")] TipoMascota tipoMascota)
        {
            tipoMascota.Estado = true;
            if (ModelState.IsValid)
            {
                _context.Add(tipoMascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoMascota);
        }

        // GET: TipoMascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMascota = await _context.TipoMascotas.FindAsync(id);
            if (tipoMascota == null)
            {
                return NotFound();
            }
            return View(tipoMascota);
        }

        // POST: TipoMascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoMascota,Nombre, Estado")] TipoMascota tipoMascota)
        {
            if (id != tipoMascota.IdTipoMascota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoMascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoMascotaExists(tipoMascota.IdTipoMascota))
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
            return View(tipoMascota);
        }

        // GET: TipoMascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var TipoMascota = await _context.TipoMascotas.FindAsync(id);
            if (TipoMascota == null)
            {
                return NotFound();
            }

            TipoMascota.Estado = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

            // POST: TipoMascotas/Delete/5
            [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var TipoMascota = await _context.TipoMascotas.FindAsync(id);
            if (TipoMascota != null)
            {
                TipoMascota.Estado = false;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TipoMascotaExists(int id)
        {
            return _context.TipoMascotas.Any(e => e.IdTipoMascota == id);
        }
    }
}
