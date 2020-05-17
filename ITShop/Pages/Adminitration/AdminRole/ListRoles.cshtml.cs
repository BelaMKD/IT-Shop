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
        public ListRolesModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public void OnGet()
        {
            Roles = roleManager.Roles;
        }
    }
}