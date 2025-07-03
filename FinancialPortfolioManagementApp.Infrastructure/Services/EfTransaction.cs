using FinancialPortfolioManagementApp.Application.Shared.Contracts;
using FinancialPortfolioManagementApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace FinancialPortfolioManagementApp.Infrastructure.Services
{
    internal class EfTransaction : ITransaction
    {
        private readonly FinancialPortfolioManagementAppDbContext _dbContext;

        private readonly IDbContextTransaction _transaction;

        public EfTransaction(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _transaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken)
        {
            await _transaction.RollbackAsync(cancellationToken);
        }

        public async ValueTask DisposeAsync()
        {
            await _transaction.DisposeAsync();
        }
    }
}
