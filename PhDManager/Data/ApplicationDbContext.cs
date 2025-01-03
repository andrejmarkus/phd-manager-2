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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>()
                .IsRequired();

            builder.Entity<IndividualPlan>()
                .HasOne(ip => ip.User)
                .WithOne(u => u.IndividualPlan)
                .HasForeignKey<ApplicationUser>();

            builder.Entity<Thesis>()
                .HasOne(t => t.IndividualPlan)
                .WithOne(ip => ip.Thesis)
                .HasForeignKey<IndividualPlan>();

            builder.Entity<StudyProgram>()
                .HasData(
                    new StudyProgram { Id = 1, Code = "AI", Name = "Aplikovaná informatika" },
                    new StudyProgram { Id = 2, Code = "MAN", Name = "Manament" }
                );

            builder.Entity<Subject>()
                .HasData(
                    new Subject { Id = 1, Code = "MPI", Name = "Matematické princípy informatiky", Semester = "zimnı", Credits = 5, StudyProgramId = 1 },
                    new Subject { Id = 2, Code = "TM AI", Name = "Teória a metodológia aplikovanej informatiky", Semester = "zimnı", Credits = 5, StudyProgramId = 1 },
                    new Subject { Id = 3, Code = "JAD1", Name = "Jazyk anglickı PhD. 1", Semester = "zimnı", Credits = 5, StudyProgramId = 1 },
                    new Subject { Id = 4, Code = "DPR1", Name = "Dizertaènı projekt 1", Semester = "zimnı", Credits = 5, StudyProgramId = 1 },
                    new Subject { Id = 5, Code = "VPub1", Name = "Vedecká a publikaèná èinnos 1", Semester = "letnı", Credits = 10, StudyProgramId = 1 },
                    new Subject { Id = 6, Code = "JAD2", Name = "Jazyk anglickı PhD. 2", Semester = "letnı", Credits = 5, StudyProgramId = 1 },
                    new Subject { Id = 7, Code = "DPR2", Name = "Dizertaènı projekt 2", Semester = "letnı", Credits = 5, StudyProgramId = 1 },
                    new Subject { Id = 8, Code = "ManT", Name = "Manaérske teórie", Semester = "zimnı", Credits = 5, StudyProgramId = 2 },
                    new Subject { Id = 9, Code = "MVM", Name = "Metodológia vıskumu v manamente", Semester = "zimnı", Credits = 5, StudyProgramId = 2 },
                    new Subject { Id = 10, Code = "JAD1", Name = "Jazyk anglickı PhD. 1", Semester = "zimnı", Credits = 5, StudyProgramId = 2 },
                    new Subject { Id = 11, Code = "DPR1", Name = "Dizertaènı projekt 1", Semester = "zimnı", Credits = 5, StudyProgramId = 2 },
                    new Subject { Id = 12, Code = "VPub1", Name = "Vedecká a publikaèná èinnos 1", Semester = "letnı", Credits = 10, StudyProgramId = 2 },
                    new Subject { Id = 13, Code = "JAD2", Name = "Jazyk anglickı PhD. 2", Semester = "letnı", Credits = 5, StudyProgramId = 2 },
                    new Subject { Id = 14, Code = "DPR2", Name = "Dizertaènı projekt 2", Semester = "letnı", Credits = 5, StudyProgramId = 2 }
                );
        }
    }
}
