using bART_Task.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART_Task.EF.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> GetContactAsync(int contactId);
        Task<IList<Contact>> GetContactsAsync();
        Task<Contact> CreateContactAsync(Contact contact);
        Task DeleteContactAsync(int contactId);
    }
}
