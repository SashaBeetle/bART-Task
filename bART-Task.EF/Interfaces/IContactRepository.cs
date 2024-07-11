using bART_Task.Core.Entities;

namespace bART_Task.EF.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> GetContactAsync(int contactId);
        Task<IList<Contact>> GetContactsAsync();
        Task<Contact> CreateContactAsync(Contact contact);
    }
}
