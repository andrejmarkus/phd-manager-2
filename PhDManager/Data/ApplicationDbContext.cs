using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhDManager.Models;
using System.Reflection.Emit;

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
                .WithOne(t => t.User)
                .HasForeignKey<Teacher>();

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.External)
                .WithOne(e => e.User)
                .HasForeignKey<External>();

            builder.Entity<Student>()
                .HasOne(s => s.Address)
                .WithOne(a => a.Student)
                .HasForeignKey<Address>()
                .IsRequired();

            builder.Entity<Student>()
                .HasOne(s => s.Thesis)
                .WithOne(t => t.Student)
                .HasForeignKey<Thesis>(t => t.StudentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Student>()
                .HasOne(s => s.IndividualPlan)
                .WithOne(ip => ip.Student)
                .HasForeignKey<IndividualPlan>();

            builder.Entity<Teacher>()
                .HasMany(t => t.SpecialistTheses)
                .WithOne(t => t.SupervisorSpecialist)
                .HasForeignKey("SupervisorSpecialistId");

            builder.Entity<IndividualPlan>()
                .HasMany(ip => ip.Subjects)
                .WithMany(s => s.IndividualPlans);

            builder.Entity<IndividualPlan>()
                .HasMany(ip => ip.OptionalSubjects)
                .WithMany(s => s.OptionalIndividualPlans);

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
                    new Subject { Id = 8, Name = "Predmet �pecializ�cie", Semester = "letn�", StudyProgramId = 2 },

                    new Subject { Id = 9, IsRequired = false, Name = "Predmet �pecializ�cie", Semester = "letn�", StudyProgramId = 2 },
                    new Subject { Id = 10, IsRequired = false, Name = "Predmet �pecializ�cie", Semester = "letn�", StudyProgramId = 2 },
                    new Subject { Id = 11, IsRequired = false, Name = "Predmet �pecializ�cie", Semester = "letn�", StudyProgramId = 2 },
                    new Subject { Id = 12, IsRequired = false, Name = "Predmet �pecializ�cie", Semester = "letn�", StudyProgramId = 2 },
                    new Subject { Id = 13, IsRequired = false, Name = "Predmet �pecializ�cie", Semester = "letn�", StudyProgramId = 2 },
                    new Subject { Id = 14, IsRequired = false, Name = "Predmet �pecializ�cie", Semester = "letn�", StudyProgramId = 2 },
                    new Subject { Id = 15, IsRequired = false, Name = "Predmet �pecializ�cie", Semester = "letn�", StudyProgramId = 2 },
                    new Subject { Id = 16, IsRequired = false, Name = "Predmet �pecializ�cie", Semester = "letn�", StudyProgramId = 2 },
                    new Subject { Id = 17, IsRequired = false, Name = "Predmet �pecializ�cie", Semester = "letn�", StudyProgramId = 2 }
                );
        }
    }
}
