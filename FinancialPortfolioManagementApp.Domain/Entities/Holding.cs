﻿namespace FinancialPortfolioManagementApp.Domain.Entities
{
    public class Holding
    {
        public Guid Id { get; set; }

        public decimal Quantity { get; set; }

        public required Guid AssetId { get; set; }

        public Asset? Asset { get; set; }

        public required Guid UserId { get; set; }

        public User? User { get; set; }
    }
}
