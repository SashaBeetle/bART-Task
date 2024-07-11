namespace bART_Task.Core.Entities
{
    public class Contact : DbItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? AccountId { get; set; }
        public Account Account { get; set; }
    }
}
