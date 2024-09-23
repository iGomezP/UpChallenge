namespace UpBack.Domain.Abstractions.Dtos
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string ObjectStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
