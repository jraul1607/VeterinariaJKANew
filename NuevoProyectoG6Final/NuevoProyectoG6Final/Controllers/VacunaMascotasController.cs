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
    public class VacunaMascotasController : Controller
    {
        private readonly VetContext _context;

        public VacunaMascotasController(VetContext context)
        {
            _context = context;
        }

        // GET: VacunaMascotas
        public async Task<IActionResult> Index()
        {
            var vetContext = _context.VacunaMascotas.Include(v => v.Mascota).Include(v => v.Vacuna);
            return View(await vetContext.ToListAsync());
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
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Genero");
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
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Genero", vacunaMascota.IdMascota);
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
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Genero", vacunaMascota.IdMascota);
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
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Genero", vacunaMascota.IdMascota);
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
