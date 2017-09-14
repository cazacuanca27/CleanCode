using shanuMVCUserRoles.Models;
using System.Web.Mvc;
using System.Linq;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace shanuMVCUserRoles.Controllers
{
    public class HomeController : Controller
	{
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

        public Boolean isTeamLeaderUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Team Leader")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public ActionResult TeamLeaderStatistics()
        {
            var teamLeaderEmployees = Enumerable.Empty<ProfileViewModel>().AsQueryable();

            int holidayRequestsInPending = 0;
            int holidayRequestsApproved = 0;
            int holidayRequests = 0;

            int oohRequestsInPending = 0;
            int oohRequestsApproved = 0;
            int oohRequests = 0;

            if (User.Identity.IsAuthenticated)
            {
                if (isTeamLeaderUser() == true)
                {  

                    var user = User.Identity.Name;

                    string team = (from b in db.ProfileViewModel
                                   where b.UserName == user
                                   select b.Team).Single();
                    string email = (from b in db.ProfileViewModel
                                    where b.UserName == user
                                    select b.Email).Single();
                    
                    teamLeaderEmployees = from b in db.ProfileViewModel
                                              join c in db.Users on b.Email equals c.Email
                                              where b.Team == team
                                              select b;
                    //statistics for holiday requests of the logged in teamleader
                    holidayRequestsInPending = (from b in db.AspNetHolidays
                                                where (b.StartDate.Month.Equals(DateTime.Now.Month) && b.TLEmail.Equals(email)
                                                && b.Flag.Equals(false))
                                                select b).Count();
                    holidayRequestsApproved = (from b in db.AspNetHolidays
                                                where (b.StartDate.Month.Equals(DateTime.Now.Month) && b.TLEmail.Equals(email)
                                                && b.Flag.Equals(true))
                                                select b).Count();
                    holidayRequests = (from b in db.AspNetHolidays
                                               where (b.StartDate.Month.Equals(DateTime.Now.Month) && b.TLEmail.Equals(email))
                                               select b).Count();
                    //place the values to a viewbag
                    ViewBag.holidayRequestsInPending = holidayRequestsInPending;
                    ViewBag.holidayRequestsApproved = holidayRequestsApproved;
                    ViewBag.holidayRequests = holidayRequests;

                    //statistics for ooh requests of the logged in teamleader
                    oohRequestsInPending = (from b in db.OOHRequestViewModel
                                            where (b.Day.Month.Equals(DateTime.Now.Month) && b.TeamLeaderEmail.Equals(email)
                                            && b.Flag.Equals(false))
                                            select b).Count();
                    oohRequestsApproved = (from b in db.OOHRequestViewModel
                                            where (b.Day.Month.Equals(DateTime.Now.Month) && b.TeamLeaderEmail.Equals(email)
                                            && b.Flag.Equals(true))
                                            select b).Count();
                    oohRequests = (from b in db.OOHRequestViewModel
                                           where (b.Day.Month.Equals(DateTime.Now.Month) && b.TeamLeaderEmail.Equals(email))
                                           select b).Count();
                    //place the values to a viewbag
                    ViewBag.oohRequestsInPending = oohRequestsInPending;
                    ViewBag.oohRequestsApproved = oohRequestsApproved;
                    ViewBag.oohRequests = oohRequests;
                }
                          
            }
            return View(teamLeaderEmployees.ToList());

        }
    }
}