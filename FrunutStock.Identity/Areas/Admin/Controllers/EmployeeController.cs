using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrunutStock.Model.Models;
using FrunutStock.Service;
using FrunutStock.Identity.Areas.Admin.ViewModels;
using AutoMapper;

namespace FrunutStock.Identity.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        // GET: Admin/Employee
        public ActionResult Index()
        {
            //IEnumerable<EmployeeViewModel> viewModelEmployee;
            IEnumerable<Employee> employees;
            employees = employeeService.GetEmployees().ToList();
           // viewModelEmployee = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(employees);

            
            
        }
    }
}