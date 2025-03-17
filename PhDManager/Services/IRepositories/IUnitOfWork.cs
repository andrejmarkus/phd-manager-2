namespace PhDManager.Services.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IThesisRepository Theses { get; }
        IRegistrationRepository Registrations { get; }
        IStudyProgramRepository StudyPrograms { get; }
        ISubjectRepository Subjects { get; }
        ICommentRepository Comments { get; }
        IIndividualPlanRepository IndividualPlans { get; }
        IAddressRepository Addresses { get; }
        IAdminRepository Admins { get; }
        IStudentRepository Students { get; }
        ITeacherRepository Teachers { get; }
        ISystemStateRepository SystemState { get; }
        IDepartmentRepository Departments { get; }
        IIndividualPlanSubjectRepository IndividualPlanSubjects { get; }
        ISubjectsExamApplicationRepository SubjectsExamApplications { get; }
        IExamApplicationRepository ExamApplications { get; }
        IStudentEvaluationRepository StudentEvaluations { get; }
        IExamSupervisorRepository ExamSupervisors { get; }
        IDissertationDefenseSupervisorRepository DissertationDefenseSupervisors { get; }
        IDissertationDefenseApplicationRepository DissertationDefenseApplications { get; }
        Task CompleteAsync();
    }
}
