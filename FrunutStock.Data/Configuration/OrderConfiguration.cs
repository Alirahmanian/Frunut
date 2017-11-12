using FrunutStock.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrunutStock.Data.Configuration
{
    class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("Orders");
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AddedDate).IsRequired();
            Property(t => t.ModifiedDate).IsOptional();
            Property(t => t.UserName).IsRequired().HasMaxLength(50);

            Property(t => t.CompanyID).IsRequired();
            Property(t => t.EmpoyeeID).IsRequired();
            Property(x => x.OrderDate).IsRequired();
            Property(x => x.PaymentDate).IsRequired();

            Property(x => x.LoadedDate).IsOptional();
            Property(x => x.PaidDate).IsOptional();

            // Relationship
            HasRequired(x => x.Company)
                .WithMany(x => x.Orders)
                .HasForeignKey(y => y.CompanyID);

            HasRequired(x => x.Employee)
                .WithMany(x => x.Orders)
                .HasForeignKey(y => y.EmpoyeeID);









        }
    }
}
