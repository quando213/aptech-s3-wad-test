using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoDucQuanTestWAD.Models
{
    public class User: IdentityUser
    {
        public string IdentityNumber { get; set; }
        public override string PhoneNumber { get; set; }
        public override string Email { get; set; }
        public int Status { get; set; }
    }
}