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
    public class EmployeeAttendanceTablesController : Controller
    {
        private RetailGuruEntities db = new RetailGuruEntities();

        // GET: EmployeeAttendanceTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var employeeAttendanceTables = db.EmployeeAttendanceTables.Include(e => e.EmployeeTable);
            return View(employeeAttendanceTables.ToList());
        }

        // GET: EmployeeAttendanceTables/Details/5
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
            EmployeeAttendanceTable employeeAttendanceTable = db.EmployeeAttendanceTables.Find(id);
            if (employeeAttendanceTable == null)
            {
                return HttpNotFound();
            }
            return View(employeeAttendanceTable);
        }

        // GET: EmployeeAttendanceTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Employee_ID = new SelectList(db.EmployeeTables, "EmployeeID", "Name");
            return View();
        }

        // POST: EmployeeAttendanceTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeAttendanceID,Employee_ID,AttendDate,ComingTime,ClosingTime")] EmployeeAttendanceTable employeeAttendanceTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.EmployeeAttendanceTables.Add(employeeAttendanceTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_ID = new SelectList(db.EmployeeTables, "EmployeeID", "Name", employeeAttendanceTable.Employee_ID);
            return View(employeeAttendanceTable);
        }

        // GET: EmployeeAttendanceTables/Edit/5
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
            EmployeeAttendanceTable employeeAttendanceTable = db.EmployeeAttendanceTables.Find(id);
            if (employeeAttendanceTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_ID = new SelectList(db.EmployeeTables, "EmployeeID", "Name", employeeAttendanceTable.Employee_ID);
            return View(employeeAttendanceTable);
        }

        // POST: EmployeeAttendanceTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeAttendanceTable employeeAttendanceTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
  
            if (ModelState.IsValid)
            {
                db.Entry(employeeAttendanceTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_ID = new SelectList(db.EmployeeTables, "EmployeeID", "Name", employeeAttendanceTable.Employee_ID);
            return View(employeeAttendanceTable);
        }

        // GET: EmployeeAttendanceTables/Delete/5
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
            EmployeeAttendanceTable employeeAttendanceTable = db.EmployeeAttendanceTables.Find(id);
            if (employeeAttendanceTable == null)
            {
                return HttpNotFound();
            }
            return View(employeeAttendanceTable);
        }

        // POST: EmployeeAttendanceTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            EmployeeAttendanceTable employeeAttendanceTable = db.EmployeeAttendanceTables.Find(id);
            db.EmployeeAttendanceTables.Remove(employeeAttendanceTable);
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
