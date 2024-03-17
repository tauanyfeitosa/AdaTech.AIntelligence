namespace AdaTech.AIntelligence.Service.Exceptions
{
    /// <summary>
    /// Represents an exception thrown when an invalid description is encountered.
    /// </summary>
    public class InvalidDescriptionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDescriptionException"/> class.
        /// </summary>
        public InvalidDescriptionException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDescriptionException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidDescriptionException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDescriptionException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public InvalidDescriptionException(string message, Exception inner) : base(message, inner) { }
    }
}
