﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SEMaker.ViewModels; // пространство имен моделей RegisterModel и LoginModel
using SEMaker.Models; // пространство имен UserContext и класса User
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SEMaker.Controllers
{
    public class AccountController : Controller
    {

        DataAccessLayer objevent = new DataAccessLayer();
        

        //+
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //+
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = objevent.GetUserData(model.Login);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    objevent.AddUser(new User
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        SecondName = model.SecondName,
                        BirthDate = model.BirthDate,
                        Login = model.Login,
                        Password = model.Password,
                        PhoneNum = model.PhoneNum,
                        RoleId = 0
                    });

                    var userRole = objevent.GetUserRole(0);
                    if (userRole != null)
                        user.Role = userRole;

                    await Authenticate(model.Login); // аутентификация

                    return RedirectToAction("Index", "Event", new { area = "" });
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        //+
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //+
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = objevent.GetUserData(model.Login);
               
                if (user != null)
                {
                    await Authenticate(model.Login); // аутентификация

                    return RedirectToAction("Index", "Event", new { area = "" });
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Index()
        {
            var user = objevent.GetUserData(User.Identity.Name);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var user = objevent.GetUserData(User.Identity.Name);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.SecondName = model.SecondName;
                    user.BirthDate = model.BirthDate;
                    user.Password = model.Password;
                    user.PhoneNum = model.PhoneNum;
                    objevent.UpdateUser(user);
                    return RedirectToAction("Index", "Event", new { area = "" });
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}