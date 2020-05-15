using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITShop.Pages.Adminitration.AdminUser
{
    public class EditUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMembershipData membershipData;

        [BindProperty]
        public ApplicationUser UserEdit { get; set; }
        public List<SelectListItem> Memberships { get; set; }
        public EditUserModel(UserManager<ApplicationUser> userManager, IMembershipData membershipData)
        {
            this.userManager = userManager;
            this.membershipData = membershipData;
        }    
        public async Task<IActionResult> OnGet(string id)
        {
            UserEdit = await userManager.FindByIdAsync(id);
            if (UserEdit == null)
            {
                return RedirectToPage("./ListUser");
            }
            Memberships = membershipData.GetMemberships().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Type
            }).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./ListUser");
            }

            var user = await userManager.FindByIdAsync(UserEdit.Id);
            if (user != null)
            {
                user.Email = UserEdit.Email;
                user.UserName = UserEdit.UserName;
                if (UserEdit.MembershipId.HasValue)
                {
                    user.MembershipId = UserEdit.MembershipId.Value;
                }
                else
                {
                    user.MembershipId = null;
                }
            }

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToPage("./ListUser");
            }
            ModelState.AddModelError("", "User not updated, something went wrong.");
            return Page();
        }
    }
}