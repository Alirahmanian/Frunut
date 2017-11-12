using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FrunutStock.Model.Models
{
    class Role
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }
    }
}
