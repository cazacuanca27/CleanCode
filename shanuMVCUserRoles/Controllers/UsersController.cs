using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using shanuMVCUserRoles.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace shanuMVCUserRoles.Controllers
{
	[Authorize]
	public class UsersController : Controller
    {
		// GET: Users
        //checks if the logged in user is Admin
		public Boolean isAdminUser()
		{
			if (User.Identity.IsAuthenticated)
			{
				var user = User.Identity;
				ApplicationDbContext context = new ApplicationDbContext();
				var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
				var s = UserManager.GetRoles(user.GetUserId());
				if (s[0].ToString() == "Admin")
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
        //checks if logged in user is employee
        public Boolean isEmployeeUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Employee")
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
        //checks if logged in user is team leader
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

        //checks if logged in user is manager
        public Boolean isManagerUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Manager")
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


        public ActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				var user = User.Identity;
				ViewBag.Name = user.Name;
                

				//	ApplicationDbContext context = new ApplicationDbContext();
				//	var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

				//var s=	UserManager.GetRoles(user.GetUserId());
				ViewBag.displayMenu = "No";
                    

				if (isAdminUser())
				{
					ViewBag.displayMenu = "AdminUser";
				}
                if (isEmployeeUser())
                {
                    ViewBag.displayMenu = "EmployeeUser";
                }
                if (isTeamLeaderUser())
                {
                    ViewBag.displayMenu = "Team Leader";
                }
                if (isManagerUser())
                {
                    ViewBag.displayMenu = "Manager";
                }
                return View();
			}
			else
			{
				ViewBag.Name = "Not Logged IN";
			}


			return View();


		}
	}
}