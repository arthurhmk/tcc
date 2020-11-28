using ControlProduct.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlProduct.Repository.Config
{
    public class PedidoExtra : IEntityTypeConfiguration<Models.PedidoExtra>
    {
        public void Configure(EntityTypeBuilder<Models.PedidoExtra> builder)
        {
            builder
                .ToTable("pedido_extra");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("cd_extra");

            builder
                .Property(p => p.IdPedido)
                .HasColumnName("cd_pedido");

            builder
                .Property(p => p.Nome)
                .HasColumnName("nm_extra");

            builder
                .Property(p => p.Valor)
                .HasColumnName("vl_pagamento")
                .HasConversion(
                    p => (decimal)p,
                    p => decimal.ToDouble(p)
                );

            builder
                .HasOne(p => p.Pedido).WithMany(p => p.Extras).HasForeignKey(p => p.IdPedido);
        }
    }
}
