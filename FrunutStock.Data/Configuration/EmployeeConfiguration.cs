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
    class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("Employees");
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AddedDate).IsRequired();
            Property(t => t.ModifiedDate).IsOptional();
            Property(t => t.UserName).IsRequired().HasMaxLength(50);

            Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            Property(e => e.LastName).IsRequired().HasMaxLength(50);
            Property(t => t.Age).IsOptional();

        }
    }
}
