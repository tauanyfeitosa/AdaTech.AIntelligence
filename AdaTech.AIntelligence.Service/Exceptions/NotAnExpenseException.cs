namespace AdaTech.AIntelligence.Service.Exceptions
{
    public class NotAnExpenseException : Exception
    {
        public NotAnExpenseException() : base() { }
        public NotAnExpenseException(string message) : base(message) { }
        public NotAnExpenseException(string message, Exception inner) : base(message, inner) { }
    }
}
