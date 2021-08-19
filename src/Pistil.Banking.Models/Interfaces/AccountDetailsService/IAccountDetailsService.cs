using Pistil.Banking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pistil.Banking.Domain.Interfaces.AccountDetailsService
{
    public interface IAccountDetailsService
    {
        Task<AccountDetails> Get(long id);
    }
}
