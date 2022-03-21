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
    public class PersonalActionController : Controller
    {
        private readonly Context _context;
        public PersonalActionController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var personalActions = _context.PersonalActions.Include(x => x.User).Include(x=>x.ActionType).OrderByDescending(x=>x.PersonalActionId).ToList();
            return View(personalActions);
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> dropdownlist = (from x in _context.Users.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.FullName,
                                                     Value = x.UserId.ToString()
                                                 }).ToList();
            ViewBag.list = dropdownlist;
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
            return RedirectToAction("Index", "PersonalAction");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<SelectListItem> dropdownlist = (from x in _context.Users.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.FullName,
                                                     Value = x.UserId.ToString()
                                                 }).ToList();
            ViewBag.list = dropdownlist;
            List<SelectListItem> dropdownlist1 = (from x in _context.ActionTypes.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Name,
                                                      Value = x.ActionTypeId.ToString()
                                                  }).ToList();
            ViewBag.list1 = dropdownlist1;
            var userId = _context.Users.Find(id);
            return View(userId);
        }
        [HttpPost]
        public IActionResult Edit(PersonalAction personalAction)
        {
            _context.Update(personalAction);
            _context.SaveChanges();
            return RedirectToAction("Index", "PersonalAction");
        }
        public IActionResult Delete(int id)
        {
            var value = _context.PersonalActions.Find(id);
            _context.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "PersonalAction");
        }

    }
}
