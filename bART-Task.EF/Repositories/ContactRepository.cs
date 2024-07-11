using bART_Task.Core.Entities;
using bART_Task.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bART_Task.EF.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly bARTTaskContext _dbContext;
        private readonly IDbEntityService<Contact> _dbContactService;

        public ContactRepository(bARTTaskContext dbContext, IDbEntityService<Contact> dbContactService)
        {
            _dbContext = dbContext;
            _dbContactService = dbContactService;
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            Contact createdContact = await _dbContactService.Create(contact) ?? throw new Exception($"Contact {contact.FirstName} not created");

            return createdContact;
        }

        public async Task DeleteContactAsync(int contactId)
        {
            Contact contact = _dbContactService.GetByIdforUser(contactId) ?? throw new Exception($"Contact with Id: {contactId} not found");

            await _dbContactService.Delete(contact);
        }

        public async Task<Contact> GetContactAsync(int contactId)
        {
            Contact contact = await _dbContext.Set<Contact>()
                        .Include(o => o.Account!)
                        .FirstOrDefaultAsync(o => o.Id == contactId)
                        ?? throw new Exception($"Contact with Id: {contactId} not found");

            return contact;
        }

        public async Task<IList<Contact>> GetContactsAsync()
        {
            List<Contact> contacts = await _dbContext.Set<Contact>()
                                    .Include(o => o.Account!)
                                    .OrderBy(o => o.Id).ToListAsync()
                                    ?? throw new Exception($"Contacts not found");

            return contacts;
        }
    }
}
