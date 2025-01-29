namespace PhDManager.Services.IRepositories
{
    public interface IUnitOfWork
    {
        IThesisRepository Theses { get; }
        IRegistrationRepository Registrations { get; }
        IStudyProgramRepository StudyPrograms { get; }
        ISubjectRepository Subjects { get; }
        ICommentRepository Comments { get; }
        IIndividualPlanRepository IndividualPlans { get; }
        IAddressRepository Addresses { get; }
        Task CompleteAsync();
    }
}
