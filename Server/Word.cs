/* NS: Server */
/* FN: Word.cs */
/* FUNCTION: Single word of sentence, calculate type */

using System;

namespace Server
{
    public class Word
    {
        /* PRIVATE VARS */
        // possible: N...Noun, V...Verb, A...Adjective, R...Article, P...Preposition
        private char _Type;

        /* PUBLIC VARS */        
        public char Type_ 
        {
            get { return _Type; }
            set 
            {
                if (value == 'N' || value == 'V' || value == 'A' || value == 'R' || value == 'P')
                { _Type = value; }
            }
        }
    }
}
