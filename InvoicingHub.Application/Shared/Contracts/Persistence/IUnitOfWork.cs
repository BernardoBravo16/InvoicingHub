namespace InvoicingHub.Application.Shared.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        void Save();
    }
}