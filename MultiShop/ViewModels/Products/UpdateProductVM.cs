using System.ComponentModel.DataAnnotations;

namespace MultiShop.ViewModels.Products
{
    public class UpdateProductVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SellPrice { get; set; }
        public decimal CostPrice { get; set; }
        public int Discount { get; set; }
        public int StockCount { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
