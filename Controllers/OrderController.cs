

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using SmallBizManager.Models;
using SmallBizManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using static SmallBizManager.Models.Order;
using DinkToPdf.Contracts;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.ComponentModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


namespace SmallBizManager.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IConverter _pdfConverter;



        public OrderController(IOrderService orderService, IProductService productService, IConverter converter)
        {
            _orderService = orderService;
            _productService = productService;
            _pdfConverter = converter;


        }

        public IActionResult Index()
        {
            var data = _orderService.GetAllOrders();
            return View(data);
        }


        private List<SelectListItem> GetStatusList(string selected = null) =>
            Enum.GetValues<OrderStatus>()
                .Select(s => new SelectListItem(s.ToString(), s.ToString()) { Selected = s.ToString() == selected })
                .ToList();




        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Products = _productService.GetAllProducts();
            ViewBag.StatusList = GetStatusList(OrderStatus.Pending.ToString());


            var vm = new CreateOrderViewModel
            {
                OrderDate = DateTime.Now,


                Status = OrderStatus.Pending.ToString(),
                Items = new List<OrderItemInputModel> { new() }


            };
            return View(vm);
        }

        [Authorize(Policy = "StaffOnly")]
        [HttpPost]
        public IActionResult Create(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Products = _productService.GetAllProducts();
                ViewBag.StatusList = GetStatusList(model.Status);
                return View(model);
            }

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
                })
                .ToList();

            var order = new Order
            {
                CustomerName = model.CustomerName,
                OrderDate = model.OrderDate,
                Status = Enum.Parse<OrderStatus>(model.Status),
                Items = orderItems,
                TotalAmount = orderItems.Sum(i => i.Quantity * i.UnitPrice)
            };

            _orderService.CreateOrder(order);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();

            var vm = new CreateOrderViewModel
            {
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount,
                Items = order.Items.Select(i => new OrderItemInputModel
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };

            ViewBag.Products = _productService.GetAllProducts();
            ViewBag.StatusList = GetStatusList(vm.Status);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(int id, CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Products = _productService.GetAllProducts();
                ViewBag.StatusList = GetStatusList(model.Status);
                return View(model);
            }

            var existingOrder = _orderService.GetOrderById(id);
            if (existingOrder == null) return NotFound();

            
            existingOrder.Items.Clear();
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
                })
                .ToList();

            existingOrder.CustomerName = model.CustomerName;
            existingOrder.OrderDate = model.OrderDate;
            existingOrder.Status = Enum.Parse<OrderStatus>(model.Status);
            existingOrder.Items = updatedItems;
            existingOrder.TotalAmount = updatedItems.Sum(i => i.Quantity * i.UnitPrice);

            _orderService.UpdateOrder(existingOrder);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction("Index");
        }


        public IActionResult ExportToExcel()
        {
            ExcelPackage.License.SetNonCommercialPersonal("Abhijit More");


            var orders = _orderService.GetAllOrders();


            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Orders");

                worksheet.Cells[1, 1].Value = "Order ID";
                worksheet.Cells[1, 2].Value = "Customer Name";
                worksheet.Cells[1, 3].Value = "Order Date";
                worksheet.Cells[1, 4].Value = "Items";
                worksheet.Cells[1, 5].Value = "Status";
                worksheet.Cells[1, 6].Value = "Total Amount";

                int row = 2;
                foreach (var order in orders)
                {
                    worksheet.Cells[row, 1].Value = order.Id;
                    worksheet.Cells[row, 2].Value = order.CustomerName;
                    worksheet.Cells[row, 3].Value = order.OrderDate.ToString("dd-MM-yyyy");

                    var itemDetails = string.Join("\n", order.Items.Select(item =>
                        $"{item.Product.Name} (Qty: {item.Quantity}, Price: ₹ {item.UnitPrice})"));
                    worksheet.Cells[row, 4].Value = itemDetails;

                    worksheet.Cells[row, 5].Value = order.Status.ToString();
                    worksheet.Cells[row, 6].Value = $"₹ {order.TotalAmount}";

                    row++;
                }

                worksheet.Column(1).AutoFit();
                worksheet.Column(2).AutoFit();
                worksheet.Column(3).AutoFit();
                worksheet.Column(4).AutoFit();
                worksheet.Column(5).AutoFit();
                worksheet.Column(6).AutoFit();

                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Orders.xlsx");
            }
        }
    }
}

