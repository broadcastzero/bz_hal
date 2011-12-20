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
        private string _ClientSentence;
        private char _Punctuation;
        /* PUBLIC VARS */
        public List<Word> AnalysedWords { get; set; }

        public void SplitSentence(string cs)
        {
            //pass and split sentence
            _ClientSentence = cs;
            //save punctuation mark
            if (cs[cs.Length - 1] == '.') { _Punctuation = '.'; }
            else if (cs[cs.Length - 1] == '?') { _Punctuation = '?'; }
            else { _Punctuation = 'X'; }

            char[] seps = { ' ', '.', '?' };
            string[] words = cs.Split(seps);

            //create word-instance for each word and save it into list
            AnalysedWords = new List<Word>();
            foreach(string w in words)
            {
                Word nword = new Word(w);
                nword.Type = this.GetType(nword);
                AnalysedWords.Add(nword);
            }
            //add punctuation mark
            Word punct = new Word(_Punctuation.ToString());
            punct.Type = 'M';
            AnalysedWords[AnalysedWords.Count-1] = punct;
        }

        private char GetType(Word w)
        {
            return 'X';
        }

        //save type info (N...noun, V...Verb, A...adjective, R...article, P...preposition)
    }
}
