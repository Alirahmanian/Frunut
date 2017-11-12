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
    public class WarehouseConfiguration : EntityTypeConfiguration<Warehouse>
    {
        public WarehouseConfiguration()
        {
            ToTable("Warehouses");
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AddedDate).IsRequired();
            Property(t => t.ModifiedDate).IsOptional();
            Property(t => t.UserName).IsRequired().HasMaxLength(50);

            Property(t => t.Name).IsRequired();
            


            //relationship  
           

        }
    }
}
