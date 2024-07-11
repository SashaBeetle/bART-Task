namespace bART_Task.Core.Entities
{
    public class Incident
    {
        public string Name { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
