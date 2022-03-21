using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelTakipOtomasyonu.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.Controllers
{
    public class ActionController : Controller
    {
        private readonly Context _context;
        public ActionController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mail = HttpContext.Session.GetString("email");
            var session = _context.Users.Where(x => x.Email == mail).Select(x => x.UserId).FirstOrDefault();
            var value = _context.PersonalActions.Include(x => x.User).Include(x => x.ActionType).Where(x => x.UserId == session).OrderByDescending(x=>x.ActionTypeId).ToList();
            return View(value);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var mail = HttpContext.Session.GetString("email");
            var session = _context.Users.Where(x => x.Email == mail).Select(x => x.UserId).FirstOrDefault();
            ViewBag.user = session;
            List<SelectListItem> dropdownlist1 = (from x in _context.ActionTypes.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Name,
                                                      Value = x.ActionTypeId.ToString()
                                                  }).ToList();
            ViewBag.list1 = dropdownlist1;
            return View();
        }
        [HttpPost]
        public IActionResult Create(PersonalAction personalAction)
        {
            personalAction.ActionTime = DateTime.Now;
            _context.Add(personalAction);
            _context.SaveChanges();
            return RedirectToAction("Index", "Action");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var mail = HttpContext.Session.GetString("email");
            var session = _context.Users.Where(x => x.Email == mail).Select(x => x.UserId).FirstOrDefault();
            ViewBag.user = session;
            List<SelectListItem> dropdownlist1 = (from x in _context.ActionTypes.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Name,
                                                      Value = x.ActionTypeId.ToString()
                                                  }).ToList();
            ViewBag.list1 = dropdownlist1;
            var userId = _context.PersonalActions.Find(id);
            return View(userId);
        }
        [HttpPost]
        public IActionResult Edit(PersonalAction personalAction)
        {
            _context.Update(personalAction);
            _context.SaveChanges();
            return RedirectToAction("Index", "Action");
        }
        public IActionResult Delete(int id)
        {
            var value = _context.PersonalActions.Find(id);
            _context.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "Action");
        }
    }
}
