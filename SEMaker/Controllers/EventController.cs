using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEMaker.Models;

namespace SEMaker.Controllers
{
    public class EventController : Controller
    {
        DataAccessLayer objevent = new DataAccessLayer();

        public IActionResult Index(string sortOrder,
                                    string currentFilter,
                                    string searchString,
                                    int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AuthorSortParm"] = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";
            ViewData["CitySortParm"] = String.IsNullOrEmpty(sortOrder) ? "city_desc" : "";
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
                                       || s.City.Contains(searchString));
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
                default:
                    lstEvent = lstEvent.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;

            return View(PaginatedList<Event>.Create(lstEvent, page ?? 1, pageSize));
        }

        public IActionResult Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Content(User.Identity.Name);
            }
            return Content("не аутентифицирован");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
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
        public IActionResult Edit(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Event evnt)
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

        [HttpGet]
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
            if (User.Identity.IsAuthenticated && evnt.Author != User.Identity.Name && !objevent.CheckApplication(User.Identity.Name, id))
            {
                objevent.Apply(id, User.Identity.Name);
            }
            return RedirectToAction("Index");
        }
    }
}