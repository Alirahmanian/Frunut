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
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PaidDate { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LoadingDate { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LoadedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal AmountPaid { get; set; }
        public string Coments { get; set; }
        public string OrderdBy { get; set; }
        public OrderTransport Transport { get; set; }
        public string PaymentWarning { get; set; }
        public bool ForcedPaid { get; set; }
        public bool OrderPaid { get; set; }
        public bool Cash { get; set; }


        public Company Company { get; set; }
        public  Employee Employee { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public enum OrderTransport
        {
            Frunut = 1,
            Company = 2
        }
        

    }
}