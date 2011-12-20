using System;

namespace Server
{
    public class InvalidSentenceException : Exception
    {
        public InvalidSentenceException(string message) : base (message){ }

        public InvalidSentenceException(string message, Exception inner)
            : base(message, inner)
        {   }
    }
}
