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
    class ItemWarehouseConfiguration : EntityTypeConfiguration<ItemWarehouse>
    {
        public ItemWarehouseConfiguration()
        {
            ToTable("ItemWarehouses");
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AddedDate).IsRequired();
            Property(t => t.ModifiedDate).IsOptional();
            Property(t => t.UserName).IsRequired().HasMaxLength(50);
                       
            Property(t => t.ItemID).IsRequired();
            Property(t => t.WarehouseID).IsOptional();


            //relationship  
            HasRequired(t => t.Item);
            HasRequired(t => t.Warehouse);

        }
    }
}
