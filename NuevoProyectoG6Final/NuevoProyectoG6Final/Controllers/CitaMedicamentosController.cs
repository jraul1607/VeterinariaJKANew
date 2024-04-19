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
    public class CitaMedicamentosController : Controller
    {
        private readonly VetContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CitaMedicamentosController(VetContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: CitaMedicamentos
        public async Task<IActionResult> Index(string busquedaMedicamentoCita)
        {
            //Obtener usuario loggueado
            var identidad = User.Identity as ClaimsIdentity;
            var usuarioLoggueadoTask = RolesUtils.ObtenerUsuarioLogueado(_userManager, new ClaimsPrincipal(identidad));
            usuarioLoggueadoTask.Wait();
            var usuarioLoggueado = usuarioLoggueadoTask.Result;

            var medicamentos = _context.CitaMedicamentos
            .Include(c => c.Cita)
            .Include(m => m.Medicamento)
            .AsQueryable();

            if (User.IsInRole("User"))
            {
                medicamentos = medicamentos
                    .Where(c => c.Cita.DuenoId == usuarioLoggueado.Id)
                    .AsQueryable();
            }

            if (!string.IsNullOrEmpty(busquedaMedicamentoCita))
            {
                medicamentos = medicamentos.Where(m => m.Medicamento.Nombre.Contains(busquedaMedicamentoCita));
            }

            var vetContext = await medicamentos.ToListAsync();

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

        // GET: CitaMedicamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaMedicamento = await _context.CitaMedicamentos
                .Include(c => c.Cita)
                .Include(c => c.Cita.Mascota)
                .Include(c => c.Medicamento)
                .FirstOrDefaultAsync(m => m.IdCitaMedicamento == id);

            ViewData["NombreMascota"] = citaMedicamento.Cita.Mascota.Nombre;

            if (citaMedicamento == null)
            {
                return NotFound();
            }

            return View(citaMedicamento);
        }

        // GET: CitaMedicamentos/Create
        public IActionResult Create()
        {
            ViewData["IdCita"] = new SelectList(_context.Citas, "IdCita", "IdCita");
            ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos, "IdMedicamento", "Marca");
            return View();
        }

        // POST: CitaMedicamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCitaMedicamento,IdCita,IdMedicamento,Dosis")] CitaMedicamento citaMedicamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citaMedicamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCita"] = new SelectList(_context.Citas, "IdCita", "IdCita", citaMedicamento.IdCita);
            ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos, "IdMedicamento", "Marca", citaMedicamento.IdMedicamento);
            return View(citaMedicamento);
        }

        // GET: CitaMedicamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaMedicamento = await _context.CitaMedicamentos.FindAsync(id);
            if (citaMedicamento == null)
            {
                return NotFound();
            }
            ViewData["IdCita"] = new SelectList(_context.Citas, "IdCita", "Descripcion", citaMedicamento.IdCita);
            ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos, "IdMedicamento", "Marca", citaMedicamento.IdMedicamento);
            return View(citaMedicamento);
        }

        // POST: CitaMedicamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCitaMedicamento,IdCita,IdMedicamento,Dosis")] CitaMedicamento citaMedicamento)
        {
            if (id != citaMedicamento.IdCitaMedicamento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citaMedicamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaMedicamentoExists(citaMedicamento.IdCitaMedicamento))
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
            ViewData["IdCita"] = new SelectList(_context.Citas, "IdCita", "Descripcion", citaMedicamento.IdCita);
            ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos, "IdMedicamento", "Marca", citaMedicamento.IdMedicamento);
            return View(citaMedicamento);
        }

        // GET: CitaMedicamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaMedicamento = await _context.CitaMedicamentos
                .Include(c => c.Cita)
                .Include(c => c.Medicamento)
                .FirstOrDefaultAsync(m => m.IdCitaMedicamento == id);
            if (citaMedicamento == null)
            {
                return NotFound();
            }

            return View(citaMedicamento);
        }

        // POST: CitaMedicamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citaMedicamento = await _context.CitaMedicamentos.FindAsync(id);
            if (citaMedicamento != null)
            {
                _context.CitaMedicamentos.Remove(citaMedicamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaMedicamentoExists(int id)
        {
            return _context.CitaMedicamentos.Any(e => e.IdCitaMedicamento == id);
        }
    }
}
