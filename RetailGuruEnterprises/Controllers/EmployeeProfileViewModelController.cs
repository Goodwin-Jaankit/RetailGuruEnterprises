using DatabaseAccess;
using RetailGuruEnterprises.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RetailGuruEnterprises.Controllers
{
    public class EmployeeProfileViewModelController : Controller
    {
        private RetailGuruEntities db = new RetailGuruEntities();
        // GET: EmployeeProfileViewModel
    
        public ActionResult profilecheck(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = db.EmployeeTables.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var educationDetails = db.EmployeeEducationTables.Where(e => e.EmployeeID == id).ToList();
            var salaryDetails = db.EmployeeSalaryTables.Where(e => e.EmployeeID == id).ToList();

            var viewModel = new EmployeeProfileViewModel
            {
                Employee = employee,
                EducationDetails = educationDetails,
                SalaryDetails = salaryDetails
            };



            return View(viewModel);
        }

    }
}