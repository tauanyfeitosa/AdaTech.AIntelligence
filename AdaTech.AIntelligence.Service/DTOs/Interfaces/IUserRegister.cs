namespace AdaTech.AIntelligence.Service.DTOs.Interfaces
{
    /// <summary>
    /// Represents the interface for user registration data.
    /// </summary>
    public interface IUserRegister
    {
        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the user.
        /// </summary>
        public DateOnly DateBirth { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the CPF (Brazilian individual taxpayer registry) of the user.
        /// </summary>
        public string CPF { get; set; }
    }
}
