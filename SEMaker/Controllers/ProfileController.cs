using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEMaker.Models;

namespace SEMaker.Controllers
{
    public class ProfileController : Controller
    {
        DataAccessLayer objevent = new DataAccessLayer();
        public IActionResult Index()
        {
            var user = objevent.GetUserData(User.Identity.Name);
            return View(user);
        }
    }
}