using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using shanuMVCUserRoles.Models;

namespace shanuMVCUserRoles.Controllers
{
    public class OOHRequestViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OOHRequestViewModels
        public ActionResult Index()
        {
            return View(db.OOHRequestViewModel.ToList());
        }

        // GET: OOHRequestViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OOHRequestViewModel oOHRequestViewModel = db.OOHRequestViewModel.Find(id);
            if (oOHRequestViewModel == null)
            {
                return HttpNotFound();
            }
            return View(oOHRequestViewModel);
        }

        // GET: OOHRequestViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OOHRequestViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Day,Hours,TicketNUmber,TeamLeaderEmail,Flag,Email")] OOHRequestViewModel oOHRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                db.OOHRequestViewModel.Add(oOHRequestViewModel);
                db.SaveChanges();
                return RedirectToAction("SuccessOOH","Success");
            }

            return View(oOHRequestViewModel);
        }
        //ooh request inPending and approved for employee
        public ActionResult InPending()
        {
            var list = from b in db.OOHRequestViewModel
                       join c in db.Users on b.Email equals c.Email
                       where (c.UserName.Equals(User.Identity.Name) && b.Flag.Equals(false))
                       select b;
            return View(list.ToList());
        }

        public ActionResult Approved()
        {
            var list = from b in db.OOHRequestViewModel
                       join c in db.Users on b.Email equals c.Email
                       where (c.UserName.Equals(User.Identity.Name) && b.Flag.Equals(true))
                       select b;
            return View(list.ToList());
        }

        //ooh request inPending and approved for teamleader
        public ActionResult InPendingTeamLeader()
        {
            var list = from b in db.OOHRequestViewModel
                       join c in db.Users on b.TeamLeaderEmail equals c.Email
                       where (c.UserName.Equals(User.Identity.Name) && b.Flag.Equals(false))
                       select b;
            return View(list.ToList());
        }

        public ActionResult ApprovedTeamLeader()
        {
            var list = from b in db.OOHRequestViewModel
                       join c in db.Users on b.TeamLeaderEmail equals c.Email
                       where (c.UserName.Equals(User.Identity.Name) && b.Flag.Equals(true))
                       select b;
            return View(list.ToList());
        }


        // GET: OOHRequestViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OOHRequestViewModel oOHRequestViewModel = db.OOHRequestViewModel.Find(id);
            if (oOHRequestViewModel == null)
            {
                return HttpNotFound();
            }
            return View(oOHRequestViewModel);
        }

        // POST: OOHRequestViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Day,Hours,TicketNUmber,TeamLeaderEmail,Flag,Email")] OOHRequestViewModel oOHRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oOHRequestViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oOHRequestViewModel);
        }

        // GET: OOHRequestViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OOHRequestViewModel oOHRequestViewModel = db.OOHRequestViewModel.Find(id);
            if (oOHRequestViewModel == null)
            {
                return HttpNotFound();
            }
            return View(oOHRequestViewModel);
        }

        // POST: OOHRequestViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OOHRequestViewModel oOHRequestViewModel = db.OOHRequestViewModel.Find(id);
            db.OOHRequestViewModel.Remove(oOHRequestViewModel);
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
