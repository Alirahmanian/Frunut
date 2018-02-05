using FrunutStock.Model.Models;
using FrunutStock.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNet.Identity;
using System.Web;

using System.Data;
using FrunutStock.Data;


namespace FrunutStock.Data
{
   public class FrunutStockEntities : DbContext
    {
        public FrunutStockEntities() : base("FrunutStockEntities") {
            this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ReceiveItem> ReceiveItems { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ItemWarehouse> ItemWarehouses { get; set; }

        //users and roles
        public DbSet<AspNetRoles> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public DbSet<AspNetUsers> AspNetUsers { get; set; }
        public DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }

        public virtual void Commit()
        {
            SaveChanges();
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("AddedDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("AddedDate").CurrentValue = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                   entry.Property("AddedDate").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(
                e =>
                    e.Entity.GetType().GetProperty("ModifiedDate") != null &&
                    e.State == EntityState.Modified))
            {
                entry.Property("ModifiedDate").CurrentValue = DateTime.Now;
            }

            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("UserName") != null))
            {
                entry.Property("UserName").CurrentValue = System.Web.HttpContext.Current.User.Identity.GetUserName();
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<FrunutStockEntities>(null);
          //  Database.SetInitializer<FrunutStockEntities>(new Drop DropCreateDatabaseIfModelChanges<FrunutStockEntities>());
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace))
           .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
           

           /*
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailConfiguration());
            modelBuilder.Configurations.Add(new ItemGroupConfiguration());
            modelBuilder.Configurations.Add(new ItemConfiguration());
            */

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);


            base.OnModelCreating(modelBuilder);


        }

       
    }
}
