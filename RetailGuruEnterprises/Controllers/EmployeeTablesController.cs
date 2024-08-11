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
    public class EmployeeTablesController : Controller
    {
        private RetailGuruEntities db = new RetailGuruEntities();

        // GET: EmployeeTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var employeeTables = db.EmployeeTables.Include(e => e.DesignationTable).Include(e => e.UserTable);
            return View(employeeTables.ToList());
        }

        // GET: EmployeeTables/Details/5
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
            EmployeeTable employeeTable = db.EmployeeTables.Find(id);
            if (employeeTable == null)
            {
                return HttpNotFound();
            }
            return View(employeeTable);
        }

        // GET: EmployeeTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.Designation_ID = new SelectList(db.DesignationTables, "DesignationID", "Title");
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName");
            return View();
        }

        // POST: EmployeeTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeTable employeeTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }


            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            employeeTable.User_ID = userid;


            employeeTable.Photo = "/Content/EmployeePhotos/default.png";
            if (ModelState.IsValid)
            {
                db.EmployeeTables.Add(employeeTable);
                db.SaveChanges();

                if (employeeTable.PhotoFile != null)
                {
                    var folder = "/Content/EmployeePhotos";
                    var file = string.Format("{0}.png", employeeTable.EmployeeID);
                    var response = FileHelper.UploadFile.UploadPhoto(employeeTable.PhotoFile, folder, file);

                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        employeeTable.Photo = pic;
                        db.Entry(employeeTable).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }


            ViewBag.Designation_ID = new SelectList(db.DesignationTables, "DesignationID", "Title", employeeTable.Designation_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", employeeTable.User_ID);
            return View(employeeTable);
        }

        // GET: EmployeeTables/Edit/5
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
            EmployeeTable employeeTable = db.EmployeeTables.Find(id);
            if (employeeTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.Designation_ID = new SelectList(db.DesignationTables, "DesignationID", "Title", employeeTable.Designation_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", employeeTable.User_ID);
            return View(employeeTable);
        }

        // POST: EmployeeTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeTable employeeTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                if (employeeTable.PhotoFile != null)
                {
                    UploadEmployeePhoto(employeeTable);
                }

                db.Entry(employeeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Designation_ID = new SelectList(db.DesignationTables, "DesignationID", "Title", employeeTable.Designation_ID);
            ViewBag.User_ID = new SelectList(db.UserTables, "UserID", "FullName", employeeTable.User_ID);
            return View(employeeTable);
        }
        // GET: EmployeeTables/Delete/5
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
            EmployeeTable employeeTable = db.EmployeeTables.Find(id);
            if (employeeTable == null)
            {
                return HttpNotFound();
            }
            return View(employeeTable);
        }

        // POST: EmployeeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            EmployeeTable employeeTable = db.EmployeeTables.Find(id);
            db.EmployeeTables.Remove(employeeTable);
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
        private bool UploadEmployeePhoto(EmployeeTable employeeTable)
        {
            var folder = "/Content/EmployeePhotos";
            var file = string.Format("{0}.png", employeeTable.EmployeeID);
            var response = FileHelper.UploadFile.UploadPhoto(employeeTable.PhotoFile, folder, file);

            if (response)
            {
                var pic = string.Format("{0}/{1}", folder, file);
                employeeTable.Photo = pic;
                return true;
            }

            return false;
        }
    }
}
