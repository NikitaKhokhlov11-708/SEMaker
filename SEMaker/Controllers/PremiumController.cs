using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEMaker.Models;

namespace SEMaker.Controllers
{
    public class PremiumController : Controller
    {
        [Authorize(Roles = "user, admin")]
        public IActionResult Index()
        {
            DataAccessLayer objevent = new DataAccessLayer();
            User user = objevent.GetUserData(User.Identity.Name);
            CheckPremium(user);
            return View(user);
        }

        [Authorize(Roles = "user, admin")]
        public IActionResult Buy()
        {
            DataAccessLayer objevent = new DataAccessLayer();
            User user = objevent.GetUserData(User.Identity.Name);
            CheckPremium(user);
            user.Premium = 1;
            user.EndDate = DateTime.Now.AddMonths(1);
            objevent.UpdateUser(user);
            return RedirectToAction("Index");
        }

        private void CheckPremium(User user)
        {
            DataAccessLayer objevent = new DataAccessLayer();
            if (user.EndDate < DateTime.Now)
            {
                user.Premium = 0;
                objevent.UpdateUser(user);
            }
        }
    }
}