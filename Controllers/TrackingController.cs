using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipOtomasyonu.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.Controllers
{
    public class TrackingController : Controller
    {
        private readonly Context _context;
        public TrackingController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var trackingList = _context.Trackings.Include(x => x.User).OrderByDescending(x=>x.TrackingId).ToList();
            return View(trackingList);
        }
    }
}
