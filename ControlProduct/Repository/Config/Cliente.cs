using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlProduct.Repository.Config
{
    public class Cliente: IEntityTypeConfiguration<Models.Cliente>
    {
        public void Configure(EntityTypeBuilder<Models.Cliente> builder)
        {
            builder
                .ToTable("cliente", "ControlProduct");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("cd_cliente");

            builder
                .Property(p => p.Nome)
                .HasColumnName("nm_cliente")
                .HasMaxLength(60);

            builder
                .Property(p => p.Email)
                .HasColumnName("ds_email")
                .HasMaxLength(30);

            builder
                .Property(p => p.Endereco)
                .HasColumnName("ds_endereco_cliente")
                .HasMaxLength(100);

            builder
                .Property(p => p.Telefone)
                .HasColumnName("ds_telefone")
                .HasMaxLength(60);
        }
    }
}
