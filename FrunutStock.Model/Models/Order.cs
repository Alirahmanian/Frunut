using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FrunutStock.Model.Models
{
   public class Order : BaseEntity
    {
        public Int64 CompanyID { get; set; }
        public Int64 EmpoyeeID { get; set; }
        public int AmountItem { get; set; }
        public int AmountReserve { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public DateTime LoadingDate { get; set; }
        public DateTime? LoadedDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string Coments { get; set; }
        public string OrderdBy { get; set; }
        public OrderTransport Transport { get; set; }
        public string PaymentWarning { get; set; }
        public bool ForcedPaid { get; set; }
        public bool OrderPaid { get; set; }
        public bool Cash { get; set; }


        public virtual Company Company { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public enum OrderTransport
        {
            Frunut = 1,
            Company = 2
        }
        

    }
}