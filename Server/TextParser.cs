/* NS: Server */
/* FN: TextParser.cs */
/* FUNCTION: Split sentence into words, analyse them and save their position in the sentence */
/* Is called by ClientComm */

using System;
using System.Collections.Generic;

namespace Server
{
    public class TextParser
    {
        private string _clientsentence;
        private List<Word> _Analysedword;

        public TextParser()
        {}

        public void SplitSentence(string cs)
        {
            //pass sentence
            this._clientsentence = cs;

            //split sentence into words and save it into list
            while (true)
            {
                Word nword = new Word();
                _Analysedword.Add(nword);
            }
            //save type info (N...noun, V...Verb, A...adjective, R...article, P...preposition)
        }
    }
}
