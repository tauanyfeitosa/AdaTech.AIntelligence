namespace AdaTech.AIntelligence.Service.Exceptions
{
    public class InvalidDescriptionException : Exception
    {
        public InvalidDescriptionException() : base() { }
        public InvalidDescriptionException(string message) : base(message) { }
        public InvalidDescriptionException(string message, Exception inner) : base(message, inner) { }
    }
}
