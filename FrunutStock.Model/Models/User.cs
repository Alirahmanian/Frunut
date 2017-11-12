using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FrunutStock.Model.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int EmployeeId { get; set; }

        public Employee employee { get; set; }


    }
}