using Pistil.Banking.Domain.Enums;

namespace Pistil.Banking.Domain.Entities
{
    public class Account : EntityBase
    {
        public long UserId { get; set; }
        public AccountType AccountType { get; set; }
        public int Agency { get; set; }
        public int AccountNumber { get; set; }
        public short Digit { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
    }
}
