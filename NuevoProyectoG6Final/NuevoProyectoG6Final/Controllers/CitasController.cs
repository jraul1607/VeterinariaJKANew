﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using NuevoProyectoG6Final.Enums;
using NuevoProyectoG6Final.Utils;
using Vet.DAL;

namespace NuevoProyectoG6Final.Controllers
{
    public class CitasController : Controller
    {
        private readonly VetContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CitasController(
            VetContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Citas
        public async Task<IActionResult> Index(string busquedaCita)
        {
            DateTime fechaHoy = DateTime.Now;

            var historial = await _context.Citas.Include(c => c.Mascota)
                .Include(c => c.Dueno)
                .Include(c => c.VeterinarioPrincipal)
                .Include(c => c.VeterinarioSecundario)
                .Where(c => c.Fecha.Date < fechaHoy.Date)
                .ToListAsync();

            var enCurso = await _context.Citas.Include(c => c.Mascota)
                .Include(c => c.Dueno)
                .Include(c => c.VeterinarioPrincipal)
                .Include(c => c.VeterinarioSecundario)
                .Where(c => c.Fecha.Date == fechaHoy.Date)
                .ToListAsync();

            var proxima = await _context.Citas.Include(c => c.Mascota)
                .Include(c => c.Dueno)
                .Include(c => c.VeterinarioPrincipal)
                .Include(c => c.VeterinarioSecundario)
                .Where(c => c.Fecha.Date > fechaHoy.Date)
                .ToListAsync();

            ViewData["historial"] = historial;
            ViewData["enCurso"] = enCurso;
            ViewData["proxima"] = proxima;

            return View();
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
                .Include(c => c.Dueno)
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
            ViewData["Mascotas"] = new SelectList(_context.Mascotas.Where(m => m.Estado), "IdMascota", "Nombre");
            var duenosTask = RolesUtils.ObtenerUsuariosPorRol(_roleManager, _userManager, "User");
            duenosTask.Wait();
            var duenos = duenosTask.Result;
            ViewData["Duenos"] = new SelectList(duenos, "Id", "Nombre");
            var veterinariosTask = RolesUtils.ObtenerUsuariosPorRol(_roleManager, _userManager, "Veterinario");
            veterinariosTask.Wait();
            var veterinarios = veterinariosTask.Result.Where(v => v.Estado);
            ViewData["VetsPrincipales"] = new SelectList(veterinarios, "Id", "Nombre");
            ViewData["VetsSecundarios"] = new SelectList(veterinarios, "Id", "Nombre");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,IdMascota,DuenoId,VeterinarioPrincipalId,VeterinarioSecundarioId,Fecha,Descripcion,Diagnostico")] Cita cita)
        {
            // Verificar si la fecha de la cita es anterior a la fecha actual
            if (cita.Fecha < DateTime.Now)
            {
                ModelState.AddModelError("Diagnostico", "La fecha de la cita no puede ser anterior a la fecha actual.");
            }

            //El veterinario principal y secundario deben ser diferentes
            if (cita.VeterinarioPrincipalId == cita.VeterinarioSecundarioId)
            {
                ModelState.AddModelError("Diagnostico", "Nota: El veterinario secundario no puede ser el mismo que el veterinario principal.");
            }

            // Verificar si el veterinario ya tiene una cita en la misma fecha y hora
            if (await CitaAsignadaVet(cita.VeterinarioPrincipalId, cita.Fecha))
            {
                ModelState.AddModelError("Diagnostico", "El veterinario ya tiene una cita programada en esta fecha y hora.");
            }

            //No se permiten citas antes de las 7am o después de las 6pm, de lunes a sábado. Domingos NO Hay citas.
            if (Domingo(cita.Fecha) || (DiaLaboral(cita.Fecha) && (cita.Fecha.TimeOfDay < TimeSpan.FromHours(7) || cita.Fecha.TimeOfDay > TimeSpan.FromHours(18))))
            {
                ModelState.AddModelError("Diagnostico", "Nota: No se pueden programar citas los días Domingo ni los días de Lunes a Sábado antes de las 7 am y después de las 6 pm.");
            }

            if (ModelState.IsValid)
            {
                cita.EstadoCita = CitaEstado.AGENDADA.ToString();

                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Mascotas"] = new SelectList(_context.Mascotas.Where(m => m.Estado), "IdMascota", "Nombre");
            var duenosTask = RolesUtils.ObtenerUsuariosPorRol(_roleManager, _userManager, "User");
            duenosTask.Wait();
            var duenos = duenosTask.Result;
            ViewData["Duenos"] = new SelectList(duenos, "Id", "Nombre", cita.DuenoId);
            var veterinariosTask = RolesUtils.ObtenerUsuariosPorRol(_roleManager, _userManager, "Veterinario");
            veterinariosTask.Wait();
            var veterinarios = veterinariosTask.Result.Where(v => v.Estado);
            ViewData["VetsPrincipales"] = new SelectList(veterinarios, "Id", "Nombre");
            ViewData["VetsSecundarios"] = new SelectList(veterinarios, "Id", "Nombre");
            return View(cita);
        }

        // Verificación día laboral (de Lunes a Sábado)
        private bool DiaLaboral(DateTime fecha)
        {
            return fecha.DayOfWeek >= DayOfWeek.Monday && fecha.DayOfWeek <= DayOfWeek.Saturday;
        }
        // Verificación día domingo
        private bool Domingo(DateTime fecha)
        {
            return fecha.DayOfWeek == DayOfWeek.Sunday;
        }

        // Método para verificar si el veterinario tiene una cita en la misma fecha y hora
        private async Task<bool> CitaAsignadaVet(string IdUsuario, DateTime fecha)
        {
            return await _context.Citas.AnyAsync(c => (c.VeterinarioPrincipalId == IdUsuario || c.VeterinarioSecundarioId == IdUsuario) && c.Fecha == fecha);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas.FindAsync(id);

            // Verificar si la fecha de la cita es anterior a la fecha actual
            if (cita.Fecha < DateTime.Now)
            {
                ModelState.AddModelError("Fecha", "La fecha de la cita no puede ser anterior a la fecha actual.");
            }
            if (cita == null)
            {
                return NotFound();
            }
            ViewData["IdMascota"] = new SelectList(_context.Mascotas.Where(m => m.Estado), "IdMascota", "Nombre", cita.IdMascota);
            var duenosTask = RolesUtils.ObtenerUsuariosPorRol(_roleManager, _userManager, "User");
            duenosTask.Wait();
            var duenos = duenosTask.Result;
            ViewData["Duenos"] = new SelectList(duenos, "Id", "Nombre", cita.DuenoId);
            var veterinariosTask = RolesUtils.ObtenerUsuariosPorRol(_roleManager, _userManager, "Veterinario");
            veterinariosTask.Wait();
            var veterinarios = veterinariosTask.Result.Where(v => v.Estado);
            ViewData["VetsPrincipales"] = new SelectList(veterinarios, "Id", "Nombre", cita.VeterinarioPrincipalId);
            ViewData["VetsSecundarios"] = new SelectList(veterinarios, "Id", "Nombre", cita.VeterinarioSecundarioId);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,IdMascota,DuenoId,VeterinarioPrincipalId,VeterinarioSecundarioId,Fecha,Descripcion,Diagnostico,EstadoCita")] Cita cita)
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
            ViewData["IdMascota"] = new SelectList(_context.Mascotas.Where(m => m.Estado), "IdMascota", "Genero", cita.IdMascota);
            var duenosTask = RolesUtils.ObtenerUsuariosPorRol(_roleManager, _userManager, "User");
            duenosTask.Wait();
            var duenos = duenosTask.Result;
            ViewData["Duenos"] = new SelectList(duenos, "Id", "Nombre", cita.DuenoId);
            var veterinariosTask = RolesUtils.ObtenerUsuariosPorRol(_roleManager, _userManager, "Veterinario");
            veterinariosTask.Wait();
            var veterinarios = veterinariosTask.Result.Where(v => v.Estado);
            ViewData["VetsPrincipales"] = new SelectList(veterinarios, "Id", "Nombre", cita.VeterinarioPrincipalId);
            ViewData["VetsSecundarios"] = new SelectList(veterinarios, "Id", "Nombre", cita.VeterinarioSecundarioId);
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
                .Include(c => c.Dueno)
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
                cita.EstadoCita = CitaEstado.CANCELADA.ToString();
                _context.Citas.Update(cita);
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
