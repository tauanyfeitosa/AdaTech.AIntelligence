

namespace AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer
{
    /// <summary>
    /// Represent an exception thrown when there is no connection to the GPT service.
    /// </summary>
    public class NotConnectionGPTException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotConnectionGPTException"/> class.
        /// </summary>
        public NotConnectionGPTException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotConnectionGPTException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
        public NotConnectionGPTException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotConnectionGPTException"/> class with a specified error message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public NotConnectionGPTException(string message, Exception inner) : base(message, inner) { }
    }
}
