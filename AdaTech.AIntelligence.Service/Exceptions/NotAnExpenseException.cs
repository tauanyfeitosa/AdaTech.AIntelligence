using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Exceptions
{
    public class NotAnExpenseException : Exception
    {
        public NotAnExpenseException() : base() { }
        public NotAnExpenseException(string message) : base(message) { }
        public NotAnExpenseException(string message, Exception inner) : base(message, inner) { }
    }
}
