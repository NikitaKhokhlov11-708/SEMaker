using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEMaker.Models;

namespace SEMaker.Controllers
{
    public class ApplicationsController : Controller
    {
        EventDataAccessLayer objevent = new EventDataAccessLayer();

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
            var lstEvent = objevent.GetMyApplications(User.Identity.Name);

            if (!String.IsNullOrEmpty(searchString))
            {
                lstEvent = lstEvent.Where(s => s.Name.Contains(searchString)
                                       || s.Sport.Contains(searchString)
                                       || s.Author.Contains(searchString)
                                       || s.City.Contains(searchString));
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

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Event evnt = objevent.GetEventData(id);

            if (evnt == null)
                if (evnt == null)
                {
                    return NotFound();
                }
            return View(evnt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            objevent.RemoveApplication(id, User.Identity.Name);
            return RedirectToAction("Index");
        }
    }
}