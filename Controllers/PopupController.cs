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
    public class PopupController : Controller
    {
        private readonly Context _context;
        public PopupController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
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

        public IActionResult GetUserList()
        {
            //var liste = _context.Users.Include(x => x.Department).Select(x => new User()

            List <User> liste = _context.Users.Include(x => x.Department).Select(x => new User()
            {
                UserId = x.UserId,
                FullName = x.FullName,
                Email = x.Email,
                Telephone = x.Telephone
            }).ToList();
            var deger = liste.Count();
            return Json(liste);


        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            var userValue = _context.Users.Find(user.UserId);
            user.FullName = userValue.FullName;
            user.Email = userValue.Email;
            user.Password = userValue.Password;
            user.Address = userValue.Address;
            user.Telephone = userValue.Telephone;
            user.Description = userValue.Description;
            user.DepartmentId = userValue.DepartmentId;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult Delete(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetEditDeleteUser(int id)
        {
            var model = _context.Users.FirstOrDefault(x => x.UserId == id);
            string value = "";
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value);
        }

    }
}
