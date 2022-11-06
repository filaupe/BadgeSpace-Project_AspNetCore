using System.Data.Entity.ModelConfiguration;

namespace Infra.Map.MapEstudante
{
    public class MapEstudante : EntityTypeConfiguration<Domain.Entidades.Estudante.Estudante>
    {
        public MapEstudante()
        {
            ToTable("Estudante");

            Property(p => p.Id).HasColumnName("EstudanteId").IsRequired();
            Property(p => p.Nome).HasMaxLength(100).IsRequired();
            Property(p => p.CPF).HasMaxLength(20).IsRequired();
        }
    }
}
