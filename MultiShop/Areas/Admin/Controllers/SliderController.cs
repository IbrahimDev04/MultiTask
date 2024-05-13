using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccessLayer;
using MultiShop.Extensions;
using MultiShop.Models;
using MultiShop.ViewModels.Sliders;

namespace MultiShop.Areas.Admin.Controllers
{   
    [Area("Admin")]
    public class SliderController(MultishopContext _context, IWebHostEnvironment _env) : Controller
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
                
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSliderVM sw)
        {
            if (sw.ImageFile != null)
            {
                if (!sw.ImageFile.IsValidType("image"))
                    ModelState.AddModelError("ImageFile", "Type Error");

                if (!sw.ImageFile.IsValidSize(200))
                    ModelState.AddModelError("ImageFile", "Size Error");
            }

            if (!ModelState.IsValid)
                return View(sw);

            string fileName = await sw.ImageFile.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "sliders"));
            Slider slider = new Slider
            {
                Title = sw.Title,
                SubTitle = sw.SubTitle,
                İmageUrl = Path.Combine("imgs","sliders",fileName),
                isDelete=false,
                CreatedTime=DateTime.Now,
            };

            await _context.sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Slider slider = await _context.sliders.FirstOrDefaultAsync(s => s.Id == id);

            if (slider == null) return NotFound();

            UpdateSliderVM sliderVM = new UpdateSliderVM
            {
                Title = slider.Title,
                SubTitle = slider.SubTitle,
            };

            return View(sliderVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateSliderVM sliderVM)
        {
            if (id == null || id < 1) return BadRequest();

            Slider slider = await _context.sliders.FirstOrDefaultAsync (s => s.Id == id);

            if (slider == null) return NotFound();

            slider.Title = sliderVM.Title;
            slider.SubTitle = sliderVM.SubTitle;
            slider.UpdatedTime = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            var item = await _context.sliders.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null) return NotFound();

            item.İmageUrl.Delete(Path.Combine(_env.WebRootPath));
            _context.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
