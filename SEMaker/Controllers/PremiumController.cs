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
        DataAccessLayer objevent = new DataAccessLayer();

        [Authorize(Roles = "user, admin")]
        public IActionResult Index()
        {
            User user = objevent.GetUserData(User.Identity.Name);
            CheckPremium(user);
            return View(user);
        }

        [Authorize(Roles = "user, admin")]
        public IActionResult Buy()
        {
            User user = objevent.GetUserData(User.Identity.Name);
            CheckPremium(user);
            if (user.Premium == 0)
            {
                user.Premium = 1;
                user.EndDate = DateTime.Now.AddMonths(1);
            }
            else
            {
                user.EndDate = user.EndDate.AddMonths(1);
            }
            
            objevent.UpdateUser(user);
            return RedirectToAction("Index");
        }

        private void CheckPremium(User user)
        {
            if (user.EndDate < DateTime.Now)
            {
                user.Premium = 0;
                objevent.UpdateUser(user);
            }
        }
    }
}