namespace AdaTech.AIntelligence.Entities
{
    public class UserAuth
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsLogged { get; set; } = false;
    }
}
