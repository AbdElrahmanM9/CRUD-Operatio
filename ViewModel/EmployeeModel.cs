using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Operations.ViewModel
{
    public class EmployeeModel
    {
        public string EmployeeName { get; set; }
        public int Salary { get; set; }
        public string NationalID { get; set; }
        public string Governorate { get; set; }
        public string Center { get; set; }
        public string Village { get; set; }
    }
}