using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplicationNetLab.DAL;
using WebApplicationNetLab.ViewModels;

namespace WebApplicationNetLab.Controllers
{
    public class HomeController : Controller
    {
        private EventorerContext db = new EventorerContext();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            IQueryable<EnrollmentDateGroup> data = from student in db.Accounts
                                                   group student by student.CreationDate into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       AccountCount = dateGroup.Count()
                                                   };

            return View(await data.ToListAsync());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}