using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlProduct.Repository.Config
{
    public class Debito: IEntityTypeConfiguration<Models.Debito>
    {
        public void Configure(EntityTypeBuilder<Models.Debito> builder)
        {
            builder
                .ToTable("debito");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("cd_debito");

            builder
                .Property(p => p.Data)
                .HasColumnName("dt_debito");


            builder
                .Property(p => p.Entrada)
                .HasColumnName("vl_entrada")
                .HasConversion(
                    p => (decimal)p,
                    p => decimal.ToDouble(p)
                );


            builder
                .Property(p => p.Saida)
                .HasColumnName("vl_saida")
                .HasConversion(
                    p => (decimal)p,
                    p => decimal.ToDouble(p)
                );

            builder
                .Property(p => p.Valor)
                .HasColumnName("vl_debito")
                .HasConversion(
                    p => (decimal)p,
                    p => decimal.ToDouble(p)
                );
        }
    }
}
