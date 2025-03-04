using Microsoft.EntityFrameworkCore;
using PhDManager.Data;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly SchoolYearService _schoolYearService;

        public IThesisRepository Theses { get; private set; }
        public IRegistrationRepository Registrations { get; private set; }
        public IStudyProgramRepository StudyPrograms { get; private set; }
        public ISubjectRepository Subjects { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IIndividualPlanRepository IndividualPlans { get; private set; }
        public IAddressRepository Addresses { get; private set; }
        public IAdminRepository Admins { get; private set; }
        public IStudentRepository Students { get; private set; }
        public IExternalRepository Externals { get; private set; }
        public ITeacherRepository Teachers { get; private set; }
        public ISystemStateRepository SystemState { get; private set; }

        public UnitOfWork(IDbContextFactory<ApplicationDbContext> contextFactory, ILoggerFactory loggerFactory, SchoolYearService schoolYearService)
        {
            _context = contextFactory.CreateDbContext();
            _logger = loggerFactory.CreateLogger("logs");
            _schoolYearService = schoolYearService;

            Theses = new ThesisRepository(_context, _logger);
            Registrations = new RegistrationRepository(_context, _logger);
            StudyPrograms = new StudyProgramRepository(_context, _logger);
            Subjects = new SubjectRepository(_context, _logger);
            Comments = new CommentRepository(_context, _logger);
            IndividualPlans = new IndividualPlanRepository(_context, _logger);
            Addresses = new AddressRepository(_context, _logger);
            Admins = new AdminRepository(_context, _logger);
            Students = new StudentRepository(_context, _logger, _schoolYearService);
            Externals = new ExternalRepository(_context, _logger);
            Teachers = new TeacherRepository(_context, _logger);
            SystemState = new SystemStateRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
