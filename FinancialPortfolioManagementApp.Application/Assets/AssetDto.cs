using FinancialPortfolioManagementApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialPortfolioManagementApp.Application.Assets
{
    public class AssetDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public Decimal CurrentMarketPrice { get; set; }

        public ICollection<AssetTransaction> Transactions { get; set; } = new List<AssetTransaction>();

        public ICollection<Holding> Holdings { get; set; } = new List<Holding>();
    }
}
