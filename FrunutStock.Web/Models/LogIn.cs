using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace FrunutStock.Web.Models
{
    public class LogIn
    {
        [Required]
        [DataType(DataType.Text), Display(Name ="user name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password), Display(Name = "password")]
        public string Password { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}