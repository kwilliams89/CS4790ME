using System.Net;
using System.Web.Mvc;
using CS4790ME.Models;

namespace CS4790ME.Controllers
{
    public class ServiceEventController : Controller
    {
        private BasicVehicleDbContext db = new BasicVehicleDbContext();

        // GET: ServiceEvent
        public ActionResult Index()
        {
            return View(Repository.getServiceEvents());
        }

        // GET: ServiceEvent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceEvent serviceEvent = Repository.getServiceEvent(id);
            if (serviceEvent == null)
            {
                return HttpNotFound();
            }
            return View(serviceEvent);
        }

        // GET: ServiceEvent/Create
        public ActionResult Create(string vin)

        {
            ServiceEvent myServiceEvent = new ServiceEvent();
            myServiceEvent.Vin = vin;
            return View(myServiceEvent);
        }

        // POST: ServiceEvent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ServiceDate,Vin,ServiceType,ServiceMileage,ServiceAmount")] ServiceEvent serviceEvent, string vin)
        {
            if (ModelState.IsValid)
            {
                serviceEvent.Vin = vin;
                Repository.createServiceEvent(serviceEvent);
                return RedirectToAction("Index");
            }

            return View(serviceEvent);
        }

        // GET: ServiceEvent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceEvent serviceEvent = Repository.getServiceEvent(id);
            if (serviceEvent == null)
            {
                return HttpNotFound();
            }
            return View(serviceEvent);
        }

        // POST: ServiceEvent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ServiceDate,Vin,ServiceType,ServiceMileage,ServiceAmount")] ServiceEvent serviceEvent)
        {
            if (ModelState.IsValid)
            {
                Repository.editServiceEvent(serviceEvent);
                return RedirectToAction("Index");
            }
            return View(serviceEvent);
        }

        // GET: ServiceEvent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceEvent serviceEvent = Repository.getServiceEvent(id);
            if (serviceEvent == null)
            {
                return HttpNotFound();
            }
            return View(serviceEvent);
        }

        // POST: ServiceEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceEvent serviceEvent = Repository.getServiceEvent(id);
            Repository.deleteServiceEvent(serviceEvent);
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