using System.ComponentModel.DataAnnotations;

namespace MultiShop.ViewModels.Products
{
    public class CreateProductVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SellPrice { get; set; }
        public decimal CostPrice { get; set; }
        [Range(0,100)]
        public int Discount { get; set; }
        public int CategoryIds { get; set; }
        public int StockCount { get; set; }
        public IFormFile ImageFile { get; set; }
        public IEnumerable<IFormFile> ImageFiles { get; set; }
        //public IEnumerable<IFormFile> ImageFiles { get; set; }
    }
}
