using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailGuruEnterprises.Controllers
{
    public class ReportsController : Controller
    {
        private RetailGuruEntities db = new RetailGuruEntities();
        // GET: Reports
        public ActionResult EmployeeAttendance(int? id)
        {
            var staffattendance = db.EmployeeAttendanceTables.Where(t => t.Employee_ID == id).OrderByDescending(t => t.EmployeeAttendanceID);
            return View(staffattendance);
        }

        public ActionResult EmployeePerformance(int? id)
        {
            var empperformance = db.EmployeePerformances.Where(t => t.EmployeeID == id).OrderByDescending(t => t.PerformanceID);
            return View(empperformance);
        }


        //[HttpPost]
        //public ActionResult profilecheck(int? id)
        //{
           
        //    var empideducation = db.EmployeeEducationTables.Find(id);
        //    var empidsalary = db.EmployeeSalaryTables.Find(id);
        //    var empidemptbl = db.EmployeeTables.Find(id);
        //    if (empideducation != null && empidsalary != null && empidemptbl != null)
        //    {
        //        var emptables = db.EmployeeTables.Where(e => e.EmployeeID == empidemptbl.EmployeeID).ToList();
      
        //        return View(emptables);
        //    }
        //    if (empideducation != null && empidsalary != null && empidemptbl != null)
        //    {
        //        var empeducation = db.EmployeeEducationTables.Where(e => e.EmployeeID == empideducation.EmployeeID).ToList();

        //        return View(empeducation);
        //    }
        //    if (empideducation != null && empidsalary != null && empidemptbl != null)
        //    {
        //        var empsalary = db.EmployeeSalaryTables.Where(e => e.EmployeeID == empidsalary.EmployeeID).ToList();

        //        return View(empsalary);
        //    }
        //    return HttpNotFound();
        //}
    }
}