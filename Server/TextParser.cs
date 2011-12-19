/* NS: Server */
/* FN: TextParser.cs */
/* FUNCTION: Split sentence into words, analyse them and save their position in the sentence */
/* Is called by ClientComm */
/* All thrown exceptions will be handled by ClientComm and quit the connection */

using System;
using System.Collections.Generic;

namespace Server
{
    public class TextParser
    {
        /* PRIVATE VARS */
        /* PUBLIC VARS */
        public string ClientSentence { get; set; }
        public List<Word> AnalysedWord { get; set; }

        public void SplitSentence(string cs)
        {
            //pass sentence
            ClientSentence = cs;
            //split sentence
            char[] seps = { ' ', '.' };
            string[] words = cs.Split(seps);
            AnalysedWord = new List<Word>();
            //create word-instance for each word and save it into list
           foreach(string w in words)
            {
                Word nword = new Word(w);
                nword.Type = this.GetType(nword);
                AnalysedWord.Add(nword);
            }
        }

        private char GetType(Word w)
        {
            return 'X';
        }

        //save type info (N...noun, V...Verb, A...adjective, R...article, P...preposition)
    }
}
