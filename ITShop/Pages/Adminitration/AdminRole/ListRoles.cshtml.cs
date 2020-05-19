using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITShop.Pages.Adminitration.AdminRole
{
    public class ListRolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public IEnumerable<IdentityRole> Roles { get; set; }
        [BindProperty]
        public IdentityRole Role { get; set; }
        public ListRolesModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public void OnGet()
        {
            Roles = roleManager.Roles;
        }
        public async Task<IActionResult> OnPost(string roleId)
        {
            roleId = "asdasd";
            Role = await roleManager.FindByIdAsync(roleId);
            if (Role != null)
            {
                var result = await roleManager.DeleteAsync(Role);
                if (result.Succeeded)
                    return RedirectToPage("./ListRoles");
                ModelState.AddModelError("", "Something went wrong while deleting this role.");
            }
            else
            {
                ModelState.AddModelError("", "This role can't be found.");
            }
            return RedirectToPage("./ListRoles");

        }
    }
}