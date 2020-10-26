using ControlProduct.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlProduct.Repository.Config
{
    public class Produto : IEntityTypeConfiguration<Models.Produto>
    {
        public void Configure(EntityTypeBuilder<Models.Produto> builder)
        {
            builder
                .ToTable("produto", "ControlProduct");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("cd_produto");

            builder
                .Property(p => p.CategoriaId)
                .HasColumnName("cd_categoria");

            builder
                .Property(p => p.Nome)
                .HasColumnName("nm_produto")
                .HasMaxLength(60);

            builder
                .Property(p => p.Valor)
                .HasColumnName("vl_produto")
                .HasConversion(
                    p => (decimal)p,
                    p => decimal.ToDouble(p)
                );

            builder
                .Property(p => p.Quantidade)
                .HasColumnName("qt_produto");

            builder
                .Property(p => p.Ativo)
                .HasColumnName("ic_ativo")
                .HasConversion(
                    p => (char)p,
                    p => (EstadoProduto)p
                );

            builder
                .HasOne(p => p.Categoria).WithMany(p => p.Produtos).HasForeignKey(p => p.CategoriaId);
        }
    }
}
