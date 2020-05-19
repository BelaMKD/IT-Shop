using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITShop.Pages.Adminitration.AdminRole
{
    public class AddRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        [BindProperty]
        public IdentityRole Role { get; set; }
        public AddRoleModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public void OnGet()
        {
            Role = new IdentityRole();
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            IdentityResult result = await roleManager.CreateAsync(Role);

            if (result.Succeeded)
            {
                return RedirectToPage("./ListRoles");
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return Page();
        } 
    }
}