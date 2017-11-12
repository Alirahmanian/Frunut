using FrunutStock.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrunutStock.Data
{
    public class FrunutStockSeedData : DropCreateDatabaseAlways<FrunutStockEntities>  //DropCreateDatabaseIfModelChanges<FrunutStockEntities>
    {
        protected override void Seed(FrunutStockEntities context)
        {
           
            GetEmployees().ForEach(e => context.Employees.Add(e));
            context.Commit();
           
        }
        
       


        private static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee { FirstName = "Ali", LastName ="Rahmanian", Age=57 }
            };
        }
    }
}