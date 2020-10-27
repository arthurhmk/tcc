using ControlProduct.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlProduct.Repository.Config
{
    public class PedidoProduto : IEntityTypeConfiguration<Models.PedidoProduto>
    {
        public void Configure(EntityTypeBuilder<Models.PedidoProduto> builder)
        {
            builder
                .ToTable("pedido_produto", "ControlProduct");

            builder
                .HasKey(p => new { p.IdPedido, p.IdProduto });

            builder
                .Property(p => p.IdProduto)
                .HasColumnName("cd_produto");

            builder
                .Property(p => p.IdPedido)
                .HasColumnName("cd_pedido");

            builder
                .HasOne(p => p.Produto).WithMany(p => p.PedidoProdutos).HasForeignKey(p => p.IdProduto);

            builder
                .HasOne(p => p.Pedido).WithMany(p => p.PedidoProdutos).HasForeignKey(p => p.IdPedido);
        }
    }
}
