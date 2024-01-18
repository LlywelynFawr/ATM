using Microsoft.EntityFrameworkCore;
using ATMBlazorApp.DAL;
using System;
using System.Net.NetworkInformation;

namespace ATMBlazorApp.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IDbContextFactory<ATMContext> _dbContextFactory;

        public AccountServices(IDbContextFactory<ATMContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Account> CreateAccount(string cardNumber, string pin, decimal amount)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var account = new Account() { Pin = pin, CardNumber = cardNumber, Amount = amount };
            await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            return account;
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            using var context = _dbContextFactory.CreateDbContext();

            var accounts = await context.Accounts.ToListAsync();
            return accounts;
        }

        public async Task<Account> GetAccount(string cardNumber, string pin)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var account = await context.Accounts
                .Where(account => account.Pin == pin)
                .Where(account => account.CardNumber == cardNumber)
                .FirstOrDefaultAsync();
            return account;
        }

        public async Task<Account> UpdateAccount(string cardNumber, string pin, decimal amount)
        {
            using var context = _dbContextFactory.CreateDbContext();
                var account = await context.Accounts
                .Where(account => account.Pin == pin)
                .Where(account => account.CardNumber == cardNumber)
                .FirstOrDefaultAsync();
                 account.Amount = amount;
                 await context.SaveChangesAsync();
                 return account;
        }

    }
}