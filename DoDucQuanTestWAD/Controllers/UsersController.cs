using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoDucQuanTestWAD.Data;
using DoDucQuanTestWAD.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace DoDucQuanTestWAD.Controllers
{
    public class UsersController : Controller
    {
        private MyIdentityDbContext db;
        private UserManager<User> userManager;
        private RoleManager<Role> roleManager;

        public UsersController()
        {
            db = new MyIdentityDbContext();
            UserStore<User> userStore = new UserStore<User>(db);
            userManager = new UserManager<User>(userStore);
            RoleStore<Role> roleStore = new RoleStore<Role>(db);
            roleManager = new RoleManager<Role>(roleStore);
        }

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [HttpPatch]
        public ActionResult SetStatus()
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdentityNumber,PhoneNumber,Email,Status,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                var result = await userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdentityNumber,PhoneNumber,Email,Status,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
    }
}
