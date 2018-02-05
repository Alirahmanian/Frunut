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
    public interface IOrderService
    {
        IEnumerable<Order> GetOrders(Int64? id);
        Order GetOrder(Int64 id);
        IEnumerable<Order> GetOrdersByCompany(Company company);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
        void SaveOrder();
    }

   public class OrderService: IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            this.orderRepository = orderRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IOrderService Members

        public IEnumerable<Order> GetOrders(Int64? id)
        {
            if (id == null || id <= 0)
            {
                var orders = orderRepository.GetOrderWithIncludes();
                return orders;
            }
            else
            {
                return orderRepository.GetAll().Where(o => o.ID == id);
            }
               
        }

        public Order GetOrder(Int64 id)
        {
            var order = orderRepository.GetById(id);
            return order;
        }

        public IEnumerable<Order> GetOrdersByCompany(Company company)
        {
            return orderRepository.GetAll().Where(o => o.CompanyID == company.ID);
        }

        public void CreateOrder(Order order)
        {
            orderRepository.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            orderRepository.Update(order);
        }

        public void DeleteOrder(Order order)
        {
            orderRepository.Delete(order);
        }

        public void SaveOrder()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}

