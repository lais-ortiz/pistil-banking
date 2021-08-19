namespace Pistil.Banking.Domain.Interfaces.TransactionsService
{
    public interface ITransaction
    {
        void EnsurePositiveBalances(decimal[] balances);
    }
}
