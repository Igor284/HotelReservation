using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hotel_Reservation_System.Models;

namespace Hotel_Reservation_System.Controllers
{
    [Authorize]
    public class StayController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stay
        public async Task<ActionResult> Index()
        {
            return View(await db.Stay.ToListAsync());
        }

        // GET: Stay/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stay stay = await db.Stay.FindAsync(id);
            if (stay == null)
            {
                return HttpNotFound();
            }
            return View(stay);
        }

        // GET: Stay/Create
        public ActionResult Create()
        {
            return View(new Stay());
        }

        // POST: Stay/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,RoomId,GuestId,Hours,Checkin")] Stay stay)
        {
            if (stay != null)
            {
                stay.Date = DateTime.Now;
            }
            if (ModelState.IsValid)
            {
                List<Stay> usages = db.Stay.ToList().Where(x => x.RoomId == stay.RoomId).ToList();
                foreach (var item in usages)
                {
                    if ((item.Checkin >= stay.Checkin && item.Checkin <= stay.CheckOut) ||
                        item.Checkin.AddHours(stay.Hours) >= stay.Checkin)
                    {
                        ModelState.AddModelError("", "Room is not Availble!");
                        return View(stay);
                    }
                }


                db.Stay.Add(stay);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(stay);
        }

        // GET: Stay/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stay stay = await db.Stay.FindAsync(id);
            if (stay == null)
            {
                return HttpNotFound();
            }
            return View(stay);
        }

        // POST: Stay/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,RoomId,GuestId,Isactive,Date")] Stay stay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stay).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stay);
        }

        // POST: Stay/Rooms

        public async Task<JsonResult> Rooms(string Type)
        {
            if (!string.IsNullOrEmpty(Type))
            {
                List<Room> rooms = await db.Rooms.ToListAsync();
                rooms = rooms.ToList().Where(x => x.Type.Trim().ToLower() ==
                            Type.Trim().ToLower()).ToList();
                return Json(new { Rooms = rooms });
            }
            return Json(new { Rooms = new List<Room>() });
        }

        // GET: Stay/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stay stay = await db.Stay.FindAsync(id);
            if (stay == null)
            {
                return HttpNotFound();
            }
            return View(stay);
        }

        // POST: Stay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Stay stay = await db.Stay.FindAsync(id);
            db.Stay.Remove(stay);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
