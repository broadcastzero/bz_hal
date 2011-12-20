/* NS: Server */
/* FN: InvalidSentenceException.cs */
/* FUNCTION: Defines a custom exception which is thrown, 
 * if Server doesn't know how to answer the question/statement. */

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
