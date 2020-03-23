using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class ApplicationUser : IdentityUser
    {
        public int? CardNumber { get; set; }
        public int? MembershipId { get; set; }
        public Membership Membership { get; set; }
        public bool IsMember 
        {
            get 
            {
                return MembershipId.HasValue;
            }
        }
    }
}
