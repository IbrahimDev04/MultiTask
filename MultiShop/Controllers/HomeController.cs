using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccessLayer;
using MultiShop.SendModelView;
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
                    İmageUrl = s.İmageUrl
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

            HomeData homeData = new HomeData
            {
                getProductVM = data1,
                getSliderVM = data
            };

            return View(homeData);
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
