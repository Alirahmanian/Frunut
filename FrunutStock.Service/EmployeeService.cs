using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrunutStock.Data.Infrastructure;
using FrunutStock.Data.Repositories;
using FrunutStock.Model.Models;

namespace FrunutStock.Service
{
    // operations you want to expose
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees(Int64? id);
        Employee GetEmployee(Int64 id);
        IEnumerable<Employee> GetEmployeeByName(string employeeName);
        void CreateEmployee(Employee employee);
        void SaveEmployee();
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IUnitOfWork unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            this.employeeRepository = employeeRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IEmployeeService Members

        public IEnumerable<Employee> GetEmployees(Int64? id)
        {
            if (id == null || id <= 0)
                return employeeRepository.GetAll();
            else
                return employeeRepository.GetAll().Where(e => e.ID == id);
        }

        public Employee GetEmployee(Int64 id)
        {
            var employee = employeeRepository.GetById(id);
            return employee;
        }

        public IEnumerable<Employee> GetEmployeeByName(string employeeName)
        {
            return employeeRepository.GetAll().Where(e => e.FullName == employeeName);
        }

        public void CreateEmployee(Employee employee)
        {
            employeeRepository.Add(employee);
        }

        public void SaveEmployee()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}
