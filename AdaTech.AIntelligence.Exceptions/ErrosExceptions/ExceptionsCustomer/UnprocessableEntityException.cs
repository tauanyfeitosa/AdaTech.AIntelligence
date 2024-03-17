
namespace AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer
{
    public class UnprocessableEntityException: Exception
    {
        public UnprocessableEntityException() : base() { }
        public UnprocessableEntityException(string message) : base(message) { }
        public UnprocessableEntityException(string message, Exception inner) : base(message, inner) { }
    }
}
