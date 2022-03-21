using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu.Models.Entity
{
    public class ActionType
    {
        [Key]
        public int ActionTypeId { get; set; }
        public string Name { get; set; }
        public ICollection<PersonalAction> PersonalActions { get; set; }
    }
}
