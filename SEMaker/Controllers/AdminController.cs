using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SEMaker.Models;
using SEMaker.ViewModels;

namespace SEMaker.Controllers
{
    public class AdminController : Controller
    {
        DataAccessLayer objevent = new DataAccessLayer();

        private readonly IHostingEnvironment _hostingEnvironment;

        public AdminController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index(string sortOrder,
                                    string currentFilter,
                                    string searchString,
                                    int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["SurnameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "surname_desc" : "";
            ViewData["SecondNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "secondname_desc" : "";
            ViewData["LoginSortParm"] = String.IsNullOrEmpty(sortOrder) ? "login_desc" : "";
            ViewData["PasswordSortParm"] = String.IsNullOrEmpty(sortOrder) ? "password_desc" : "";
            ViewData["BirthdateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "birthdate_desc" : "";
            ViewData["PhoneNumSortParm"] = String.IsNullOrEmpty(sortOrder) ? "phone_desc" : "";
            ViewData["RoleIdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "roleid_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var lstUsers = objevent.GetAllUsers();

            if (!String.IsNullOrEmpty(searchString))
            {
                lstUsers = lstUsers.Where(s => s.Name.Contains(searchString)
                                       || s.Surname.Contains(searchString)
                                       || s.SecondName.Contains(searchString)
                                       || s.Login.Contains(searchString)
                                       || s.Password.Contains(searchString)
                                       || s.PhoneNum.Contains(searchString)
                                       || s.RoleId.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    lstUsers = lstUsers.OrderByDescending(s => s.Name);
                    break;
                case "surname_desc":
                    lstUsers = lstUsers.OrderBy(s => s.Surname);
                    break;
                case "secondname_desc":
                    lstUsers = lstUsers.OrderBy(s => s.SecondName);
                    break;
                case "login_desc":
                    lstUsers = lstUsers.OrderBy(s => s.Login);
                    break;
                case "passsword_desc":
                    lstUsers = lstUsers.OrderBy(s => s.Password);
                    break;
                case "birthdate_desc":
                    lstUsers = lstUsers.OrderBy(s => s.BirthDate);
                    break;
                case "phone_desc":
                    lstUsers = lstUsers.OrderBy(s => s.PhoneNum);
                    break;
                case "roleid_desc":
                    lstUsers = lstUsers.OrderBy(s => s.RoleId);
                    break;
            }

            int pageSize = 3;

            return View(PaginatedList<User>.Create(lstUsers, page ?? 1, pageSize));
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = objevent.GetUserData(model.Login);
                if (user == null)
                {
                    user = new User
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        SecondName = model.SecondName,
                        BirthDate = model.BirthDate,
                        Login = model.Login,
                        Password = model.Password,
                        PhoneNum = model.PhoneNum,
                        RoleId = 1,
                        Role = objevent.GetUserRole(1)
                    };

                    // добавляем пользователя в бд
                    objevent.AddUser(user);

                    return RedirectToAction("Index", "Admin");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string login)
        {
            if (login == null)
            {
                return NotFound();
            }
            User usr = objevent.GetUserData(login);

            if (usr == null)
            {
                return NotFound();
            }
            return View(usr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string login, [Bind]User usr)
        {
            if (login != usr.Login)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objevent.UpdateUser(usr);
                return RedirectToAction("Index", "Admin");
            }
            return View(usr);
        }

        [HttpGet]
        public IActionResult Delete(string login)
        {
            if (login == null)
            {
                return NotFound();
            }
            User usr = objevent.GetUserData(login);

            if (usr == null)
            {
                return NotFound();
            }
            return View(usr);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string login)
        {
            objevent.DeleteUser(login);
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult Details(string login)
        {
            if (login == null)
            {
                return NotFound();
            }
            User usr = objevent.GetUserData(login);

            if (usr == null)
            {
                return NotFound();
            }
            return View(usr);
        }
    }
}