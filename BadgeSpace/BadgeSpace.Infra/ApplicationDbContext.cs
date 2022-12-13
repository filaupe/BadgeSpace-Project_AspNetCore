using BadgeSpace.Models.User;
using BadgeSpace.Models.User.Certification;
using BadgeSpace.Models.User.Certification.Empress;
using BadgeSpace.Models.User.Certification.Empress.Course;
using BadgeSpace.Models.User.Certification.Student;
using Microsoft.EntityFrameworkCore;

namespace BadgeSpace.Infra
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<EmpressModel> Empresses { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<CertificationModel> Certifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetData();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetData();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetData()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added) entry.Property("CreationDate").CurrentValue = DateTime.Now;
                if (entry.State == EntityState.Modified) entry.Property("ChangeDate").CurrentValue = DateTime.Now;
            }
        }
    }
}
