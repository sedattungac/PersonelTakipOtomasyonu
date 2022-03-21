using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipOtomasyonu.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly Context _context;
        public DepartmentController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var departmentList = _context.Departments.ToList();
            return View(departmentList);
        }
        public IActionResult DepartmentUser(int id)
        {
            var userList = _context.Users.Where(x => x.Department.DepartmentId == id).Include(x => x.Department).ToList();
            var departmentName = _context.Users.Where(x => x.Department.DepartmentId == id).Include(x => x.Department).Select(x => x.Department.Name).FirstOrDefault();
            ViewBag.departmentName = departmentName;
            return View(userList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            _context.Add(department);
            _context.SaveChanges();
            return RedirectToAction("Index", "Department");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = _context.Departments.Find(id);
            return View(userId);
        }
        [HttpPost]
        public IActionResult Edit(Department department)
        {
            _context.Update(department);
            _context.SaveChanges();
            return RedirectToAction("Index", "Department");
        }
        public IActionResult Delete(int id)
        {
            var value = _context.Departments.Find(id);
            _context.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "Department");
        }
    }
}
