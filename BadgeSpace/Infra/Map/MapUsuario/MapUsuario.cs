using System.Data.Entity.ModelConfiguration;

namespace Infra.Map.MapUsuario
{
    public class MapUsuario : EntityTypeConfiguration<Domain.Entidades.Usuario.Usuario>
    {
        public MapUsuario()
        {
            ToTable("Usuario");

            Property(p => p.Id).HasColumnName("UsuarioId").IsRequired();
            Property(p => p.Nome).HasMaxLength(100).IsRequired();
            Property(p => p.CPFouCNPJ).HasMaxLength(20).IsRequired().IsUnicode();
            Property(p => p.Email).HasMaxLength(100).IsRequired();
            Property(p => p.Senha).HasMaxLength(100).IsRequired();
        }
    }
}
