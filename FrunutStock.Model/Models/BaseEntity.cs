using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FrunutStock.Model.Models
{
    public abstract class BaseEntity
    {
        [ScaffoldColumn(false)]
        public Int64 ID { get; set; }
        [ScaffoldColumn(false)]
        [DisplayName("Created by")]
        public DateTime AddedDate { get; set; }
        [ScaffoldColumn(false)]
        [DisplayName("Changed by")]
        public DateTime? ModifiedDate { get; set ; }
        [ScaffoldColumn(false)]
        [DisplayName("Saved by")]
        public string UserName { get; set; } 
    }
}

