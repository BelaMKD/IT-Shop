using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Enter a valid card number")]
        [Range(16, 16,
        ErrorMessage = "The credit card number must be 16 digits in length")]
        public int? CardNumber { get; set; }
        [Display(Name = "Choose Membership")]
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
