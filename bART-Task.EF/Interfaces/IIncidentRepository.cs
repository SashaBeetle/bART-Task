using bART_Task.Core.Entities;

namespace bART_Task.EF.Interfaces
{
    public interface IIncidentRepository
    {
        Task<Incident> GetIncidentAsync(string incidentId);
        Task<IList<Incident>> GetIncidentsAsync();
        Task<Incident> CreateIncidentAsync(Incident incident, string email);
    }
}
