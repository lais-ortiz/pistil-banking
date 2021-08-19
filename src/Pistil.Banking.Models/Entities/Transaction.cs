using Pistil.Banking.Domain.Enums;
using System;

namespace Pistil.Banking.Domain.Entities
{
    public class Transaction : EntityBase
    {
        public long OriginAccountId { get; set; }
        public long DestinationAccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public string Explanation { get; set; }
        public string Location { get; set; }
    }
}
