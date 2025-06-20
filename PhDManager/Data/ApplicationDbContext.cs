﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhDManager.Models;
using PhDManager.Models.Enums;
using PhDManager.Models.Roles;

namespace PhDManager.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Thesis> Theses { get; init; }
        public DbSet<Subject> Subjects { get; init; }
        public DbSet<StudyProgram> StudyPrograms { get; init; }
        public DbSet<Invitation> Invitations { get; init; }
        public DbSet<IndividualPlan> IndividualPlans { get; init; }
        public DbSet<Comment> Comments { get; init; }
        public DbSet<Address> Addresses { get; init; }
        public DbSet<Admin> Admins { get; init; }
        public DbSet<Student> Students { get; init; }
        public DbSet<ExternalTeacher> ExternalTeachers { get; init; }
        public DbSet<Teacher> Teachers { get; init; }
        public DbSet<SystemState> SystemState { get; init; }
        public DbSet<Department> Departments { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SystemState>()
                .HasData(new SystemState { Id = 1, IsOpen = true });

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Admin)
                .WithOne(a => a.User)
                .HasForeignKey<Admin>();

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Teacher)
                .WithOne(t => t.User)
                .HasForeignKey<Teacher>(t => t.UserId);

            builder.Entity<Teacher>()
                .HasDiscriminator<string>("TeacherType")
                .HasValue<Teacher>("Teacher")
                .HasValue<ExternalTeacher>("ExternalTeacher");

            builder.Entity<Teacher>()
                .HasMany(t => t.Examiners)
                .WithOne(ea => ea.Examiner);

            builder.Entity<Teacher>()
                .HasMany(t => t.Chairpersons)
                .WithOne(ea => ea.Chairperson);

            builder.Entity<Teacher>()
                .HasMany(t => t.AcademicCommitteeMembers)
                .WithOne(ea => ea.AcademicCommitteeMember);

            builder.Entity<Teacher>()
                .HasMany(t => t.AdditionalMembers)
                .WithOne(ea => ea.AdditionalMember);

            builder.Entity<Student>()
                .HasOne(s => s.SubjectsExamApplication)
                .WithOne(s => s.Student)
                .HasForeignKey<SubjectsExamApplication>();

            builder.Entity<Student>()
                .HasOne(s => s.ExamApplication)
                .WithOne(ea => ea.Student)
                .HasForeignKey<ExamApplication>();

            builder.Entity<Student>()
                .HasOne(s => s.StudentEvaluation)
                .WithOne(se => se.Student)
                .HasForeignKey<StudentEvaluation>();

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

            builder.Entity<Student>()
                .HasOne(s => s.ExamSupervisor)
                .WithOne(ea => ea.Student)
                .HasForeignKey<ExamSupervisor>();

            builder.Entity<Student>()
                .HasOne(s => s.DissertationDefenseSupervisor)
                .WithOne(dds => dds.Student)
                .HasForeignKey<DissertationDefenseSupervisor>();

            builder.Entity<Student>()
                .HasOne(s => s.DissertationDefenseApplication)
                .WithOne(dda => dda.Student)
                .HasForeignKey<DissertationDefenseApplication>();

            builder.Entity<Teacher>()
                .HasMany(t => t.SpecialistTheses)
                .WithOne(t => t.SupervisorSpecialist)
                .HasForeignKey("SupervisorSpecialistId");

            builder.Entity<IndividualPlan>()
                .HasMany(ip => ip.Subjects)
                .WithMany(s => s.IndividualPlans)
                .UsingEntity<IndividualPlanSubject>(
                    l => l.HasOne(e => e.Subject).WithMany(s => s.IndividualPlanSubjects),
                    r => r.HasOne(e => e.IndividualPlan).WithMany(ip => ip.IndividualPlanSubjects));

            builder.Entity<StudyProgram>()
                .HasData(
                    new StudyProgram
                    {
                        Id = 1,
                        Code = "AI",
                        Name = "Aplikovaná informatika",
                        StudyFieldName = "Informatika",
                        ThesisSubjects =
                        [
                            "Matematické princípy informatiky",
                            "Teória a metodológia aplikovanej informatiky",
                            "Predmet špecializácie"
                        ]
                    },
                    new StudyProgram
                    {
                        Id = 2,
                        Code = "M",
                        Name = "Manažment",
                        StudyFieldName = "Ekonómia a manažment",
                        ThesisSubjects =
                        [
                            "Metodológia výskumu v manažmente",
                            "Manažérske teórie",
                            "Predmet špecializácie"
                        ]
                    }
                );

            builder.Entity<Subject>()
                .HasData(
                    new Subject { Id = 1, Variant = 'A', Name = "Matematické princípy informatiky - A: Deterministické metódy", Semester = Semester.Winter, StudyProgramId = 1 },
                    new Subject { Id = 2, Variant = 'B', Name = "Matematické princípy informatiky - B: Stochastické metódy", Semester = Semester.Winter, StudyProgramId = 1 },
                    new Subject { Id = 3, Variant = 'A', Name = "Teória a metodológia aplikovanej informatiky - A: Znalostné systémy a algoritmy", Semester = Semester.Summer, StudyProgramId = 1 },
                    new Subject { Id = 4, Variant = 'B', Name = "Teória a metodológia aplikovanej informatiky - B: Výpočtová inteligencia", Semester = Semester.Summer, StudyProgramId = 1 },
                    new Subject { Id = 5, Name = "Predmet špecializácie", Semester = Semester.Summer, StudyProgramId = 1 },
                    new Subject { Id = 6, Name = "Manažérske teórie", Semester = Semester.Winter, StudyProgramId = 2 },
                    new Subject { Id = 7, Name = "Metodológia výskumu v manažmente", Semester = Semester.Winter, StudyProgramId = 2 },
                    new Subject { Id = 8, Name = "Predmet špecializácie", Semester = Semester.Summer, StudyProgramId = 2 }
                );
        }
    }
}
