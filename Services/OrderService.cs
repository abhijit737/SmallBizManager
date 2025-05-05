using SmallBizManager.Data;
using SmallBizManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SmallBizManager.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateOrder(Order order)
        {
            if (order == null || order.Items == null || !order.Items.Any())
                return false;


            foreach (var item in order.Items)
            {
                var product = _context.Products.Find(item.ProductId);
                if (product != null)
                {
                    item.UnitPrice = product.Price; 
                }
            }


            order.TotalAmount = order.Items.Sum(item => item.UnitPrice * item.Quantity);


            _context.Orders.Add(order);
            _context.SaveChanges();
            return true;
        }


        public List<Order> GetAllOrders()
        {
            return _context.Orders
                           .Include(o => o.Items)              
                           .ThenInclude(i => i.Product)       
                           .Include(o => o.Employee)          
                           .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);
        }



        public bool UpdateOrder(Order order)
        {
            var existingOrder = _context.Orders
                                        .Include(o => o.Items)  
                                        .FirstOrDefault(o => o.Id == order.Id);

            if (existingOrder == null)
                return false;


            existingOrder.CustomerName = order.CustomerName;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.Status = order.Status;

            foreach (var updatedItem in order.Items)
            {
                var existingItem = existingOrder.Items.FirstOrDefault(i => i.Id == updatedItem.Id);
                if (existingItem != null)
                {

                    existingItem.Quantity = updatedItem.Quantity;
                    existingItem.UnitPrice = _context.Products.Find(updatedItem.ProductId)?.Price ?? 0;
                }
                else
                {

                    var product = _context.Products.Find(updatedItem.ProductId);
                    if (product != null)
                    {
                        updatedItem.UnitPrice = product.Price;
                        existingOrder.Items.Add(updatedItem);
                    }
                }
            }


            existingOrder.TotalAmount = existingOrder.Items.Sum(item => item.UnitPrice * item.Quantity);


            _context.Orders.Update(existingOrder);
            _context.SaveChanges();
            return true;
        }


        public bool DeleteOrder(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                return false;

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return true;
        }


        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
    }
}
