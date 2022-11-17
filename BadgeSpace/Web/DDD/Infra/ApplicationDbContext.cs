using Domain_Driven_Design.Domain.Entidades.Estudante;
using Domain_Driven_Design.Domain.Entidades.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain_Driven_Design.Infra
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estudante> Estudantes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);    

            base.OnModelCreating(modelBuilder); 
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            setData();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            setData();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void setData()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added) entry.Property("DataInclusao").CurrentValue = DateTime.Now;
                if (entry.State == EntityState.Modified) entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
            }
        }
    }
}
