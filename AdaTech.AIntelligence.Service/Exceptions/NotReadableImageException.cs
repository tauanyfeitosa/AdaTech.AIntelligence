﻿namespace AdaTech.AIntelligence.Service.Exceptions
{
    public class NotReadableImageException : Exception
    {
        public NotReadableImageException() : base() { }
        public NotReadableImageException(string message) : base(message) { }
        public NotReadableImageException(string message, Exception inner) : base(message, inner) { }
    }
}
