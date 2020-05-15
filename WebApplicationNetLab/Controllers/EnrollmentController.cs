using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationNetLab.DAL;
using WebApplicationNetLab.Models;
using PagedList.EntityFramework;

namespace WebApplicationNetLab.Controllers
{
    public class EnrollmentController : Controller
    {
        private EventorerContext db = new EventorerContext();

        // GET: Enrollment
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var enrollments = db.Enrollments.Include(e => e.Account).Include(e => e.Event);
            if (!String.IsNullOrEmpty(searchString))
            {
                enrollments = enrollments.Where(s => s.Account.Email.Contains(searchString)
                || s.Event.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "email_desc":
                    enrollments = enrollments.OrderByDescending(s => s.Account.Email);
                    break;
                case "Title":
                    enrollments = enrollments.OrderBy(s => s.Event.Title);
                    break;
                case "title_desc":
                    enrollments = enrollments.OrderByDescending(s => s.Event.Title);
                    break;
                default:
                    enrollments = enrollments.OrderBy(s => s.Account.Email);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(await enrollments.ToPagedListAsync(pageNumber, pageSize));
        }

        // GET: Enrollment/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollment/Create
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.Accounts, "ID", "Email");
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Title");
            Enrollment enrollment = new Enrollment();
            return View(enrollment);
        }

        // POST: Enrollment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EventID,AccountID,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.Accounts, "ID", "Email", enrollment.AccountID);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Title", enrollment.EventID);
            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "ID", "Email", enrollment.AccountID);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Title", enrollment.EventID);
            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollmentID,EventID,AccountID,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "ID", "Email", enrollment.AccountID);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Title", enrollment.EventID);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            db.Enrollments.Remove(enrollment);
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
