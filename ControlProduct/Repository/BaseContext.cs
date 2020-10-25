using ControlProduct.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlProduct.Repository
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Config.Cliente());
            modelBuilder.ApplyConfiguration(new Config.Categoria());
        }
    }
}
