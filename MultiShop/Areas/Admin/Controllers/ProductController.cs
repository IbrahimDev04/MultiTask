using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccessLayer;
using MultiShop.Extensions;
using MultiShop.Models;
using MultiShop.ViewModels.Products;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(MultishopContext _context, IWebHostEnvironment _environment) : Controller
    {
        public async Task<IActionResult> Index()
        {

            return View(
                await _context.products
                .Select(s => new GetProductAdminVM
                {
                    Name = s.Name,
                    Description = s.Description,
                    SellPrice = s.SellPrice,
                    CostPrice = s.CostPrice,
                    StockCount = s.StockCount,
                    Id = s.Id,
                    ImageUrl = s.ImageUrl,
                    Discount = s.Discount
                }).ToListAsync()
                );
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM vm)
        {
            if (!vm.ImageFile.IsValidType("image"))
                ModelState.AddModelError("ImageFile", "Fayl şəkil formatında olmalıdır.");
            if (!vm.ImageFile.IsValidSize(200))
                ModelState.AddModelError("ImageFile", "Faylın uzunluğu 200kb dan çox olmamalıdır.");


            if (!ModelState.IsValid) return View(vm);

            string fileName = await vm.ImageFile.SaveFileAsync(Path.Combine(_environment.WebRootPath, "imgs", "products"));

            await _context.products.AddAsync(new Models.Product
            {
                CostPrice = vm.CostPrice,
                StockCount = vm.StockCount,
                SellPrice = vm.SellPrice,
                ImageUrl = Path.Combine("imgs", "products", fileName),
                Discount = vm.Discount,
                Name = vm.Name,
                CreatedTime = DateTime.Now,
                isDelete = false,
                Description = vm.Description,
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) BadRequest();

            Product product = await _context.products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null) NotFound();



            UpdateProductVM productVM = new UpdateProductVM
            {
                Name = product.Name,
                Description = product.Description,
                SellPrice = product.SellPrice,
                CostPrice = product.CostPrice,
                StockCount = product.StockCount,
                Discount = product.Discount
            };
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateProductVM uVM)
        {


            if (id == null || id < 1) BadRequest();

            Product product = await _context.products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null) NotFound();


            
            

            product.Name = uVM.Name;
            product.Description = uVM.Description;
            product.SellPrice = uVM.SellPrice;
            product.CostPrice = uVM.CostPrice;
            product.StockCount = uVM.StockCount;
            product.Discount = uVM.Discount;
            if (uVM.ImageFile != null)
            {
                string fileName = await uVM.ImageFile.SaveFileAsync(Path.Combine(_environment.WebRootPath, "imgs", "products"));
                product.ImageUrl = Path.Combine("imgs", "products", fileName);
            }
            product.UpdatedTime = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            var item = await _context.products.FindAsync(id);
            if (item == null) return NotFound();
            _context.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
