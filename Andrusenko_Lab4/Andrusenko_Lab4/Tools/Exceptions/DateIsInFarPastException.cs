using System;
using System.Collections.Generic;

namespace Andrusenko_Lab4.Tools.Exceptions
{
    public class DateIsInFarPastException: Exception
    {
        public DateIsInFarPastException()
        {
        }

        public DateIsInFarPastException(string message)
            : base(message)
        {
        }

        public DateIsInFarPastException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
