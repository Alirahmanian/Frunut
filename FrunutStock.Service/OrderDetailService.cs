using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrunutStock.Data.Infrastructure;
using FrunutStock.Data.Repositories;
using FrunutStock.Model.Models;

namespace FrunutStock.Service
{
    // operations you want to expose
    public interface IOrderDetailService
    {
        IEnumerable<OrderDetail> GetOrderDetails(Int64? id);
        OrderDetail GetOrderDetail(Int64 id);
        IEnumerable<OrderDetail> GetOrderDetailsByOrder(Int64 orderId);
        OrderDetail GetSavedOrderDetail(OrderDetail orderDetail);
        void CreateOrderDetail(OrderDetail orderDetail);
        void SaveOrderDetail();
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IUnitOfWork unitOfWork;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            this.orderDetailRepository = orderDetailRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IOrderDetailService Members

        public IEnumerable<OrderDetail> GetOrderDetails(Int64? id)
        {
            if (id == null || id <= 0)
                return orderDetailRepository.GetAll();
            else
                return orderDetailRepository.GetAll().Where(o => o.ID == id);
        }

        public OrderDetail GetOrderDetail(Int64 id)
        {
            var orderDetail = orderDetailRepository.GetById(id);
            return orderDetail;
        }
        public IEnumerable<OrderDetail> GetOrderDetailsByOrder(Int64 orderId)
        {
            return orderDetailRepository.GetOrderDetailsByOrderId(orderId);
        }

        public OrderDetail GetSavedOrderDetail(OrderDetail orderDetail)
        {
            return orderDetailRepository.GetSavedOrderDetail(orderDetail);
        }

        public void CreateOrderDetail(OrderDetail orderDetail)
        {
            orderDetailRepository.Add(orderDetail);
        }

        public void SaveOrderDetail()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}

