

namespace AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer
{
    public class NotConnectionGPTException: Exception
    {
        public NotConnectionGPTException() : base() { }
        public NotConnectionGPTException(string message) : base(message) { }
        public NotConnectionGPTException(string message, Exception inner) : base(message, inner) { }
    }
}
