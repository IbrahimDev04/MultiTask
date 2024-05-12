using Microsoft.EntityFrameworkCore;
using MultiShop.Models;

namespace MultiShop.DataAccessLayer
{
    public class MultishopContext : DbContext
    {
        public MultishopContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Slider> sliders { get; set; }

        public DbSet<Product> products { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ZEGA;Database=MultiShopDb;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
