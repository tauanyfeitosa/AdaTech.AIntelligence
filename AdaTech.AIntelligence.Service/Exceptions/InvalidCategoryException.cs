using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Exceptions
{
    public class InvalidCategoryException : Exception
    {
        public InvalidCategoryException() : base() { }
        public InvalidCategoryException(string message) : base(message) { }
        public InvalidCategoryException(string message, Exception inner) : base(message, inner) { }
    }
}
