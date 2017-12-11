using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using FrunutStock.Data;
using FrunutStock.Model.Models;
using System.Collections.Generic;

using System.Linq;
using System.Net;
namespace FrunutStock.Web.Tests
{
    [TestClass]
    public class CompanyTest : DbContext
    {
        [TestMethod]
        public void TestContext ()
        {

            // arrange  
            var context = new FrunutStockEntities();


            // act  
            var companies = context.Companies.Count();
           
            // assert  

            Assert.AreEqual(2, companies,  "Account not debited correctly");

        }
        [TestMethod]
        public void TestAddCompany()
        {
            using (var context = new FrunutStockEntities())
            {
               
                Company Company = new Company ()
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
