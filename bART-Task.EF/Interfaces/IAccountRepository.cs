using bART_Task.Core.Entities;

namespace bART_Task.EF.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountAsync(int accountId);
        Task<IList<Account>> GetAccountsAsync();
        Task<Account> CreateAccountAsync(Account account, int contactId);
        Task DeleteAccountAsync(int accountId);
        Task<Account> LinkContactIntoAccount(int accountId, int cintactId);
    }
}
