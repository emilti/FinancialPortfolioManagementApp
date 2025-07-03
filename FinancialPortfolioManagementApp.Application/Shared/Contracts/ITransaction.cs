namespace FinancialPortfolioManagementApp.Application.Shared.Contracts
{
    public interface ITransaction
    {
        Task CommitAsync(CancellationToken cancellationToken);

        Task RollbackAsync(CancellationToken cancellationToken);
    }
}
