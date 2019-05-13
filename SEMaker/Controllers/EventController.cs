using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEMaker.Models;

namespace SEMaker.Controllers
{
    public class EventController : Controller
    {
        DataAccessLayer objevent = new DataAccessLayer();

        private readonly IHostingEnvironment _hostingEnvironment;

        public EventController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize(Roles = "user, admin")]
        public IActionResult Index(string sortOrder,
                                    string currentFilter,
                                    string searchString,
                                    int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AuthorSortParm"] = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";
            ViewData["CitySortParm"] = String.IsNullOrEmpty(sortOrder) ? "city_desc" : "";
            ViewData["PlaceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "places_desc" : "";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["SportSortParm"] = String.IsNullOrEmpty(sortOrder) ? "sport_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var lstEvent = objevent.GetAllEvents();

            if (!String.IsNullOrEmpty(searchString) && searchString != "my")
            {
                lstEvent = lstEvent.Where(s => s.Name.Contains(searchString)
                                       || s.Sport.Contains(searchString)
                                       || s.Author.Contains(searchString)
                                       || s.City.Contains(searchString)
                                       || s.Places.ToString().Contains(searchString)
                                       || s.Date.ToString().Contains(searchString));
            }
            else if (searchString == "my")
            {
                lstEvent = objevent.GetMyEvents(User.Identity.Name);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    lstEvent = lstEvent.OrderByDescending(s => s.Name);
                    break;
                case "author_desc":
                    lstEvent = lstEvent.OrderBy(s => s.Author);
                    break;
                case "city_desc":
                    lstEvent = lstEvent.OrderBy(s => s.City);
                    break;
                case "sport_desc":
                    lstEvent = lstEvent.OrderBy(s => s.Sport);
                    break;
                case "places_desc":
                    lstEvent = lstEvent.OrderBy(s => s.Places);
                    break;
                case "date_desc":
                    lstEvent = lstEvent.OrderBy(s => s.Date);
                    break;
                default:
                    lstEvent = lstEvent.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;

            return View(PaginatedList<Event>.Create(lstEvent, page ?? 1, pageSize));
        }

        [Authorize(Roles = "user, admin")]
        public IActionResult Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Content(User.Identity.Name);
            }
            return Content("не аутентифицирован");
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public IActionResult Create()
        {
            CheckPremium(objevent.GetUserData(User.Identity.Name));
            if (objevent.GetUserData(User.Identity.Name).Premium == 1 || User.IsInRole("admin"))
                return View();
            else
                return RedirectToAction("Index", "Premium", new { area = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Event evnt)
        {
            if (ModelState.IsValid)
            {
                objevent.AddEvent(evnt, User.Identity.Name);
                return RedirectToAction("Index");
            }
            return View(evnt);
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Event evnt = objevent.GetEventData(id);

            if (evnt == null || !evnt.Author.Equals(User.Identity.Name))
            {
                return NotFound();
            }
            return View(evnt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Event evnt)
        {
            CheckPremium(objevent.GetUserData(User.Identity.Name));
            if (objevent.GetUserData(User.Identity.Name).Premium == 1 || User.IsInRole("admin"))
            {
                if (id != evnt.ID)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    objevent.UpdateEvent(evnt);
                    return RedirectToAction("Index");
                }
                return View(evnt);
            }
            else
                return RedirectToAction("Index", "Premium", new { area = "" });
            
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Event evnt = objevent.GetEventData(id);

            if (evnt == null)
            {
                return NotFound();
            }
            return View(evnt);
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Event evnt = objevent.GetEventData(id);

            if (evnt == null || evnt.Author != User.Identity.Name)
            {
                return NotFound();
            }
            return View(evnt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            objevent.DeleteEvent(id);
            return RedirectToAction("Index");
        }

        public IActionResult Apply(int? id)
        {
            var evnt = objevent.GetEventData(id);
            if (User.Identity.IsAuthenticated && evnt.Author != User.Identity.Name && evnt.Places > 0 && !objevent.CheckApplication(User.Identity.Name, id))
            {
                objevent.Apply(id, User.Identity.Name);
            }
            return RedirectToAction("Index");
        }

        public FileResult GetFile(int? id)
        {
            var evnt = objevent.GetEventData(id);
            var applications = objevent.GetApplications(id).ToList();
            
            if (User.Identity.Name != evnt.Author || applications.Count == 0)
            {
                return PhysicalFile(_hostingEnvironment.ContentRootPath + @"\ApplicationsTxt\ban.txt", "text/plain");
            }

            string name = @"ApplicationsTxt\" + User.Identity.Name + ".txt";
            StreamWriter sw = new StreamWriter(name);
            foreach (var user in applications)
            {
                sw.WriteLine(user.Name + " " + user.Surname + " " + user.SecondName + " " + user.BirthDate + " " + user.PhoneNum);
            }

            sw.Close();
            return PhysicalFile(_hostingEnvironment.ContentRootPath + @"\" + name, "text/plain");
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