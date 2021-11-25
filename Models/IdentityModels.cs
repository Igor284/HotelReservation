using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hotel_Reservation_System.Models
{
    
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
           
            return userIdentity;
        }


        public string Names { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }

        

        public virtual List<Stay> Usages =>
            new ApplicationDbContext().Stay.ToList().Where(x => x.GuestId == Id).ToList();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: true)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Stay> Stay { get; set; }
    }
}