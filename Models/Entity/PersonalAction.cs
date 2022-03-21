using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.Models.Entity
{
    public class PersonalAction
    {
        [Key]
        public int PersonalActionId { get; set; }
        public DateTime ActionTime { get; set; }
        //public DateTime EndTime { get; set; }
        //public string Action { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual ActionType ActionType { get; set; }
        public int ActionTypeId { get; set; }
    }
}
