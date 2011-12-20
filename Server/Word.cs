/* NS: Server */
/* FN: Word.cs */
/* FUNCTION: Single word of a sentence, value and type are stored */

using System;

namespace Server
{
    public class Word
    {
        /* PRIVATE VARS */
        // possible: N...Noun, V...Verb, A...Adjective, R...Article, P...Preposition, M...Punctuation mark, Q...Question word
        // unknown/not found: X
        private char _Type;

        /* PUBLIC VARS */
        public string Value { get; set; }
        public char Type 
        {
            get { return _Type; }
            set 
            {
                if (value == 'N' || value == 'V' || value == 'A' || value == 'R' || value == 'P' || value =='M' || value=='Q')
                { _Type = value; }
                else { _Type = 'X'; } //type not found
            }
        }

        /* CONSTRUCTOR */
        public Word(string w)
        {
            Value = w;
        }
    }
}
