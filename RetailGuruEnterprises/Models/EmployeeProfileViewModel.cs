using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailGuruEnterprises.Models
{
    public class EmployeeProfileViewModel
    {
        public EmployeeTable Employee { get; set; }
        public List<EmployeeEducationTable> EducationDetails { get; set; }
        public List<EmployeeSalaryTable> SalaryDetails { get; set; }
    }
}