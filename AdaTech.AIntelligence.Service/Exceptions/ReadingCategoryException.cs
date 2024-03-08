using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Service.Exceptions
{
    public class ReadingCategoryException : Exception
    {
        public ReadingCategoryException() : base() { }
        public ReadingCategoryException(string message) : base(message) { }
        public ReadingCategoryException(string message, Exception inner) : base(message, inner) { }
    }
}
