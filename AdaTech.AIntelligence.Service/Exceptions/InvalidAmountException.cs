namespace AdaTech.AIntelligence.Service.Exceptions
{
    /// <summary>
    /// Represents an exception thrown when an invalid amount is encountered.
    /// </summary>
    public class InvalidAmountException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAmountException"/> class.
        /// </summary>
        public InvalidAmountException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAmountException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidAmountException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAmountException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public InvalidAmountException(string message, Exception inner) : base(message, inner) { }
    }
}
