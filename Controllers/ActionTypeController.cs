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
    public class ActionTypeController : Controller
    {
        private readonly Context _context;
        public ActionTypeController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var typeList = _context.ActionTypes.ToList();
            return View(typeList);
        }
        public IActionResult ActionList(int id)
        {
            var actionList = _context.PersonalActions.Where(x => x.ActionType.ActionTypeId == id).Include(x => x.ActionType).ToList();
            var typeName = _context.PersonalActions.Where(x => x.ActionType.ActionTypeId == id).Include(x => x.ActionType).Select(x => x.ActionType.Name).FirstOrDefault();
            ViewBag.typeName = typeName;
            return View(actionList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ActionType actionType)
        {
            _context.Add(actionType);
            _context.SaveChanges();
            return RedirectToAction("Index", "ActionType");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            var userId = _context.ActionTypes.Find(id);
            return View(userId);
        }
        [HttpPost]
        public IActionResult Edit(ActionType actionType)
        {
            _context.Update(actionType);
            _context.SaveChanges();
            return RedirectToAction("Index", "ActionType");
        }
        public IActionResult Delete(int id)
        {
            var value = _context.ActionTypes.Find(id);
            _context.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "ActionType");
        }
    }
}
