using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipOtomasyonu.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.ViewComponents
{
    public class EditViewComponent : ViewComponent
    {
        private readonly Context _context;

        public EditViewComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mail = HttpContext.Session.GetString("email");
            var id = await _context.Users.Where(x => x.Email == mail).Select(x => x.UserId).FirstOrDefaultAsync();
            var user = _context.Users.Find(id);

            return View(user);
        }




    }
}
