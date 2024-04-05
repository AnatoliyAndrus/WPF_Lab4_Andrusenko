
namespace Andrusenko_Lab4.Tools.Exceptions
{
    public class DateIsInFutureException : Exception
    {
        public DateIsInFutureException()
        {
        }

        public DateIsInFutureException(string message)
            : base(message)
        {
        }

        public DateIsInFutureException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
