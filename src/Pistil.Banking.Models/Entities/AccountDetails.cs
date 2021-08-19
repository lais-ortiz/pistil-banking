namespace Pistil.Banking.Domain.Entities
{
    public class AccountDetails : EntityBase
    {
        public long AccountId { get; set; }
        public decimal BalanceLimit { get; set; }
        public decimal MonthlyBill { get; set; }
        public decimal SavingsBalance { get; set; }
    }
}
