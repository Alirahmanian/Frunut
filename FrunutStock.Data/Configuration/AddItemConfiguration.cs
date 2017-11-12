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
    class AddItemConfiguration : EntityTypeConfiguration<AddItem>
    {
        public AddItemConfiguration()
        {
            ToTable("AddItems");
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AddedDate).IsRequired();
            Property(t => t.ModifiedDate).IsOptional();
            Property(t => t.UserName).IsRequired().HasMaxLength(50);

            Property(t => t.Date).IsRequired();
            Property(t => t.Description).HasMaxLength(500);
            Property(t => t.QtyBoxes).IsRequired();
            Property(t => t.QtyKg).IsRequired();
            Property(t => t.ItemID).IsRequired();
            Property(t => t.WarehouseID).IsRequired();



            //relationship  
            HasRequired(t => t.Item).WithMany(g => g.AddItems).HasForeignKey(t => t.ItemID);
            HasRequired(t => t.Warehouse).WithMany(g => g.AddItems).HasForeignKey(t => t.WarehouseID);

        }
    }
}
