using AdaTech.AIntelligence.Entities.Enums;

namespace AdaTech.AIntelligence.Entities.Objects
{
    /// <summary>
    /// Class to represent the user authentication with properties to acess information about the user
    /// </summary>
    public class UserInfo : UserAuth
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CPF { get; set; }
        public DateTime DateBirth { get; set; }
    }
}
