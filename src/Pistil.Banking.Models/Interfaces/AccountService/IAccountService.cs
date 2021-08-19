using Pistil.Banking.Domain.Entities;
using System.Threading.Tasks;

namespace Pistil.Banking.Domain.Interfaces.AccountService
{
    public interface IAccountService
    {
        Task<Account> Get(long id);
        Task<Account> GetByUser(long userId);
    }
}
