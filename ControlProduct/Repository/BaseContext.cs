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
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoExtra> PedidoExtra { get; set; }
        public DbSet<PedidoProduto> PedidoProduto { get; set; }
        public DbSet<Pagamento> Pagamento { get; set; }
        public DbSet<Debito> Debito { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Config.Cliente());
            modelBuilder.ApplyConfiguration(new Config.Categoria());
            modelBuilder.ApplyConfiguration(new Config.Produto());
            modelBuilder.ApplyConfiguration(new Config.Pedido());
            modelBuilder.ApplyConfiguration(new Config.PedidoExtra());
            modelBuilder.ApplyConfiguration(new Config.PedidoProduto());
            modelBuilder.ApplyConfiguration(new Config.Pagamento());
            modelBuilder.ApplyConfiguration(new Config.Debito());
            modelBuilder.ApplyConfiguration(new Config.User());
        }
    }
}
