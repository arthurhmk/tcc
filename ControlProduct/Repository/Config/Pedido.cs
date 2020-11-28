using ControlProduct.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlProduct.Repository.Config
{
    public class Pedido : IEntityTypeConfiguration<Models.Pedido>
    {
        public void Configure(EntityTypeBuilder<Models.Pedido> builder)
        {
            builder
                .ToTable("pedido");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("cd_pedido");

            builder
                .Property(p => p.IdCliente)
                .HasColumnName("cd_cliente");

            builder
                .Property(p => p.Valor)
                .HasColumnName("vl_pedido")
                .HasConversion(
                    p => (decimal)p,
                    p => decimal.ToDouble(p)
                );

            builder
                .Property(p => p.Estado)
                .HasColumnName("ic_status_pedido")
                .HasConversion(
                    p => (char)p,
                    p => (EstadoPedido)p
                );

            builder
                .Property(p => p.DataPedido)
                .HasColumnName("dt_pedido");

            builder
                .Property(p => p.DataEntrega)
                .HasColumnName("dt_entrega");

            builder
                .Property(p => p.TipoEntrega)
                .HasColumnName("ic_tipo_entrega")
                .HasConversion(
                    p => (char)p,
                    p => (TipoEntrega)p
                );

            builder
                .Property(p => p.EnderecoEntrega)
                .HasMaxLength(100)
                .HasColumnName("ds_endereco_entrega");

            builder
                .Property(p => p.ValorEntrega)
                .HasColumnName("vl_entrega")
                .HasConversion(
                    p => (decimal)p,
                    p => decimal.ToDouble(p)
                );

            builder
                .HasOne(p => p.Cliente).WithMany(p => p.Pedidos).HasForeignKey(p => p.IdCliente);

            builder
                .HasMany(p => p.Extras).WithOne(p => p.Pedido).HasForeignKey(p => p.IdPedido);

            builder
                .HasMany(p => p.Pagamentos).WithOne(p => p.Pedido).HasForeignKey(p => p.IdPedido);

            builder
                .HasMany(p => p.PedidoProdutos).WithOne(p => p.Pedido).HasForeignKey(p => p.IdPedido);
        }
    }
}
