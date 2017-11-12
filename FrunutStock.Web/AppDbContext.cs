using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FrunutStock.Web
{
    public class AppDbContext : IdentityDbContext<Models.AppUser> 
    { 
         public AppDbContext() : base("FrunutStockEntities") 
         {
          //  Database.SetInitializer<AppDbContext>( new DropCreateDatabaseIfModelChanges<AppDbContext>());
          //  Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, FrunutStock.Web.Migrations.Configuration>());
          //  Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());
           // Database.SetInitializer<AppDbContext>(null);

        }
        // in case of 'There is already an object named 'Tablename' in the database problem' run this:
        //Add-Migration Initial -IgnoreChanges
        //Update-Database -verbose

    }

   
}