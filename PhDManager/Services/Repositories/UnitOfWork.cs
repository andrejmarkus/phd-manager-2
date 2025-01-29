using PhDManager.Data;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public IThesisRepository Theses { get; private set; }
        public IRegistrationRepository Registrations { get; private set; }
        public IStudyProgramRepository StudyPrograms { get; private set; }
        public ISubjectRepository Subjects { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IIndividualPlanRepository IndividualPlans { get; private set; }
        public IAddressRepository Addresses { get; private set; }

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Theses = new ThesisRepository(_context, _logger);
            Registrations = new RegistrationRepository(_context, _logger);
            StudyPrograms = new StudyProgramRepository(_context, _logger);
            Subjects = new SubjectRepository(_context, _logger);
            Comments = new CommentRepository(_context, _logger);
            IndividualPlans = new IndividualPlanRepository(_context, _logger);
            Addresses = new AddressRepository(_context, _logger);
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
