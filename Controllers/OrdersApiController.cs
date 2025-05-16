using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmallBizManager.Models;
using SmallBizManager.Services;

namespace SmallBizManager.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] 
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrderService _orderService; 
        private readonly IProductService _productService;

        public OrdersApiController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminOnly")]

        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();
            return Ok(order);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "StaffOnly")]

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderItems = model.Items
                .Where(i => _productService.GetProductById(i.ProductId) != null)
                .Select(i =>
                {
                    var product = _productService.GetProductById(i.ProductId);
                    return new OrderItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = product.Price
                    };
                }).ToList();

            var order = new Order
            {
                CustomerName = model.CustomerName,
                OrderDate = model.OrderDate,
                Status = Enum.Parse<Order.OrderStatus>(model.Status),
                Items = orderItems,
                TotalAmount = orderItems.Sum(i => i.Quantity * i.UnitPrice)
            };

            _orderService.CreateOrder(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingOrder = _orderService.GetOrderById(id);
            if (existingOrder == null) return NotFound();

            var updatedItems = model.Items
                .Where(i => _productService.GetProductById(i.ProductId) != null)
                .Select(i =>
                {
                    var product = _productService.GetProductById(i.ProductId);
                    return new OrderItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = product.Price
                    };
                }).ToList();

            existingOrder.CustomerName = model.CustomerName;
            existingOrder.OrderDate = model.OrderDate;
            if (!Enum.TryParse<Order.OrderStatus>(model.Status, true, out var parsedStatus))
            {
                return BadRequest(new { error = $"Invalid status value '{model.Status}'. Allowed values are: Pending, Shipped, Completed." });
            }

            existingOrder.Status = parsedStatus;
           //    existingOrder.Status = Enum.Parse<Order.OrderStatus>(model.Status);
            existingOrder.Items = updatedItems;
            existingOrder.TotalAmount = updatedItems.Sum(i => i.Quantity * i.UnitPrice);

            _orderService.UpdateOrder(existingOrder);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var existingOrder = _orderService.GetOrderById(id);
            if (existingOrder == null) return NotFound();

            _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}
