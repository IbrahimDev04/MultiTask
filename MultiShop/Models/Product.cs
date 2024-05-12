using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MultiShop.Models
{
    public class Product : BaseEntity
    {
        [MaxLength(32, ErrorMessage = "Başlıq 32 simvoldan artıq ola bilməz."), Required]
        public string Name {  get; set; }

        [MaxLength(350, ErrorMessage = "Description 32 simvoldan artıq ola bilməz."), Required]
        public string Description { get; set; }

        [Required]
        public decimal SellPrice { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        [Required]
        public int StockCount { get; set; }

        [Range(0,100), Required]
        public int Discount { get; set; }

        [Required]
        public string ImageUrl { get; set; }



        public ICollection<ProductImage>? Images { get; set; }
        public ICollection<Size>? Sizes { get; set; }
        public ICollection<Color>? Colors { get; set; }
    }
}
