using DoDucQuanTestWAD.Data;
using DoDucQuanTestWAD.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DoDucQuanTestWAD.Controllers
{
    public class AuthController: Controller
    {
        private MyIdentityDbContext db;
        private UserManager<User> userManager;
        private RoleManager<Role> roleManager;
        public AuthController()
        {
            db = new MyIdentityDbContext();
            UserStore<User> userStore = new UserStore<User>(db);
            userManager = new UserManager<User>(userStore);
            RoleStore<Role> roleStore = new RoleStore<Role>(db);
            roleManager = new RoleManager<Role>(roleStore);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string UserName, string PasswordHash)
        {
            var user = await userManager.FindAsync(UserName, PasswordHash);
            if (user == null)
            {
                ViewBag.Errors = new string[] { "Invalid credentials" };
                return View("Error");
            }
            else
            {
                SignInManager<User, string> signInManager = new SignInManager<User, string>(userManager, Request.GetOwinContext().Authentication);
                await signInManager.SignInAsync(user, false, false);
                return Redirect("/Home");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/Home");
        }
    }
}