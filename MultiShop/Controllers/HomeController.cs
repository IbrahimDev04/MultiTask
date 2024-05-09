using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccessLayer;
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

            return View(data);
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
