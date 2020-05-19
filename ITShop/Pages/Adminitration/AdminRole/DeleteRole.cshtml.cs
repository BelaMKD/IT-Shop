using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITShop.Pages.Adminitration.AdminRole
{
    public class DeleteRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        [BindProperty]
        public IdentityRole Role { get; set; }
        public DeleteRoleModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> OnGet(string roleId)
        {
            Role = await roleManager.FindByIdAsync(roleId);
            if (Role == null)
            {
                return RedirectToPage("./ListRoles");
            }
            return Page();
        }
        
    }
}