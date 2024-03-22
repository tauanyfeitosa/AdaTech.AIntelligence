namespace AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer
{
    /// <summary>
    /// Represents an exception thrown when reading an amount encounters an error.
    /// </summary>
    public class ReadingAmountException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingAmountException"/> class.
        /// </summary>
        public ReadingAmountException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingAmountException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ReadingAmountException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingAmountException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public ReadingAmountException(string message, Exception inner) : base(message, inner) { }
    }
}