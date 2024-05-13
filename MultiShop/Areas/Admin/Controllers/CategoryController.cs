using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccessLayer;
using MultiShop.Extensions;
using MultiShop.Models;
using MultiShop.ViewModels.Categories;

namespace MultiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(MultishopContext _context, IWebHostEnvironment _env) : Controller
    {
        
        public async Task<IActionResult> Index()
        {
          

            return View(await _context.categories
                .Select(s => new GetCategoryVM
                {
                    Id = s.Id,
                    Name = s.Name,
                    ImageUrl = s.ImageUrl,
                }).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM vm)
        {
            if(vm.ImageFile != null)
            {
                if (!vm.ImageFile.IsValidType("image"))
                        ModelState.AddModelError("ImageFile", "Type Error");

                if (!vm.ImageFile.IsValidSize(200))
                        ModelState.AddModelError("ImageFile", "Size Error");
            }

            if (!ModelState.IsValid) 
                return View(vm);

            string fileName = await vm.ImageFile.SaveFileAsync(Path.Combine(_env.WebRootPath,"imgs","categories"));

            await _context.categories.AddAsync(new Models.Category
            {
                Name = vm.Name,
                ImageUrl = Path.Combine("imgs", "categories", fileName),
                isDelete = false,
                CreatedTime = DateTime.Now,
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            var item = await _context.categories.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null) return NotFound();

            item.ImageUrl.Delete(Path.Combine(_env.WebRootPath));
            _context.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
