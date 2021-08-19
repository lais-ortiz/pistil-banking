using Pistil.Banking.Domain.Entities;
using System.Threading.Tasks;

namespace Pistil.Banking.Domain.Interfaces.TransactionsService
{
    public interface ITransactionsService : ITransaction
    {
        Task Deposit(Transaction transaction);
        Task Withdraw(Transaction transaction);
        Task Transfer(Transaction transaction);
    }
}
