using DoDucQuanTestWAD.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoDucQuanTestWAD.Data
{
    public class MyIdentityDbContext : IdentityDbContext<User>
    {
        public MyIdentityDbContext() : base("ConnectionString")
        {

        }
    }

}