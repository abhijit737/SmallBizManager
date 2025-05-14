

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmallBizManager.Models;
using SmallBizManager.Services;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace SmallBizManager.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductService productService, IWebHostEnvironment env)
        {
            _productService = productService;
            _env = env;
        }

        public IActionResult Index()
        {
            var data = _productService.GetAllProducts();
            return View(data);
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    product.ImagePath = "/uploads/" + fileName;

                    string thumbFileName = "thumb_" + fileName;
                    string thumbPath = Path.Combine(uploadsFolder, thumbFileName);
                    GenerateThumbnail(filePath, thumbPath, 100, 100);
                    product.ThumbnailPath = "/uploads/" + thumbFileName;
                }

                _productService.CreateProduct(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Edit(int id)
        {
            var data = _productService.GetProductById(id);
            return View(data);
        }

      

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    product.ImagePath = "/images/" + uniqueFileName;
                }

                _productService.UpdateProduct(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }


        [Authorize(Policy = "AdminOnly")]
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        private void GenerateThumbnail(string sourcePath, string targetPath, int width, int height)
        {
            using (var image = System.Drawing.Image.FromFile(sourcePath))
            {
                var thumbnail = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
                thumbnail.Save(targetPath, ImageFormat.Jpeg);
            }
        }
    }
}

