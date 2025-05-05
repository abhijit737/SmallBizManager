
    
    using System.Collections.Generic;
    using global::SmallBizManager.Models;

    namespace SmallBizManager.Services
    {
        public interface IOrderService
        {
            bool CreateOrder(Order order);
            List<Order> GetAllOrders();

            Order GetOrderById(int orderId);
            bool UpdateOrder(Order order);
            bool DeleteOrder(int orderId);
        IEnumerable<Product> GetAllProducts();

    }
}


