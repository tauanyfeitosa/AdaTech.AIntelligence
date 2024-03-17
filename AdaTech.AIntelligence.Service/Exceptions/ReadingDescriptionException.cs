namespace AdaTech.AIntelligence.Service.Exceptions
{
    /// <summary>
    /// Represents an exception thrown when reading a description encounters an error.
    /// </summary>
    public class ReadingDescriptionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingDescriptionException"/> class.
        /// </summary>
        public ReadingDescriptionException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingDescriptionException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ReadingDescriptionException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingDescriptionException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public ReadingDescriptionException(string message, Exception inner) : base(message, inner) { }
    }
}