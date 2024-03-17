namespace AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer
{
    public class ReadingCategoryException : Exception
    {
        public ReadingCategoryException() : base() { }
        public ReadingCategoryException(string message) : base(message) { }
        public ReadingCategoryException(string message, Exception inner) : base(message, inner) { }
    }
}
