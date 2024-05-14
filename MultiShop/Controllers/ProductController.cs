using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccessLayer;
using MultiShop.Models;
using MultiShop.SendModelView;
using MultiShop.ViewModels.Products;

namespace MultiShop.Controllers
{
    public class ProductController(MultishopContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var data = await _context.products
                .Where(a => !a.isDelete)
                .Select(s => new GetProductAdminVM
                {
                    Name = s.Name,
                    Id = s.Id,
                    SellPrice = s.SellPrice,
                    Discount = s.Discount,
                    ImageUrl = s.ImageUrl

                }).ToListAsync() ;

            ShopDetail shopDetail = new ShopDetail
            {
                getProductVM = data
            };

            return View(shopDetail);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            
            if (id == null || id < 1) BadRequest();


            Product product = await _context.products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null) NotFound();

            GetProductAdminVM getProductAdminVM = new GetProductAdminVM
            {
                Name =  product.Name,
                Description = product.Description,
                SellPrice = product.SellPrice,
                CostPrice = product.CostPrice,
                StockCount = product.StockCount,
                Discount = product.Discount,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
            };

            var data = await _context.products
                .Where(a=> a.CategoryId==product.CategoryId && a.Id!=product.Id)
                .Take(6)
                .Select(s=> new GetProductAdminVM
                {
                    Name = s.Name,
                    Id = s.Id,
                    SellPrice = s.SellPrice,
                    Discount = s.Discount,
                    ImageUrl = s.ImageUrl
                }).ToListAsync();


            DetailData detailData = new DetailData
            {
                GetProductVM = getProductAdminVM,
                GetSameCategoryProductVM = data,
            };

           // //var query = _context.products.AsQueryable();
           // IQueryable<Product> query = _context.products;

           // query = query.OrderBy(p => p.SellPrice);



           // query = query.Where(p => p.CategoryId == 2);

           // query = query.Where(p => p.Name.Contains("salam"));

           //await query.ToListAsync();






            //GetProductAdminVM[] products = {getProductAdminVM,}; 
            return View(detailData);
        }
    }
}
