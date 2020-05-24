using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITShop.Pages.Adminitration.AdminRole
{
    public class AddUserToRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public List<SelectListItem> Users { get; set; }
        [BindProperty]
        public IdentityRole Role { get; set; }
        [BindProperty, Required]
        public string Name { get; set; }
        [BindProperty]
        public List<string> Names { get; set; }
        public AddUserToRoleModel(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            Names = new List<string>();
        }
        public async Task<IActionResult> OnGet(string roleId)
        {
            Role = await roleManager.FindByIdAsync(roleId);
            if (Role == null)
            {
                return RedirectToPage("./ListRoles");
            }
            foreach (var user in userManager.Users)
            {
                if (!await userManager.IsInRoleAsync(user, Role.Name))
                {
                    Names.Add(user.UserName);
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                List<string> names = new List<string>();
                var roleTemp = await roleManager.FindByIdAsync(Role.Id);

                foreach (var userItem in userManager.Users)
                {
                    if (!await userManager.IsInRoleAsync(userItem, roleTemp.Name))
                    {
                        Names.Add(userItem.UserName);
                    }
                }
                return Page();
            }
            var user = await userManager.FindByNameAsync(Name);
            var role = await roleManager.FindByIdAsync(Role.Id);

            var result = await userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return RedirectToPage("./EditRole", new { roleId = Role.Id });
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return Page();
        }
    }
}