namespace AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer
{
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException() : base() { }
        public InvalidAmountException(string message) : base(message) { }
        public InvalidAmountException(string message, Exception inner) : base(message, inner) { }
    }
}
