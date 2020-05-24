using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ITShop.Pages.Adminitration.AdminUser
{
    public class ListUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        public IEnumerable<ApplicationUser> Users { get; set; }
        public ListUserModel(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public void OnGet()
        {
            Users = userManager.Users
                .Include(x=>x.Membership)
                .ToList();
        }
        public async Task<IActionResult> OnPostDeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
         
            if (user != null && user != await userManager.GetUserAsync(User))
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToPage("./ListUser");
                else
                    ModelState.AddModelError("", "Something went wrong while deleting this user.");
            }
            else
            {
                if (user==null)
                {
                    ModelState.AddModelError("", "This user can't be found !");
                }
                if (user == await userManager.GetUserAsync(User))
                {
                    ModelState.AddModelError("", "You can't delete the user that is currently logged in");
                }
            }
            return RedirectToAction("./ListUser");
        }
    }
}