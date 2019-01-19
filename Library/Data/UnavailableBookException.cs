using System;
using System.Runtime.Serialization;

namespace BookstoreLogic.Data
{
    [Serializable]
    public class UnavailableBookException : Exception
    {
        public UnavailableBookException()
        {
        }

        public UnavailableBookException(string message) : base(message)
        {
        }

        public UnavailableBookException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnavailableBookException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}