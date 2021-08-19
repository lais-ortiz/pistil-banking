using Pistil.Banking.Data.Repositories;
using Pistil.Banking.Domain.Entities;
using Pistil.Banking.Domain.Interfaces.TransactionsService;
using RatesExchangeApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pistil.Banking.Services.TransactionsService
{
    public class TransactionsService : ServiceBase<Transaction>, ITransactionsService
    {
        private readonly IRepositoryBase<Transaction> _repositoryBase;
        private readonly IAccountRepository _accountRepository;

        private readonly RatesExchangeApiService _ratesExchangeApiService;

        public TransactionsService(
            IRepositoryBase<Transaction> repositoryBase,
            IAccountRepository accountRepository,
            RatesExchangeApiService ratesExchangeApiService
        ) : base(repositoryBase)
        {
            _repositoryBase = repositoryBase;
            _accountRepository = accountRepository;
            _ratesExchangeApiService = ratesExchangeApiService;
        }

        public async Task Deposit(Transaction transaction)
        {
            try
            {
                var destinationAccount = await _accountRepository.GetByIdAsync(transaction.DestinationAccountId)
                    ?? throw new ArgumentNullException("Destination account not found.");

                EnsurePositiveBalances(new decimal[] { destinationAccount.Balance });

                var destinationBalance = await GetBalanceByCurrency(transaction.Currency, destinationAccount.Currency, transaction.Balance);

                ExecuteDeposit(destinationAccount, destinationBalance);

                await _repositoryBase.AddAsync(transaction);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on Deposit: {ex.Message}");
            }
        }

        public async Task Transfer(Transaction transaction)
        {
            var originAccount = await _accountRepository.GetByIdAsync(transaction.OriginAccountId)
                ?? throw new ArgumentNullException("Origin account not found.");

            var destinationAccount = await _accountRepository.GetByIdAsync(transaction.OriginAccountId)
                ?? throw new ArgumentNullException("Destination account not found.");

            EnsurePositiveBalances(new decimal[] { originAccount.Balance, destinationAccount.Balance });

            var destinationBalance = await GetBalanceByCurrency(originAccount.Currency, destinationAccount.Currency, transaction.Balance);

            ExecuteTransfer(originAccount, destinationAccount, destinationBalance);

            await _repositoryBase.AddAsync(transaction);
        }

        public async Task Withdraw(Transaction transaction)
        {
            var originAccount = await _accountRepository.GetByIdAsync(transaction.OriginAccountId)
                ?? throw new ArgumentNullException("Origin account not found.");

            EnsurePositiveBalances(new decimal[] { originAccount.Balance });

            ExecuteWithdraw(originAccount, transaction.Balance);

            await _repositoryBase.AddAsync(transaction);
        }

        public void EnsurePositiveBalances(decimal[] balances)
        {
            if (balances.Any(b => b < 0)) 
                throw new Exception("Transaction not allowed. The balance is negative.");
        }

        private async Task<decimal> GetBalanceByCurrency(string originCurrency, string destinationCurrency, decimal balance)
        {
            var rates = await _ratesExchangeApiService.ConvertCurrency(originCurrency, balance.ToString(), DateTime.UtcNow.ToString());
            rates.Rates.TryGetValue(destinationCurrency, out decimal destinationBalance);
            return destinationBalance;
        }

        private void ExecuteDeposit(Account destinationAccount, decimal balance)
        {
            destinationAccount.Balance += balance;
            _accountRepository.UpdateAsync(destinationAccount);
        }

        private void ExecuteTransfer(Account originAccount, Account destinationAccount, decimal balance)
        {
            originAccount.Balance -= balance;
            _accountRepository.UpdateAsync(originAccount);

            destinationAccount.Balance += balance;
            _accountRepository.UpdateAsync(destinationAccount);
        }

        private void ExecuteWithdraw(Account originAccount, decimal balance)
        {
            originAccount.Balance -= balance;
            _accountRepository.UpdateAsync(originAccount);
        }
    }
}
