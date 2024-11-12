namespace PhDManager.Services.IRepositories
{
    public interface IUnitOfWork
    {
        IThesisRepository Theses { get; }
        IRegistrationRepository Registrations { get; }
        IStudyProgramRepository StudyPrograms { get; }
        Task CompleteAsync();
    }
}
