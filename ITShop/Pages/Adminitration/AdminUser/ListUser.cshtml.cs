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
    }
}