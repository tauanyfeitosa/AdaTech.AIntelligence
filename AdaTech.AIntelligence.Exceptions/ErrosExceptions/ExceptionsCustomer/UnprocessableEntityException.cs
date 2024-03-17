
namespace AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer
{

    /// <summary>
    /// Represents an exception thrown when an operation is not an expense.
    /// </summary>
    public class UnprocessableEntityException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnprocessableEntityException"/> class.
        /// </summary>
        public UnprocessableEntityException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnprocessableEntityException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
        public UnprocessableEntityException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnprocessableEntityException"/> class with a specified error message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public UnprocessableEntityException(string message, Exception inner) : base(message, inner) { }
    }
}
