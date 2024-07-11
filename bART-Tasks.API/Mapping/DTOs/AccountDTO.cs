namespace bART_Tasks.API.Mapping.DTOs
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<IncidentDTO> Incidents { get; set; }
        public ICollection<ContactDTO> Contacts { get; set; }
    }
}
