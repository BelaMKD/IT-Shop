using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class AddUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMembershipData membershipData;

        [BindProperty]
        public ApplicationUser UserNew { get; set; }
        public List<SelectListItem> Memberships { get; set; }
        [BindProperty, Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public AddUserModel(UserManager<ApplicationUser> userManager, IMembershipData membershipData)
        {
            this.userManager = userManager;
            this.membershipData = membershipData;
        }
        public IActionResult OnGet()
        {
            UserNew = new ApplicationUser();
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
                Memberships = membershipData.GetMemberships().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Type
                }).ToList();
                return Page();
            }
            if (UserNew.MembershipId.HasValue)
            {
                var tempMembership = membershipData.GetMembershipById(UserNew.MembershipId.Value);
                UserNew.Membership = tempMembership;
            }
            IdentityResult result = await userManager.CreateAsync(UserNew, Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ListUser");
            }
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return Page();
        }
    }
}