using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrunutStock.Web.Areas.Admin.ViewModels
{
    public class EmployeeFormViewModel
    {
        public int EmployeeID { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
    }
}