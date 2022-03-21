using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelTakipOtomasyonu.Models.Entity;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.Controllers
{
    public class ReportController : Controller
    {
        private readonly Context _context;
        public ReportController(Context context)
        {
            _context = context;
        }

        //Admin Paneli
        public IActionResult Index()
        {
            var reporttList = _context.Reports.Include(x => x.User).OrderByDescending(x => x.ReportId).ToList();
            return View(reporttList);
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
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddReport addreport)
        {
            Report report = new Report();
            if (addreport.ReportFile != null)
            {
                var extension = Path.GetExtension(addreport.ReportFile.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Raporlar/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                addreport.ReportFile.CopyTo(stream);
                report.ReportFile = newimagename;
            }
            report.Name = addreport.Name;
            report.UserId = addreport.UserId;
            addreport.AddDate = DateTime.Now;
            report.AddDate = addreport.AddDate;
            _context.Add(report);
            _context.SaveChanges();
            return RedirectToAction("Index", "Report");
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
            var reportId = _context.Reports.Find(id);
            return View(reportId);
        }
        [HttpPost]
        public IActionResult Edit(Report report)
        {
            if (report.ReportFile != null)
            {
                var extension = Path.GetExtension(report.ReportFile);
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Raporlar/");
                var stream = new FileStream(location, FileMode.Create);
                report.ReportFile = extension;
            }

            _context.Update(report);
            _context.SaveChanges();
            return RedirectToAction("Index", "Report");
        }
        public IActionResult Delete(int id)
        {
            var value = _context.Reports.Find(id);
            _context.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "Report");
        }

        //Personel Paneli

    }
}
