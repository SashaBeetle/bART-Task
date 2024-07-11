using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART_Task.Core.Entities
{
    public class Account : DbItem
    {
        public string Name { get; set; }
        public ICollection<Incident> Incidents { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
