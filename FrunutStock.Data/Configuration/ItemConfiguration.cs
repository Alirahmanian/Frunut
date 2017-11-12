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
    public class ItemConfiguration : EntityTypeConfiguration<Item>
    {
        public ItemConfiguration()
        {
            ToTable("Items");
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AddedDate).IsRequired();
            Property(t => t.ModifiedDate).IsOptional();
            Property(t => t.UserName).IsRequired().HasMaxLength(50);

            Property(t => t.Name).IsRequired().HasMaxLength(100);
            Property(t => t.Description).HasMaxLength(500);
            Property(t => t.MinimumPrice).IsRequired();
            Property(t => t.BoxWeight).IsRequired();
            Property(t => t.ItemGroupID).IsRequired();
            Property(t => t.CountryID).IsOptional();


            //relationship  
            HasRequired(t => t.ItemGroup).WithMany(g => g.Items).HasForeignKey(t => t.ItemGroupID);

        }
    }
}
