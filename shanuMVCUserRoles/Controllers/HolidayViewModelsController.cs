using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using shanuMVCUserRoles.Models;
using System;
using System.Collections.Generic;
using shanuMVCUserRoles.Resources;
using System.Security.AccessControl;
using System.Resources;
using Microsoft.Ajax.Utilities;

namespace shanuMVCUserRoles.Controllers
{
    public class HolidayViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HolidayViewModels
        [HttpGet]
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : string.Empty;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstNameDesc" : "FirstNameAsc";
            ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "EmailDesc" : "EmailAsc";
            ViewBag.TeamLeaderSortParm = String.IsNullOrEmpty(sortOrder) ? "TeamLeadNameDesc" : "TeamLeadNameAsc";
            ViewBag.TeamLeaderEmailsSortParm = String.IsNullOrEmpty(sortOrder) ? "TeamLeadEmailDesc" : "TeamLeadEmailAsc";
            ViewBag.FlagSortParm = String.IsNullOrEmpty(sortOrder) ? "true" : "false";
            ViewBag.HolidayTypeSortParm = String.IsNullOrEmpty(sortOrder) ? "HolidayAsc" : "HolidayDesc";
            ViewBag.SickLeaveIndexSortParm = String.IsNullOrEmpty(sortOrder) ? "SickLeaveAsc" : "SickLeaveDesc";
            ViewBag.DaysOffSortParm = String.IsNullOrEmpty(sortOrder) ? "DaysOffDesc" : "DaysOffAsc";
            ViewBag.DateSortParm = sortOrder == "Date" ? "DateDesc" : "Date";
            ViewBag.EndDateSortParm = sortOrder == "EndDate" ? "EndDateDesc" : "EndDate";

            
            var holidayRequests = from s in db.AspNetHolidays
                                  select s;


            if (!string.IsNullOrEmpty(searchString))
            {
                holidayRequests = holidayRequests.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            var dict = new Dictionary<string, IQueryable<HolidayViewModel>>
            {
                {"NameDesc", holidayRequests.OrderByDescending(s => s.LastName)},
                {"FirstNameDesc", holidayRequests.OrderByDescending(s => s.FirstName)},
                {"FirstNameAsc", holidayRequests.OrderBy(s => s.FirstName)},
                {"EmailDesc", holidayRequests.OrderByDescending(s => s.Email)},
                {"EmailAsc", holidayRequests.OrderByDescending(s => s.Email)},
                {"TeamLeadNameDesc", holidayRequests.OrderByDescending(s => s.TeamLeaderName)},
                {"TeamLeadNameAsc", holidayRequests.OrderBy(s => s.TeamLeaderName)},
                {"TeamLeadEmailDesc", holidayRequests.OrderByDescending(s => s.TLEmail)},
                {"TeamLeadEmailAsc", holidayRequests.OrderBy(s => s.TLEmail)},
                {"HolidayDesc", holidayRequests.OrderByDescending(s => s.HolidayType)},
                {"HolidayAsc",  holidayRequests.OrderBy(s => s.HolidayType)},
                {"SickLeaveDesc",  holidayRequests.OrderByDescending(s => s.HolidayType)},
                {"SickLeaveAsc",  holidayRequests.OrderBy(s => s.HolidayType)},
                {"true", holidayRequests.OrderByDescending(s => s.Flag)},
                {"false",holidayRequests.OrderBy(s => s.Flag)},
                {"Date", holidayRequests.OrderBy(s => s.StartDate)},
                {"DateDesc", holidayRequests.OrderByDescending(s => s.StartDate)},
                {"EndDate", holidayRequests.OrderBy(s => s.StartDate)},
                {"EndDateDesc", holidayRequests.OrderByDescending(s => s.StartDate)},
                {"DaysOffAsc", holidayRequests.OrderBy(s => s.DaysOff)},
                {"DaysOffDesc", holidayRequests.OrderByDescending(s => s.DaysOff)}
            };

            holidayRequests = sortOrder == null
                ?  holidayRequests.OrderBy(s => s.LastName)
                :  dict[sortOrder];
            
            return View(holidayRequests.ToList());
        }

        // GET: HolidayViewModels/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var holidayViewModel = db.AspNetHolidays.Find(id);
            if (holidayViewModel == null)
            {
                return HttpNotFound();
            }
            return View(holidayViewModel);
        }

        // GET: HolidayViewModels/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: HolidayViewModels/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = nameof(HolidayControllerResource.Info))] HolidayViewModel holidayViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(holidayViewModel);
            }

            db.AspNetHolidays.Add(holidayViewModel);
            db.SaveChanges();   
            
            return RedirectToAction(HolidayControllerResource.SuccessHoliday, HolidayControllerResource.Success);
        }


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
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var holidayViewModel = db.AspNetHolidays.Find(id);

            if (holidayViewModel == null)
            {
                return HttpNotFound();
            }
            return View(holidayViewModel);
        }

        // POST: HolidayViewModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = nameof(HolidayControllerResource.Info))] HolidayViewModel holidayViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(holidayViewModel);
            }

            db.Entry(holidayViewModel).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction(HolidayControllerResource.Index);
        }

        // GET: HolidayViewModels/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var holidayViewModel = db.AspNetHolidays.Find(id);
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
            var holidayViewModel = db.AspNetHolidays.Find(id);

            if (holidayViewModel != null)
            {
                db.AspNetHolidays.Remove(holidayViewModel);
            }

            db.SaveChanges();

            return RedirectToAction(HolidayControllerResource.Index);
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
