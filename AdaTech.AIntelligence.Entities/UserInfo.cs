namespace AdaTech.AIntelligence.Entities
{
    public class UserInfo : UserAuth
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string CPF { get; set; }
        public DateTime DateBirth { get; set; }
    }
}
