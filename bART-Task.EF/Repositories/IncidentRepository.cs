using bART_Task.Core.Entities;
using bART_Task.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bART_Task.EF.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly bARTTaskContext _dbContext;
        private readonly IAccountRepository _accountRepository;
        public IncidentRepository(
            bARTTaskContext dbContext, 
            IAccountRepository accountRepository)
        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;
        }

        public async Task<Incident> CreateIncidentAsync(Incident incidentDto, string email)
        {
            Account account = await _dbContext.Set<Account>()
                                    .FirstOrDefaultAsync(a => a.Name == incidentDto.Account.Name)
                                    ?? throw new AccountNotFoundException($"Account with Name: {incidentDto.Account.Name} not found");

            Contact contact = await _dbContext.Set<Contact>()
                                    .FirstOrDefaultAsync(n => n.Email == email) 
                                    ?? throw new Exception($"Contact with Email: {email} not found");

            if(contact.AccountId is null)
                await _accountRepository.LinkContactIntoAccount(account.Id, contact.Id);

            Incident incident = new Incident()
            {
                Description = incidentDto.Description,
                AccountId = account.Id,
            };

            var createdIncident = await _dbContext.Set<Incident>().AddAsync(incident) ?? throw new Exception($"Incident {incident.Name} not created");
            await _dbContext.SaveChangesAsync();

            return createdIncident.Entity;
        }

        public async Task<Incident> GetIncidentAsync(string incidentId)
        {
            Incident incident = await _dbContext.Set<Incident>()
                                    .Include(o => o.Account!)
                                    .FirstOrDefaultAsync(o => o.Name == incidentId)
                                    ?? throw new Exception($"Incident with Id: {incidentId} not found");

            return incident;
        }

        public async Task<IList<Incident>> GetIncidentsAsync()
        {
            List<Incident> incidents = await _dbContext.Set<Incident>()
                                               .Include(o => o.Account!)
                                               .OrderBy(o => o.Name).ToListAsync()
                                               ?? throw new Exception($"Incidents not found");

            return incidents;
        }
    }
}
