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
        public DbSet<IndividualPlan> IndividualPlans { get; init; }
        public DbSet<Comment> Comments { get; init; }
        public DbSet<Address> Addresses { get; init; }
        public DbSet<External> Admins { get; init; }
        public DbSet<Student> Students { get; init; }
        public DbSet<External> Externals { get; init; }
        public DbSet<Teacher> Teachers { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Admin)
                .WithOne(a => a.User)
                .HasForeignKey<Admin>();

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Teacher)
                .WithOne(t  => t.User)
                .HasForeignKey<Teacher>();

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.External)
                .WithOne(e => e.User)
                .HasForeignKey<External>();

            builder.Entity<Student>()
                .HasOne(ds => ds.Address)
                .WithOne(a => a.Student)
                .HasForeignKey<Address>()
                .IsRequired();

            builder.Entity<Thesis>()
                .HasMany(t => t.InterestedStudents)
                .WithOne(ds => ds.ThesisInterest);

            builder.Entity<Student>()
                .HasOne(ds => ds.Thesis)
                .WithOne(t => t.Student)
                .HasForeignKey<Thesis>();

            builder.Entity<Teacher>()
                .HasMany(t => t.Theses)
                .WithOne(t => t.Supervisor);

            builder.Entity<Teacher>()
                .HasMany(t => t.SpecialistTheses)
                .WithOne(t => t.SupervisorSpecialist);

            builder.Entity<IndividualPlan>()
                .HasOne(ip => ip.Student)
                .WithOne(ds => ds.IndividualPlan)
                .HasForeignKey<Student>();

            builder.Entity<StudyProgram>()
                .HasData(
                    new StudyProgram
                    {
                        Id = 1,
                        Code = "AI",
                        Name = "Aplikovan� informatika",
                        StudyFieldName = "Informatika",
                        ThesisSubjects =
                        [
                            "Matematick� princ�py informatiky", 
                            "Te�ria a metodol�gia aplikovanej informatiky",
                            "Predmet �pecializ�cie"
                        ]
                    },
                    new StudyProgram
                    {
                        Id = 2,
                        Code = "M",
                        Name = "Mana�ment",
                        StudyFieldName = "Ekon�mia a mana�ment",
                        ThesisSubjects =
                        [
                            "Metodol�gia v�skumu v mana�mente",
                            "Mana��rske te�rie",
                            "Predmet �pecializ�cie"
                        ]
                    }
                );

            builder.Entity<Subject>()
                .HasData(
                    new Subject { Id = 1, Variant = 'A', Name = "Matematick� princ�py informatiky - A: Deterministick� met�dy", Semester = "zimn�", StudyProgramId = 1 },
                    new Subject { Id = 2, Variant = 'B', Name = "Matematick� princ�py informatiky - B: Stochastick� met�dy", Semester = "zimn�", StudyProgramId = 1 },
                    new Subject { Id = 3, Variant = 'A', Name = "Te�ria a metodol�gia aplikovanej informatiky - A: Znalostn� syst�my a algoritmy", Semester = "letn�", StudyProgramId = 1 },
                    new Subject { Id = 4, Variant = 'B', Name = "Te�ria a metodol�gia aplikovanej informatiky - B: V�po�tov� inteligencia", Semester = "letn�", StudyProgramId = 1 },
                    new Subject { Id = 5, Name = "Predmet �pecializ�cie", Semester = "letn�", StudyProgramId = 1 },
                    new Subject { Id = 6, Name = "Mana��rske te�rie", Semester = "zimn�", StudyProgramId = 2 },
                    new Subject { Id = 7, Name = "Metodol�gia v�skumu v mana�mente", Semester = "zimn�", StudyProgramId = 2 },
                    new Subject { Id = 8, Name = "Predmet �pecializ�cie", Semester = "letn�", StudyProgramId = 2 }
                );
        }
    }
}
