namespace FinancialPortfolioManagementApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public User(string email) 
        {
            Id = Guid.NewGuid();
            Email = email;
        }

        // Factory method
        //public static User Create(string email)
        //{
        //    if (string.IsNullOrWhiteSpace(email))
        //    {
        //        throw new("Email is required");

        //    }

        //    return new User
        //    {
        //        Id = Guid.NewGuid(),
        //        Email = email,
        //    };
        //}
    }
}
