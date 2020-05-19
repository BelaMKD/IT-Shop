using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITShop.Pages.Adminitration.AdminRole
{
    public class EditRoleModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        [BindProperty]
        public IdentityRole Role { get; set; }
        public List<string> Names { get; set; }
        public EditRoleModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            Names = new List<string>();
        }
        public async Task<IActionResult> OnGet(string roleId)
        {
            Role = await roleManager.FindByIdAsync(roleId);

            if (Role == null)
            {
                return RedirectToPage("./ListRoles");
            }

            //var editRoleViewModel = new EditRoleViewModel
            //{
            //    Id = role.Id,
            //    RoleName = role.Name,
            //    Users = new List<string>()
            //};

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, Role.Name))
                    Names.Add(user.UserName);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Role != null)
            {

                var result = await roleManager.UpdateAsync(Role);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                ModelState.AddModelError("", "Role not updated, something went wrong.");

                return Page();
            }

            return RedirectToAction("Index");
        }

    }
}