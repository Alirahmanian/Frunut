using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using FrunutStock.Data;
using FrunutStock.Model.Models;
namespace FrunutStock.Web.Tests
{
    [TestClass]
    public class UnitTest1 : DbContext
    {
        
        [TestMethod]
        public void CompanyTest()
        {
            using (var context = new FrunutStockEntities())
            {
                // context.Database.Create();
                FrunutStock.Model.Models.Company Company = new FrunutStock.Model.Models.Company
                {
                    Name = "Zarif Zarifov",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    UserName = "alirah",
                    
                };
                context.Entry(Company).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}
