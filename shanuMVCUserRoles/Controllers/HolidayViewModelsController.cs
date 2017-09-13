using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using shanuMVCUserRoles.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;

namespace shanuMVCUserRoles.Controllers
{
    public class HolidayViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HolidayViewModels
        public ActionResult Index()
        {
            return View(db.AspNetHolidays.ToList());
        }

        // GET: HolidayViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HolidayViewModel holidayViewModel = db.AspNetHolidays.Find(id);
            if (holidayViewModel == null)
            {
                return HttpNotFound();
            }
            return View(holidayViewModel);
        }

        // GET: HolidayViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HolidayViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,TeamLeaderName,StartDate,EndDate,DaysOff,TLEmail,HolidayType,SickLeaveIndex,Flag")] HolidayViewModel holidayViewModel)
        {
            

            if (ModelState.IsValid)
            {
                
                db.AspNetHolidays.Add(holidayViewModel);
                db.SaveChanges();               
                return RedirectToAction("SuccessHoliday", "Success");
            }

            return View(holidayViewModel);
        }


        //employee inpending and approoved
        public ActionResult InPending()
        {
            var list = from b in db.AspNetHolidays
                       join c in db.Users on b.Email equals c.Email
                       where (c.UserName.Equals(User.Identity.Name) && b.Flag.Equals(false))
                       select b;
            return View(list.ToList());
        }      


        public ActionResult Approved()
        {
            var list = from b in db.AspNetHolidays
                       join c in db.Users on b.Email equals c.Email
                       where (c.UserName.Equals(User.Identity.Name) && b.Flag.Equals(true))
                       select b;
            return View(list.ToList());
        }

        //teamleader inpending and aproved
        public ActionResult InPendingTeamLeader()
        {
            var list = from b in db.AspNetHolidays
                       join c in db.Users on b.TLEmail equals c.Email
                       where (c.UserName.Equals(User.Identity.Name) && b.Flag.Equals(false))
                       select b;
            return View(list.ToList());
        }

        public ActionResult ApprovedTeamLeader()
        {
            var list = from b in db.AspNetHolidays
                       join c in db.Users on b.TLEmail equals c.Email
                       where (c.UserName.Equals(User.Identity.Name) && b.Flag.Equals(true))
                       select b;
            return View(list.ToList());
        }


        // GET: HolidayViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HolidayViewModel holidayViewModel = db.AspNetHolidays.Find(id);
            if (holidayViewModel == null)
            {
                return HttpNotFound();
            }
            return View(holidayViewModel);
        }

        // POST: HolidayViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,TeamLeaderName,StartDate,EndDate,DaysOff,TLEmail,HolidayType,SickLeaveIndex,Flag")] HolidayViewModel holidayViewModel)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(holidayViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(holidayViewModel);
        }

        

        // GET: HolidayViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HolidayViewModel holidayViewModel = db.AspNetHolidays.Find(id);
            if (holidayViewModel == null)
            {
                return HttpNotFound();
            }
            return View(holidayViewModel);
        }

        // POST: HolidayViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HolidayViewModel holidayViewModel = db.AspNetHolidays.Find(id);
            db.AspNetHolidays.Remove(holidayViewModel);
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
