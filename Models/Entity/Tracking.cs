using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.Models.Entity
{
    //Giriş-Çıkış İşlemleri Modeli
    public class Tracking
    {
        [Key]
        public int TrackingId { get; set; }
        public DateTime LogInTime { get; set; }
        public DateTime LogOutTime { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
