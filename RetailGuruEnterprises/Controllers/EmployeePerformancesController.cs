using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccess;

namespace RetailGuruEnterprises.Controllers
{
    public class EmployeePerformancesController : Controller
    {
        private RetailGuruEntities db = new RetailGuruEntities();

        // GET: EmployeePerformances
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var employeePerformances = db.EmployeePerformances.Include(e => e.EmployeeTable);
            return View(employeePerformances.ToList());
        }

        // GET: EmployeePerformances/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeePerformance employeePerformance = db.EmployeePerformances.Find(id);
            if (employeePerformance == null)
            {
                return HttpNotFound();
            }
            return View(employeePerformance);
        }

        // GET: EmployeePerformances/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "Name");
            return View();
        }

        // POST: EmployeePerformances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeePerformance employeePerformance)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.EmployeePerformances.Add(employeePerformance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "Name", employeePerformance.EmployeeID);
            return View(employeePerformance);
        }

        // GET: EmployeePerformances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeePerformance employeePerformance = db.EmployeePerformances.Find(id);
            if (employeePerformance == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "Name", employeePerformance.EmployeeID);
            return View(employeePerformance);
        }

        // POST: EmployeePerformances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeePerformance employeePerformance)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(employeePerformance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "Name", employeePerformance.EmployeeID);
            return View(employeePerformance);
        }

        // GET: EmployeePerformances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeePerformance employeePerformance = db.EmployeePerformances.Find(id);
            if (employeePerformance == null)
            {
                return HttpNotFound();
            }
            return View(employeePerformance);
        }

        // POST: EmployeePerformances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            EmployeePerformance employeePerformance = db.EmployeePerformances.Find(id);
            db.EmployeePerformances.Remove(employeePerformance);
            db.SaveChanges();
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
