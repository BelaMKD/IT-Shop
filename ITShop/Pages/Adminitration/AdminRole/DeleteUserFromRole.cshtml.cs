using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITShop.Pages.Adminitration.AdminRole
{
    public class DeleteUserFromRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public ApplicationUser UserDelete { get; set; }
        [BindProperty]
        public IdentityRole Role { get; set; }
        [Display(Name = "Users")]
        public List<string> Names { get; set; }
        [Required, BindProperty]
        public string Name { get; set; }
        public DeleteUserFromRoleModel(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
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
                if (user!=null && await userManager.IsInRoleAsync(user, Role.Name))
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

            var result = await userManager.RemoveFromRoleAsync(user, role.Name);

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