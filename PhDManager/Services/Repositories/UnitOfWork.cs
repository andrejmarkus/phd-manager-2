using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IThesisRepository Theses { get; private set; }
        public IRegistrationRepository Registrations { get; private set; }
        public IStudyProgramRepository StudyPrograms { get; private set; }
        public ISubjectRepository Subjects { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IIndividualPlanRepository IndividualPlans { get; private set; }
        public IAddressRepository Addresses { get; private set; }
        public IAdminRepository Admins { get; private set; }
        public IStudentRepository Students { get; private set; }
        public ITeacherRepository Teachers { get; private set; }
        public ISystemStateRepository SystemState { get; private set; }
        public IDepartmentRepository Departments { get; private set; }
        public IIndividualPlanSubjectRepository IndividualPlanSubjects { get; private set; }
        public ISubjectsExamApplicationRepository SubjectsExamApplications { get; private set; }
        public IExamApplicationRepository ExamApplications { get; private set; }
        public IStudentEvaluationRepository StudentEvaluations { get; private set; }
        public IExamSupervisorRepository ExamSupervisors { get; private set; }
        public IDissertationDefenseSupervisorRepository DissertationDefenseSupervisors { get; private set; }
        public IDissertationDefenseApplicationRepository DissertationDefenseApplications { get; private set; }

        public UnitOfWork(IDbContextFactory<ApplicationDbContext> contextFactory, ILoggerFactory loggerFactory)
        {
            _context = contextFactory.CreateDbContext();
            var logger = loggerFactory.CreateLogger("logs");

            Theses = new ThesisRepository(_context, logger);
            Registrations = new RegistrationRepository(_context, logger);
            StudyPrograms = new StudyProgramRepository(_context, logger);
            Subjects = new SubjectRepository(_context, logger);
            Comments = new CommentRepository(_context, logger);
            IndividualPlans = new IndividualPlanRepository(_context, logger);
            Addresses = new AddressRepository(_context, logger);
            Admins = new AdminRepository(_context, logger);
            Students = new StudentRepository(_context, logger);
            Teachers = new TeacherRepository(_context, logger);
            SystemState = new SystemStateRepository(_context, logger);
            Departments = new DepartmentRepository(_context, logger);
            IndividualPlanSubjects = new IndividualPlanSubjectRepository(_context, logger);
            SubjectsExamApplications = new SubjectsExamApplicationRepository(_context, logger);
            ExamApplications = new ExamApplicationRepository(_context, logger);
            StudentEvaluations = new StudentEvaluationRepository(_context, logger);
            ExamSupervisors = new ExamSupervisorRepository(_context, logger);
            DissertationDefenseSupervisors = new DissertationDefenseSupervisorRepository(_context, logger);
            DissertationDefenseApplications = new DissertationDefenseApplicationRepository(_context, logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
