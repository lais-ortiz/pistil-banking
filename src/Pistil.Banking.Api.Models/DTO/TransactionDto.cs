namespace Pistil.Banking.Api.Models.DTO
{
    public class TransactionDto
    {
        public long OriginAccountId { get; set; }
        public long? DestinationAccountId { get; set; }
        public decimal Balance { get; set; }
    }
}
