using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using shanuMVCUserRoles.Models;
using System.Collections.Generic;
using System.Net.Mail;
using CsvHelper;

namespace shanuMVCUserRoles.Controllers
{
    public class TimeSheetViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeSheetViewModels
        public ActionResult Index()
        {

            return View(db.TimeSheetViewModel.ToList());
        }

        // GET: TimeSheetViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSheetViewModel timeSheetViewModel = db.TimeSheetViewModel.Find(id);
            if (timeSheetViewModel == null)
            {
                return HttpNotFound();
            }
            return View(timeSheetViewModel);
        }

        // GET: TimeSheetViewModels/Create
        public ActionResult Create()
        {
            ViewBag.Name = Pontaj();    
            return View();
        }

        // POST: TimeSheetViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Mark,FirstName,LastName,CNP,TeamLeaderEmail,Flag")] TimeSheetViewModel timeSheetViewModel)
        {
            if (ModelState.IsValid)
            {
                db.TimeSheetViewModel.Add(timeSheetViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timeSheetViewModel);
        }



        // GET: TimeSheetViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSheetViewModel timeSheetViewModel = db.TimeSheetViewModel.Find(id);
            if (timeSheetViewModel == null)
            {
                return HttpNotFound();
            }
            return View(timeSheetViewModel);
        }

        // POST: TimeSheetViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Mark,FirstName,LastName,CNP,TeamLeaderEmail,Flag")] TimeSheetViewModel timeSheetViewModel)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(timeSheetViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timeSheetViewModel);
        }
        

        public List<HolidayViewModel> Holiday()
        {
            var holiday = from b in db.AspNetHolidays
                          join c in db.Users on b.Email equals c.Email
                          where (c.UserName.Equals(User.Identity.Name) && (b.StartDate.Month.Equals(DateTime.Now.Month) || b.EndDate.Month.Equals(DateTime.Now.Month)) 
                          && b.Flag==true && b.HolidayType!="Concediu medical" && b.HolidayType!="Concediu fara plata")
                          select b;
            return holiday.ToList();
        }
        public List<HolidayViewModel> FullMedicalHoliday()
        {
            var holiday = from b in db.AspNetHolidays
                          join c in db.Users on b.Email equals c.Email
                          where (c.UserName.Equals(User.Identity.Name) && (b.StartDate.Month.Equals(DateTime.Now.Month) || b.EndDate.Month.Equals(DateTime.Now.Month))
                          && b.Flag == true && b.HolidayType=="Concediu medical" && 
                          (b.SickLeaveIndex==2|| b.SickLeaveIndex == 3
                          || b.SickLeaveIndex == 4 || b.SickLeaveIndex == 5
                          || b.SickLeaveIndex == 6 || b.SickLeaveIndex == 12
                          || b.SickLeaveIndex == 14))
                          select b;
            return holiday.ToList();
        }
        public List<HolidayViewModel> PartialMedicalHoliday()
        {
            var holiday = from b in db.AspNetHolidays
                          join c in db.Users on b.Email equals c.Email
                          where (c.UserName.Equals(User.Identity.Name) && (b.StartDate.Month.Equals(DateTime.Now.Month) || b.EndDate.Month.Equals(DateTime.Now.Month))
                          && b.Flag == true && b.HolidayType == "Concediu medical" &&
                          (b.SickLeaveIndex == 1 || b.SickLeaveIndex == 7
                          || b.SickLeaveIndex == 13 || b.SickLeaveIndex == 15))
                          select b;
            return holiday.ToList();
        }
        public List<HolidayViewModel> NotPaidHoliday()
        {
            var holiday = from b in db.AspNetHolidays
                          join c in db.Users on b.Email equals c.Email
                          where (c.UserName.Equals(User.Identity.Name) && (b.StartDate.Month.Equals(DateTime.Now.Month) || b.EndDate.Month.Equals(DateTime.Now.Month))
                          && b.Flag == true && b.HolidayType == "Concediu fara plata")
                          select b;
            return holiday.ToList();
        }
        public List<OOHRequestViewModel> Ooh()
        {
            var ooh = from b in db.OOHRequestViewModel
                      join c in db.Users on b.Email equals c.Email
                      where (c.UserName.Equals(User.Identity.Name) && b.Day.Month.Equals(DateTime.Now.Month) && b.Flag==true)
                      select b;
            return ooh.ToList();
        }


        public string PontajV2()
        {
            bool holiday = Holiday().Count > 0;
            bool fullMedicalHoliday = FullMedicalHoliday().Count > 0;
            bool partialMedicalHoliday = PartialMedicalHoliday().Count > 0;
            bool notPaidHoliday = NotPaidHoliday().Count > 0;
            bool ooh = Ooh().Count > 0;

            int daysInMonth = System.DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            int businessDaysInMonth = 0;
            for (var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); date < new DateTime(DateTime.Now.Year, DateTime.Now.Month, daysInMonth); date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday
                    && date.DayOfWeek != DayOfWeek.Sunday)
                    businessDaysInMonth++;
            }

            int workingHoursThisMonth = 0;
            int hoursWorked = 0;
            double hoursPaid = 0;
            double transport = 80.0;
            int mealTickets = 0;

            int daysOffHoliday = 0;
            int daysOffFullMedical = 0;
            int daysOffPartialMedical = 0;
            int daysOffNotPaid = 0;

            double bonusHours = 0;

            if (holiday == true)
            {
                foreach (var hol in Holiday())
                {
                    for (var date = hol.StartDate; date <= hol.EndDate; date = date.AddDays(1))
                    {
                        if (date.DayOfWeek != DayOfWeek.Saturday
                            && date.DayOfWeek != DayOfWeek.Sunday)
                            daysOffHoliday++;
                    }
                }
            }
            if (fullMedicalHoliday == true)
            {
                foreach (var hol in FullMedicalHoliday())
                {
                    for (var date = hol.StartDate; date <= hol.EndDate; date = date.AddDays(1))
                    {
                        if (date.DayOfWeek != DayOfWeek.Saturday
                            && date.DayOfWeek != DayOfWeek.Sunday)
                            daysOffFullMedical++;
                    }
                }
            }
            if (partialMedicalHoliday == true)
            {
                foreach (var hol in PartialMedicalHoliday())
                {
                    for (var date = hol.StartDate; date <= hol.EndDate; date = date.AddDays(1))
                    {
                        if (date.DayOfWeek != DayOfWeek.Saturday
                            && date.DayOfWeek != DayOfWeek.Sunday)
                            daysOffPartialMedical++;
                    }
                }
            }
            if (notPaidHoliday == true)
            {
                foreach (var hol in NotPaidHoliday())
                {
                    for (var date = hol.StartDate; date <= hol.EndDate; date = date.AddDays(1))
                    {
                        if (date.DayOfWeek != DayOfWeek.Saturday
                            && date.DayOfWeek != DayOfWeek.Sunday)
                            daysOffNotPaid++;
                    }
                }
            }
            if (ooh == true)
            {
                foreach (var objectooh in Ooh())
                {
                    bonusHours += (objectooh.Hours * 2);
                }
            }

            workingHoursThisMonth = businessDaysInMonth * 8;
            hoursWorked = (businessDaysInMonth - daysOffHoliday - daysOffFullMedical - daysOffPartialMedical - daysOffNotPaid) * 8 + Convert.ToInt32(bonusHours);
            hoursPaid = (hoursWorked - Convert.ToInt32(bonusHours)) + daysOffHoliday * 8 + daysOffFullMedical * 8 + daysOffPartialMedical * 0.75 * 8 + bonusHours * 2;
            transport = (float)((float)(businessDaysInMonth - daysOffHoliday - daysOffFullMedical - daysOffPartialMedical - daysOffNotPaid) / (float)businessDaysInMonth) * (float)transport;
            mealTickets = (businessDaysInMonth - daysOffHoliday - daysOffFullMedical - daysOffPartialMedical - daysOffNotPaid);


            return "Ore lucratoare in luna: " + workingHoursThisMonth + " Ore lucrate in luna: " + hoursWorked
                + " Ore platite in luna: " + hoursPaid + " Decont transport: " + transport + " Tichete de masa: " + mealTickets;
        }



        public string Pontaj()
        {
            bool holiday = Holiday().Count > 0;            
            bool ooh = Ooh().Count > 0;

            int daysInMonth = System.DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);                       
            var businessDaysInMonth = 0;     
            for (var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); date < new DateTime(DateTime.Now.Year, DateTime.Now.Month, daysInMonth); date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday
                    && date.DayOfWeek != DayOfWeek.Sunday)
                    businessDaysInMonth++;
            }
            int mealTickets = businessDaysInMonth;
            int workingHoursInMonth = businessDaysInMonth * 8;
            int daysOff = 0;            
            double bonusHours = 0;
            double hoursWorked = 0;
            double hoursPaid = 0;
            double transport = 80.0;


            //if the employee had days off and extra hours worked
            if (holiday && ooh == true)
            {
                //compute days off
                foreach (var hol in Holiday())
                {
                    for (var date = hol.StartDate; date <= hol.EndDate; date = date.AddDays(1))
                    {
                        if (date.DayOfWeek != DayOfWeek.Saturday
                            && date.DayOfWeek != DayOfWeek.Sunday)
                            daysOff++;
                    }
                }
                //compute extra hours worked
                foreach (var objectooh in Ooh())
                {
                    bonusHours += (objectooh.Hours * 2);
                }

                hoursWorked = (businessDaysInMonth - daysOff) * 8 + bonusHours;
                hoursPaid = businessDaysInMonth * 8 + bonusHours * 2;
                transport = (float)((float)(businessDaysInMonth - daysOff) / (float)businessDaysInMonth) * (float)transport;
                mealTickets = mealTickets - daysOff;
            }

            //if the employee had days off but no extra hours worked
            if (holiday == true && ooh==false)
            {
                //compute days off
                foreach (var hol in Holiday())
                {
                    for (var date = hol.StartDate; date <= hol.EndDate; date = date.AddDays(1))
                    {
                        if (date.DayOfWeek != DayOfWeek.Saturday
                            && date.DayOfWeek != DayOfWeek.Sunday)
                            daysOff++;
                    }
                }
                hoursWorked = (businessDaysInMonth - daysOff) * 8;
                hoursPaid = businessDaysInMonth * 8;
                transport = (float)((float)(businessDaysInMonth - daysOff) / (float)businessDaysInMonth) * (float)transport;
                mealTickets = mealTickets - daysOff;
            }

            //if the employee had no days off but extra hours worked
            if (holiday ==false && ooh == true)
            {
               
                //compute extra hours worked
                foreach (var objectooh in Ooh())
                {
                    bonusHours += (objectooh.Hours * 2);
                }

                hoursWorked = businessDaysInMonth * 8 + bonusHours/2;
                hoursPaid = businessDaysInMonth * 8 + bonusHours;                
            }

            //if the employee had no days off and no extra hours worked
            if (holiday&&ooh == false)
            {
                hoursWorked = workingHoursInMonth;
                hoursPaid = workingHoursInMonth;
            }

            return "Ore lucratoare in luna: " + workingHoursInMonth + " Ore lucrate in luna: " + hoursWorked
                + " Ore platite in luna: " + hoursPaid + " Transport: " + transport + " Bonuri de masa: " + mealTickets;
        }

        public ActionResult SendMail()
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("internalapppontaj@gmail.com");
            mail.To.Add("myheroalinabrinza95@gmail.com");
            mail.Subject = "Test Mail";
            mail.Body = PontajV2();

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("internalapppontaj@gmail.com", "SCCpontaj2017$");
            SmtpServer.EnableSsl = true;


            SmtpServer.Send(mail);
            return RedirectToAction("SuccessOOH", "Success");
        }


        // GET: TimeSheetViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSheetViewModel timeSheetViewModel = db.TimeSheetViewModel.Find(id);
            if (timeSheetViewModel == null)
            {
                return HttpNotFound();
            }
            return View(timeSheetViewModel);
        }

        // POST: TimeSheetViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSheetViewModel timeSheetViewModel = db.TimeSheetViewModel.Find(id);
            db.TimeSheetViewModel.Remove(timeSheetViewModel);
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
