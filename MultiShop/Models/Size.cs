using System.ComponentModel.DataAnnotations;

namespace MultiShop.Models
{
    public class Size : BaseEntity
    {
        [MaxLength(12),Required]
        public string Name { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
