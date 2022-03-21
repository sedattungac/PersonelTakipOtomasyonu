using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.Models.Entity
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }
        public string Name { get; set; }
        public string ReportFile { get; set; }
        public DateTime AddDate { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
