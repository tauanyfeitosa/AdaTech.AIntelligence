using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Exceptions
{
    public class ReadingAmountException : Exception
    {
        public ReadingAmountException() : base() { }
        public ReadingAmountException(string message) : base(message) { }
        public ReadingAmountException(string message, Exception inner) : base(message, inner) { }
    }
}
