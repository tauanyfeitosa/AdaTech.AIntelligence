using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Exceptions
{
    public class ReadingDescriptionException : Exception
    {
        public ReadingDescriptionException() : base() { }
        public ReadingDescriptionException(string message) : base(message) { }
        public ReadingDescriptionException(string message, Exception inner) : base(message, inner) { }
    }
}
