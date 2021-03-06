﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrunutStock.Model.Models
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
        public int? Age { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}