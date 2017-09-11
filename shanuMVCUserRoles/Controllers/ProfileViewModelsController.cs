using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using shanuMVCUserRoles.Models;
using System.Security.Principal;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace shanuMVCUserRoles.Controllers
{
    public class ProfileViewModelsController : Controller
    {
        

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProfileViewModels
        public ActionResult Index()
        {
            return View(db.ProfileViewModel.ToList());
        }

        // GET: ProfileViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileViewModel profileViewModel = db.ProfileViewModel.Find(id);
            if (profileViewModel == null)
            {
                return HttpNotFound();
            }
            return View(profileViewModel);
        }

        // GET: ProfileViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,FirstName,LastName,Emai,Mark,CNP,Location,Team,TeamLeaderEmail")] ProfileViewModel profileViewModel)
        {


            /*string query = (from a in db.Users
                         where a.UserName.Equals(User.Identity.Name)
                         select a.Email).Single();*/

            if (ModelState.IsValid)
            {
                profileViewModel.UserName= User.Identity.Name;
                profileViewModel.FirstName = Request.Form["FirstName"];
                profileViewModel.LastName = Request.Form["LastName"];
                profileViewModel.Email = Request.Form["Email"];
                profileViewModel.Mark = Convert.ToInt32(Request.Form["Mark"]);
                profileViewModel.CNP = Request.Form["CNP"];
                profileViewModel.Location = Request.Form["Location"];
                profileViewModel.Team = Request.Form["Team"];
                profileViewModel.TeamLeaderEmail = Request.Form["TeamLeaderEmail"];

                db.ProfileViewModel.Add(profileViewModel);
                db.SaveChanges();
                return RedirectToAction("SuccessRegistration", "Success");
            }

            return View(profileViewModel);
        }

        // GET: ProfileViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileViewModel profileViewModel = db.ProfileViewModel.Find(id);
            if (profileViewModel == null)
            {
                return HttpNotFound();
            }
            return View(profileViewModel);
        }

        // POST: ProfileViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,FirstName,LastName,Email,Mark,CNP,Location,Team,TeamLeaderEmail")] ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profileViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profileViewModel);
        }

        // GET: ProfileViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileViewModel profileViewModel = db.ProfileViewModel.Find(id);
            if (profileViewModel == null)
            {
                return HttpNotFound();
            }
            return View(profileViewModel);
        }

        // POST: ProfileViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProfileViewModel profileViewModel = db.ProfileViewModel.Find(id);
            db.ProfileViewModel.Remove(profileViewModel);
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
    }
}
