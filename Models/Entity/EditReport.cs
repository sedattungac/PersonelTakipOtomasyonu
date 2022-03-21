using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.Models.Entity
{
    public class EditReport
    {
        public int ReportId { get; set; }
        public string Name { get; set; }
        public IFormFile ReportFile { get; set; }
        public DateTime AddDate { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
