namespace AdaTech.AIntelligence.Service.Exceptions
{
    public class InvalidCategoryException : Exception
    {
        public InvalidCategoryException() : base() { }
        public InvalidCategoryException(string message) : base(message) { }
        public InvalidCategoryException(string message, Exception inner) : base(message, inner) { }
    }
}
