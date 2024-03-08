using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Exceptions
{
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException() : base() { }
        public InvalidAmountException(string message) : base(message) { }
        public InvalidAmountException(string message, Exception inner) : base(message, inner) { }
    }
}
