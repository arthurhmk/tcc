using ControlProduct.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlProduct.Repository.Config
{
    public class Pagamento : IEntityTypeConfiguration<Models.Pagamento>
    {
        public void Configure(EntityTypeBuilder<Models.Pagamento> builder)
        {
            builder
                .ToTable("pagamento");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("cd_pagamento");

            builder
                .Property(p => p.IdPedido)
                .HasColumnName("cd_pedido");

            builder
                .Property(p => p.DataPagamento)
                .HasColumnName("dt_pagamento");

            builder
                .Property(p => p.Valor)
                .HasColumnName("vl_pagamento")
                .HasConversion(
                    p => (decimal)p,
                    p => decimal.ToDouble(p)
                );

            builder
                .HasOne(p => p.Pedido).WithMany(p => p.Pagamentos).HasForeignKey(p => p.IdPedido);
        }
    }
}
