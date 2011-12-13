/* NS: Server */
/* FN: TextParser.cs */
/* FUNCTION: Split sentence into words, analyse them and save their position in the sentence */

using System;

namespace Server
{
    public class TextParser
    {
        private string _clientsentence;
        private char[][] _analysedword;

        public TextParser(string cs)
        { 
            _clientsentence = cs;
        }

        public void SplitSentence()
        {
            //save type info (N...noun, V...Verb, A...adjective, R...article, P...preposition)
        }
    }
}
