using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoDucQuanTestWAD.Models
{
    public class User: IdentityUser
    {
        [Required]
        [Display(Name = "Identity number")]
        public string IdentityNumber { get; set; }
        [Required]
        [Display(Name = "Phone number")]
        public override string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Email")]
        public override string Email { get; set; }
        [Required]
        [Display(Name = "Status")]
        public Status Status { get; set; }
        [Required]
        [Display(Name = "Username")]
        public override string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public override string PasswordHash { get; set; }
    }

    public enum Status
    {
        Active = 1,
        Deactive = -1,
        Pending = 0
    }
}