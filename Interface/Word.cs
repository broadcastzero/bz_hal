/* NS: Interface */
/* FN: Word.cs */
/* FUNCTION: Single word of a sentence, value and type are stored */
/* Every class who gets List<Word> must be using Interface */

using System;

namespace Interface
{
    public class Word
    {
        /* PRIVATE VARS */
        // possible: S...Subject, N...Noun, V...Verb, A...Adjective, R...Article, P...Preposition, M...Punctuation mark, Q...Question word, C...Math(Calc)
        // unknown/not found: X
        private char _Type;

        /* PUBLIC VARS */
        public string Value { get; set; }
        public int Position { get; set; }
        public char Type 
        {
            get { return _Type; }
            set 
            {
                if (value == 'S' || value == 'N' || value == 'V' || value == 'A' || value == 'R' || value == 'P' || value =='M' || value=='Q')
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
