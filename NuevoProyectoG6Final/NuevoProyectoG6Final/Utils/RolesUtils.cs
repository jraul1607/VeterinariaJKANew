using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using Vet.DAL;

namespace NuevoProyectoG6Final.Utils
{
    public static class RolesUtils
    {
        //Obtiene usuarios de acuerdo al rol
        public static async Task<List<ApplicationUser>> ObtenerUsuariosPorRol(
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            string nombreRol
            )
        {
            var role = await _roleManager.FindByNameAsync(nombreRol);
            if (role == null)
            {
                return new List<ApplicationUser>();
            }

            var userIdsInRole = await _userManager.GetUsersInRoleAsync(nombreRol);
            var usersInRole = _userManager.Users.Where(u => userIdsInRole.Contains(u)).ToList();

            return usersInRole.ToList();
        }

        //Conseguir usuario Logueado

        public static async Task<ApplicationUser> ObtenerUsuarioLogueado(UserManager<ApplicationUser> _userManager, ClaimsPrincipal identidad)
        {
            string idUsuarioLogueado = identidad.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            var usuarioLogueado = await _userManager.FindByIdAsync(idUsuarioLogueado);
            return usuarioLogueado;
        }

        //Confirmar si usuario logueado tiene cierto rol
        public static Boolean UsuarioLogueadoEsRol(ClaimsIdentity identidad, String rol)
        {
            var roles = identidad.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            return roles.Select(r => r.Value).Contains(rol);
        }
    }
}
