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
    public class EmployeeEducationTablesController : Controller
    {
        private RetailGuruEntities db = new RetailGuruEntities();

        // GET: EmployeeEducationTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var employeeEducationTables = db.EmployeeEducationTables.Include(e => e.EmployeeTable);
            return View(employeeEducationTables.ToList());
        }

        // GET: EmployeeEducationTables/Details/5
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
            EmployeeEducationTable employeeEducationTable = db.EmployeeEducationTables.Find(id);
            if (employeeEducationTable == null)
            {
                return HttpNotFound();
            }
            return View(employeeEducationTable);
        }

        // GET: EmployeeEducationTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "Name");
            return View();
        }

        // POST: EmployeeEducationTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeEducationTable employeeEducationTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }


            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            employeeEducationTable.UserID = userid;

            if (ModelState.IsValid)
            {
                db.EmployeeEducationTables.Add(employeeEducationTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "Name", employeeEducationTable.EmployeeID);
            return View(employeeEducationTable);
        }

        // GET: EmployeeEducationTables/Edit/5
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
            EmployeeEducationTable employeeEducationTable = db.EmployeeEducationTables.Find(id);
            if (employeeEducationTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "Name", employeeEducationTable.EmployeeID);
            return View(employeeEducationTable);
        }

        // POST: EmployeeEducationTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeEducationTable employeeEducationTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }


            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            employeeEducationTable.UserID = userid;

            if (ModelState.IsValid)
            {
                db.Entry(employeeEducationTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "Name", employeeEducationTable.EmployeeID);
            return View(employeeEducationTable);
        }

        // GET: EmployeeEducationTables/Delete/5
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
            EmployeeEducationTable employeeEducationTable = db.EmployeeEducationTables.Find(id);
            if (employeeEducationTable == null)
            {
                return HttpNotFound();
            }
            return View(employeeEducationTable);
        }

        // POST: EmployeeEducationTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            EmployeeEducationTable employeeEducationTable = db.EmployeeEducationTables.Find(id);
            db.EmployeeEducationTables.Remove(employeeEducationTable);
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
