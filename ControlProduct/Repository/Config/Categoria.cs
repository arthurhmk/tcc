using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlProduct.Repository.Config
{
    public class Categoria: IEntityTypeConfiguration<Models.Categoria>
    {
        public void Configure(EntityTypeBuilder<Models.Categoria> builder)
        {
            builder
                .ToTable("categoria");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("cd_categoria");

            builder
                .Property(p => p.Nome)
                .HasColumnName("nm_categoria")
                .HasMaxLength(30);

            builder
                .Property(p => p.SuperCategoriaId)
                .HasColumnName("cd_subcategoria");

            builder.
                HasOne(p => p.SuperCategoria).WithMany(p => p.SubCategorias).HasForeignKey(p => p.SuperCategoriaId);

            builder.
                HasMany(p => p.SubCategorias).WithOne(p => p.SuperCategoria).HasForeignKey(p => p.SuperCategoriaId);
        }
    }
}
