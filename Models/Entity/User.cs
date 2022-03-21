using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.Models.Entity
{
    public class User 
    {
        [Key]
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Description { get; set; }
        public char Role { get; set; }
        public bool Type { get; set; }


        public virtual Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<PersonalAction> PersonalActions { get; set; }
        public ICollection<Tracking> Trackings { get; set; }
        public ICollection<Report> Reports { get; set; }

    }
}
