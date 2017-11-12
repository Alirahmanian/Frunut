using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrunutStock.Model.Models
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public bool Ruble { get; set; }
        public decimal Discount { get; set; }
        public decimal Purchases { get; set; }
        public decimal SouldToUs { get; set; }
        public decimal Paid { get; set; }
        public decimal Received { get; set; }
        public DateTime LastBalanceDate { get; set; }
        public decimal LastBalance { get; set; }
        public bool Locked { get; set; }
        public decimal CreditReceived { get; set; }
        public decimal CreditLimit { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
