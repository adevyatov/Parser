using System.Runtime.Serialization;

namespace Parser.Exception
{
    public class ParseTimeoutException : System.Exception
    {
        public ParseTimeoutException()
        {
        }

        protected ParseTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ParseTimeoutException(string? message) : base(message)
        {
        }

        public ParseTimeoutException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}