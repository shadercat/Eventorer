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
    public class AccountsController : Controller
    {
        private EventorerContext db = new EventorerContext();

        // GET: Accounts
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var accounts = from s in db.Accounts
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(s => s.Nickname.Contains(searchString)
                || s.Email.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    accounts = accounts.OrderByDescending(s => s.Nickname);
                    break;
                case "Date":
                    accounts = accounts.OrderBy(s => s.CreationDate);
                    break;
                case "date_desc":
                    accounts = accounts.OrderByDescending(s => s.CreationDate);
                    break;
                default:
                    accounts = accounts.OrderBy(s => s.Nickname);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(await accounts.ToPagedListAsync(pageNumber, pageSize));
        }

        // GET: Accounts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = await db.Accounts.FindAsync(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            var model = new Account() { CreationDate = DateTime.Now };
            return View(model);
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Email, Nickname, CreationDate")] Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Accounts.Add(account);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = await db.Accounts.FindAsync(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Email,Nickname,CreationDate")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = await db.Accounts.FindAsync(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Account account = await db.Accounts.FindAsync(id);
            db.Accounts.Remove(account);
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
