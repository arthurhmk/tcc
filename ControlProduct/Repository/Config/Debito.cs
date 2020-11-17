using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlProduct.Repository.Config
{
    public class Debito: IEntityTypeConfiguration<Models.Debito>
    {
        public void Configure(EntityTypeBuilder<Models.Debito> builder)
        {
            builder
                .ToTable("debito", "ControlProduct");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("cd_debito");

            builder
                .Property(p => p.Data)
                .HasColumnName("dt_debito");

            builder
                .Property(p => p.Valor)
                .HasColumnName("vl_debito");
        }
    }
}
