using System.ComponentModel.DataAnnotations;

namespace FinancialPortfolioManagementApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public ICollection<AssetTransaction> AssetTransactions { get; set; } = new List<AssetTransaction>();

        public ICollection<Holding> Holdings { get; set; } = new List<Holding>();

        public User(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty", nameof(email));
            }

            Email = email;
        }
    }
}
