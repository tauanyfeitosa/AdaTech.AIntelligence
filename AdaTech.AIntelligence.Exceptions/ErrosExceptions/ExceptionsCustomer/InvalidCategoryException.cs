namespace AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer
{
    public class InvalidCategoryException : Exception
    {
        public InvalidCategoryException() : base() { }
        public InvalidCategoryException(string message) : base(message) { }
        public InvalidCategoryException(string message, Exception inner) : base(message, inner) { }
    }
}
