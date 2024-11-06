namespace PhDManager.IRepositories
{
    public interface IUnitOfWork
    {
        IThesisRepository Theses { get; }
        Task CompleteAsync();
    }
}
