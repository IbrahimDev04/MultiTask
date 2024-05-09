using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccessLayer;
using MultiShop.Models;
using MultiShop.ViewModels.Sliders;

namespace MultiShop.Areas.Admin.Controllers
{   
    [Area("Admin")]
    public class SliderController(MultishopContext _context) : Controller
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSliderVM sw)
        {
            if(!ModelState.IsValid)
            {
                return View(sw);
                
            }
            Slider slider = new Slider
            {
                Title = sw.Title,
                SubTitle = sw.SubTitle,
                İmageUrl = sw.İmageUrl,
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
                İmageUrl = slider.İmageUrl,
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
            slider.İmageUrl = sliderVM.İmageUrl;
            slider.UpdatedTime = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            
            var item = await _context.sliders.FindAsync(id);
            if (item == null) return NotFound();
            _context.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
