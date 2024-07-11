using bART_Task.Core.Entities;
using bART_Task.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bART_Task.EF.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly bARTTaskContext _dbContext;
        private readonly IDbEntityService<Account> _dbAccountService;
        private readonly IDbEntityService<Contact> _dbContactService;

        public AccountRepository(bARTTaskContext dbContext, IDbEntityService<Account> dbAccountService, IDbEntityService<Contact> dbContactService)
        {
            _dbContext = dbContext;
            _dbAccountService = dbAccountService;
            _dbContactService = dbContactService;
        }

        public async Task<Account> CreateAccountAsync(Account account, int contactId)
        {
            Contact contact = _dbContactService.GetByIdforUser(contactId) ?? throw new Exception($"Contact with Id: {contactId} not found");
            account.Contacts = new List<Contact> { contact };

            Account createdAccount = await _dbAccountService.Create(account) ?? throw new Exception($"Account {account.Name} not created");

            return createdAccount;
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            Account account = _dbAccountService.GetByIdforUser(accountId) ?? throw new Exception($"Account with Id: {accountId} not found");

            await _dbAccountService.Delete(account);
        }

        public async Task<Account> GetAccountAsync(int accountId)
        {
            Account account = await _dbContext.Set<Account>()
            .Include(o => o.Incidents!)
            .Include(p => p.Contacts!)
            .FirstOrDefaultAsync(o => o.Id == accountId)
            ?? throw new Exception($"Account with Id: {accountId} not found");

            return account;
        }

        public async Task<IList<Account>> GetAccountsAsync()
        {
            List<Account> account = await _dbContext.Set<Account>()
                        .Include(o => o.Incidents!)
                        .Include(p => p.Contacts!)
                        .OrderBy(o => o.Id).ToListAsync()
                        ?? throw new Exception($"Accounts not found");

            return account;
        }

        public async Task<Account> LinkContactIntoAccount(int accountId, int cintactId)
        {
            Contact contact = _dbContactService.GetByIdforUser(cintactId) ?? throw new Exception($"Contact with Id: {cintactId} not found");
            Account account = await _dbContext.Set<Account>()
                                .Include(p => p.Contacts!)
                                .FirstOrDefaultAsync(o => o.Id == accountId) ?? throw new Exception($"Account with Id: {accountId} not found");

            account.Contacts.Add(contact);

            Account updatedAccount = await _dbAccountService.Update(account) ?? throw new Exception($"Account {account.Name} not updated");

            return updatedAccount;
        }
    }
}
