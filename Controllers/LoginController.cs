using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonelTakipOtomasyonu.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.Controllers
{
   
    public class LoginController : Controller
    {
        private readonly Context _context;
        public LoginController(Context context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        //public IActionResult Login(string email, string password)
        public IActionResult Login(User p)
        {
            //var user = _context.Users.FirstOrDefault(w => w.Email.Equals(email) && w.Password.Equals(password));
            var user = _context.Users.FirstOrDefault(x => x.Email == p.Email && x.Password == p.Password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("id", user.UserId);
                HttpContext.Session.SetString("email", user.Email);
                HttpContext.Session.SetString("fullname", user.FullName);
                HttpContext.Session.SetString("type", user.Type.ToString());
                return RedirectToAction("Index", "Welcome");
            }
            else
            {
                ViewBag.alert = "Hatalı Giriş! Lütfen tekrar deneyiniz!";

            }
            return View("Index");
            //return RedirectToAction("Index", "Login");

        }
        [AllowAnonymous]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
