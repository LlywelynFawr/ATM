using ATMBlazorApp.DAL;

namespace ATMBlazorApp.Services
{
    public interface IAccountServices
    {
        Task<Account> GetAccount(string Pin, string CardNumber);
        Task<List<Account>> GetAllAccounts();
        Task<Account> CreateAccount(string Pin, string CardNumber, decimal Amount);

        Task<Account> UpdateAccount(string Pin, string CardNumber, decimal Amount);

    }
}
