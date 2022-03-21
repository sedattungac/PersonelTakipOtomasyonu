using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipOtomasyonu.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace PersonelTakipOtomasyonu.Controllers
{
    public class PersonalReportController : Controller
    {
        private readonly Context _context;
        public PersonalReportController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var mail = HttpContext.Session.GetString("email");
            var session = _context.Users.Where(x => x.Email == mail).Select(x => x.UserId).FirstOrDefault();
            var value = _context.Reports.Include(x => x.User).Where(x => x.User.Email == mail).OrderByDescending(x => x.ReportId).ToList();
            return View(value);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var mail = HttpContext.Session.GetString("email");
            var session = _context.Users.Where(x => x.Email == mail).Select(x => x.UserId).FirstOrDefault();
            ViewBag.user = session;
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
            return RedirectToAction("Index", "PersonalReport");
        }
        public IActionResult Delete(int id)
        {
            var value = _context.Reports.Find(id);
            _context.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "PersonalReport");
        }
    }
}
