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

        public DbSet<Sizes> sizes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=CA-R214-PC15;Database=MultiShop;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
