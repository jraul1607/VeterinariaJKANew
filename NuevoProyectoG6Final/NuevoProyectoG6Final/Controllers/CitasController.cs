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
    public class CitasController : Controller
    {
        private readonly VetContext _context;

        public CitasController(VetContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var vetContext = _context.Citas.Include(c => c.Mascota).Include(c => c.Usuario);
            return View(await vetContext.ToListAsync());
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Mascota)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Genero");
            ViewData["IdUsuarioPrincipal"] = new SelectList(_context.Usuarios, "IdUsuario", "Contrasena");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,IdMascota,IdUsuarioPrincipal,IdUsuarioSecundario,Fecha,Descripcion,Diagnostico,EstadoCita")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Genero", cita.IdMascota);
            ViewData["IdUsuarioPrincipal"] = new SelectList(_context.Usuarios, "IdUsuario", "Contrasena", cita.IdUsuarioPrincipal);
            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Genero", cita.IdMascota);
            ViewData["IdUsuarioPrincipal"] = new SelectList(_context.Usuarios, "IdUsuario", "Contrasena", cita.IdUsuarioPrincipal);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,IdMascota,IdUsuarioPrincipal,IdUsuarioSecundario,Fecha,Descripcion,Diagnostico,EstadoCita")] Cita cita)
        {
            if (id != cita.IdCita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.IdCita))
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
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Genero", cita.IdMascota);
            ViewData["IdUsuarioPrincipal"] = new SelectList(_context.Usuarios, "IdUsuario", "Contrasena", cita.IdUsuarioPrincipal);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Mascota)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.IdCita == id);
        }
    }
}
