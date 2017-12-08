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
    public class OrderDetailConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfiguration()
        {
            ToTable("OrderDetails");
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AddedDate).IsRequired();
            Property(t => t.ModifiedDate).IsOptional();
            Property(t => t.UserName).IsRequired().HasMaxLength(50);

            Property(t => t.OrderID).IsRequired();
            Property(t => t.ItemID).IsRequired();
            Property(t => t.WarehouseID).IsRequired();
            Property(t => t.Price).IsRequired();


            //relationship  
           // HasRequired(t => t.Item).WithMany(g => g.OrderDetails).HasForeignKey(t => t.ItemID);
           // HasRequired(t => t.Warehouse).WithMany(g => g.OrderDetails).HasForeignKey(t => t.WarehouseID);
            HasRequired(t => t.Order).WithMany(g => g.OrderDetails).HasForeignKey(t => t.OrderID);

        }
    
    }
}
