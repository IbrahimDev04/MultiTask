using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccessLayer;
using MultiShop.SendModelView;
using MultiShop.ViewModels.Categories;
using MultiShop.ViewModels.Products;
using MultiShop.ViewModels.Sliders;

namespace MultiShop.Controllers
{
    public class HomeController(MultishopContext _context) : Controller
    {

        public async Task<IActionResult> Index()
        {

            
            
            var data = await _context.sliders
                .Where(a => !a.isDelete)
                .Select(s => new GetSliderVM
                {
                    Id = s.Id,
                    Title = s.Title,
                    SubTitle = s.SubTitle,
                    ImageUrl = s.İmageUrl
                }).ToListAsync();

            var data1 = await _context.products
                .Where(a => !a.isDelete)
                .Select(s => new GetProductAdminVM
                {
                    Id = s.Id,
                    Name = s.Name,
                    SellPrice = s.SellPrice,
                    Discount = s.Discount,
                    ImageUrl = s.ImageUrl
                }).ToListAsync();

            var data2 = await _context.categories
                .Where(a => !a.isDelete)
                .Select(s => new GetCategoryVM
                {
                    Name = s.Name,
                    ImageUrl = s.ImageUrl,
                }).ToListAsync();

            HomeData homeData = new HomeData
            {
                getProductVM = data1,
                getSliderVM = data,
                getCategoryVM = data2,
            };

            return View(homeData);
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
