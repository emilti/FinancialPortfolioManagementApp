using FinancialPortfolioManagementApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace FinancialPortfolioManagementApp.Application.Contracts
{
    public interface IHoldingRepository
    {
        public Holding Get(Guid UserId, Guid AssetId);

        IQueryable<Holding> GetByUserId(Guid userId);

        Task<IDbContextTransaction> BeginTransactionAsync();
       
        void Add(Holding holding);

        void Update(Holding holding);

        void Delete(Holding holding);

        Task SaveAsync();
    }
}
