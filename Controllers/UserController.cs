using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PersonelTakipOtomasyonu.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.Controllers
{
    public class UserController : Controller
    {
        private readonly Context _context;
        public UserController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userList = _context.Users.Include(x => x.Department).ToList();
            return View(userList);
        }
       public IActionResult Details(int id)
        {
            var userDetails = _context.Users.Where(x => x.UserId == id).Include(x => x.Department).ToList();
            return View(userDetails);
        }
        public IActionResult UserReport(int id)
        {
            var reportList = _context.Reports.Where(x => x.User.UserId == id).Include(x => x.User).ToList();
            var userName = _context.Reports.Where(x => x.User.UserId == id).Include(x => x.User).Select(x=>x.User.FullName).FirstOrDefault();
            ViewBag.userName = userName;
            return View(reportList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> dropdownlist = (from x in _context.Departments.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.DepartmentId.ToString()
                                                 }).ToList();
            ViewBag.list = dropdownlist;
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index","User");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<SelectListItem> dropdownlist = (from x in _context.Departments.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.DepartmentId.ToString()
                                                 }).ToList();
            ViewBag.list = dropdownlist;
            var userId = _context.Users.Find(id);
            return View(userId);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "User");
        }
        public IActionResult Delete(int id)
        {
            var value = _context.Users.Find(id);
            _context.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "User");
        }


    }
}
