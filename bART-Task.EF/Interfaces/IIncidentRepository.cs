using bART_Task.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART_Task.EF.Interfaces
{
    public interface IIncidentRepository
    {
        Task<Incident> GetIncidentAsync(string incidentId);
        Task<IList<Incident>> GetIncidentsAsync();
        Task<Incident> CreateIncidentAsync(Incident incident, string email);
        Task DeleteIncidentAsync(string incidentId);
    }
}
