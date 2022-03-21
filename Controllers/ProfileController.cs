using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipOtomasyonu.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.Controllers
{
    public class ProfileController : Controller
    {
        private readonly Context _context;

        public ProfileController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mail = HttpContext.Session.GetString("email");
            var session = _context.Users.Where(x => x.Email == mail).Select(x => x.UserId).FirstOrDefault();
            var userName = _context.Users.Where(x => x.Email == mail).Select(x => x.FullName).FirstOrDefault();
            ViewBag.username = userName;
            var userDep = _context.Users.Where(x => x.Email == mail).Include(x => x.Department).Select(x => x.Department.Name).FirstOrDefault();
            ViewBag.userDep = userDep;
            var report = _context.Reports.Include(x => x.User).Where(x => x.User.Email == mail).Count();
            ViewBag.report = report;
            var lastAction = _context.PersonalActions.Include(x => x.User).Where(x => x.User.Email == mail).Include(x => x.ActionType).OrderByDescending(x => x.PersonalActionId).Select(x => x.ActionType.Name).FirstOrDefault();
            ViewBag.lastAction = lastAction;
            var customer = _context.PersonalActions.Include(x => x.User).Where(x => x.User.Email == mail).Where(x => x.ActionTypeId == 3).Count();
            ViewBag.customer = customer;
            var value = _context.PersonalActions.Include(x => x.User).Where(x => x.User.Email == mail).Include(x => x.ActionType).OrderByDescending(x => x.PersonalActionId).ToList();
            return View(value);
        }
        public IActionResult _Edit()
        {
            var mail = HttpContext.Session.GetString("email");
            var id = _context.Users.Where(x => x.Email == mail).Select(x => x.UserId).FirstOrDefault();
            var user = _context.Users.Find(id);

            return PartialView("_Edit", user);

        }


        [HttpPost]
        public IActionResult Edit(User user)
        {
            var userValue = _context.Users.Find(user.UserId);
            userValue.FullName = user.FullName;
            userValue.Email = user.Email;
            userValue.Address = user.Address;
            userValue.Description = user.Description;
            userValue.Password = user.Password;
            _context.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
