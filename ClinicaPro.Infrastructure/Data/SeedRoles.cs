using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ClinicaPro.Web.Data
{
    public static class SeedRoles
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = new[] { "Admin", "Medico", "Recepcionista", "RH" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
