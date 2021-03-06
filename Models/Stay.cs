using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hotel_Reservation_System.Models
{
    public class Stay 
    {
        public int Id { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public string GuestId { get; set; }
        [Required]
        [Range(24, 336)]
        public int Hours { get; set; }
        [Required]
        public DateTime Checkin { get; set; }
        public DateTime Date { get; set; }

        [NotMapped]
        public virtual Room Room => new ApplicationDbContext().Rooms.Find(RoomId);
        [NotMapped]
        public virtual ApplicationUser Guest => new ApplicationDbContext().Users.Find(GuestId);

        [NotMapped]
        public virtual string[] Types => new string[] { "First Class", "2nd Class", "Third Class" };
        [NotMapped]
        public virtual List<ApplicationUser> Guests => getGuestUsers();

        private List<ApplicationUser> getGuestUsers()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<ApplicationUser> users = db.Users.ToList();
            List<ApplicationUser> newUsers = new List<ApplicationUser>();

            var role = db.Roles.Where(m => m.Name == "Guest").FirstOrDefault();

            foreach (var item in users)
            {
                if (item.Roles.FirstOrDefault().RoleId == role.Id)
                {
                    newUsers.Add(item);
                }
            }
            return newUsers;

        }
        [NotMapped]
        public virtual DateTime CheckOut => Checkin.AddHours(Hours);
    }
}