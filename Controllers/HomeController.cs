using Microsoft.AspNetCore.Mvc;
using SmallBizManager.Services;
using SmallBizManager.Models;

namespace SmallBizManager.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            var totalProducts = _productService.GetAllProducts().Count();
            var totalOrders = _orderService.GetAllOrders().Count();

            var monthlySales = _orderService.GetAllOrders()
                .Where(o => o.OrderDate.Month == DateTime.Now.Month && o.OrderDate.Year == DateTime.Now.Year)
                .Sum(o => o.TotalAmount);

            var metrics = new DashboardMetrics
            {
                TotalProducts = totalProducts,
                TotalOrders = totalOrders,
                MonthlySales = monthlySales
            };

            return View(metrics); 
        }

        public IActionResult About() => View();

        public IActionResult Contact() => View();

        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public HomeController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }
        public IActionResult Dashboard()
        {
            var totalProducts = _productService.GetAllProducts().Count();
            var totalOrders = _orderService.GetAllOrders().Count();

            var monthlySales = _orderService.GetAllOrders()
                .Where(o => o.OrderDate.Month == DateTime.Now.Month && o.OrderDate.Year == DateTime.Now.Year)
                .Sum(o => o.TotalAmount);

            var metrics = new DashboardMetrics
            {
                TotalProducts = totalProducts,
                TotalOrders = totalOrders,
                MonthlySales = monthlySales
            };

            return View(metrics);
        }
    }
}