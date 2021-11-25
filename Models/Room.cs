using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hotel_Reservation_System.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        public bool Isactive { get; set; }
        public string Location { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

        [NotMapped]
        public virtual string[] Types => new string[] {"First Class", "2nd Class", "Third Class"};

    }
}