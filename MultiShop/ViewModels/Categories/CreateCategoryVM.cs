using System.ComponentModel.DataAnnotations;

namespace MultiShop.ViewModels.Categories
{
    public class CreateCategoryVM
    {
        [MaxLength(32, ErrorMessage = "Size Error"),Required]
        public string Name { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
