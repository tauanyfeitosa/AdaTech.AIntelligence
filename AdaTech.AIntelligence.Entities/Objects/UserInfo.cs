namespace AdaTech.AIntelligence.Entities.Objects
{
    public class UserInfo : UserAuth
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CPF { get; set; }
        public DateTime DateBirth { get; set; }
    }
}
