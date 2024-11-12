using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhDManager.Models;

namespace PhDManager.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Thesis> Theses { get; init; }
        public DbSet<Subject> Subjects { get; init; }
        public DbSet<StudyProgram> StudyPrograms { get; init; }
        public DbSet<Registration> Registrations { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Thesis>()
                .HasOne(t => t.Supervisor)
                .WithOne(u => u.SupervisorThesis)
                .HasForeignKey<Thesis>(t => t.SupervisorId);

            base.OnModelCreating(builder);
        }
    }
}
