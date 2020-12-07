using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlProduct.Repository.Config
{
    public class User: IEntityTypeConfiguration<Models.User>
    {
        public void Configure(EntityTypeBuilder<Models.User> builder)
        {
            builder
                .ToTable("usuario");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("cd_usuario");

            builder
                .Property(p => p.Token)
                .HasColumnName("cd_token");

            builder
                .Property(p => p.Senha)
                .HasColumnName("ds_senha");
        }
    }
}
