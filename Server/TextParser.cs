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
        //R - Articles
        public List<string> _Articles;

        /* PUBLIC VARS */
        public List<Word> AnalysedWords { get; set; }

        /* METHODS */
        /* CONSTRUCTOR - initialize word lists */
        public TextParser()
        {
            _Articles = new List<string> { "der", "die", "das" };
        }

        /* Split sentence and add it to List<Word> AnalysedWords */
        public void SplitSentence(string cs)
        {
            //pass and split sentence
            _ClientSentence = cs;
            int len = 0;
            len = _ClientSentence.Length;

            //save punctuation mark
            if (cs.LastIndexOf('.') == len-1) { _Punctuation = '.'; }
            else if (cs.LastIndexOf('?') == len-1) { _Punctuation = '?'; }
            else 
            { 
                string errmsg = "Der Satz endet nicht mit einem '.' oder '?' - woher soll ich wissen, was du meinst?";
                throw new InvalidSentenceException(errmsg); 
            }

            char[] seps = { ' ', '.', '?' };
            string[] words = cs.Split(seps);

            //create word-instance for each word and save it into list
            AnalysedWords = new List<Word>();
            foreach(string w in words)
            {
                Word nword = new Word(w);
                nword.Type = this.GetType(ref nword);
                AnalysedWords.Add(nword);
            }
            //remove newline - add punctuation mark instead
            Word punct = new Word(_Punctuation.ToString());
            punct.Type = 'M';
            AnalysedWords.RemoveAt(AnalysedWords.Count-1);
            AnalysedWords.Add(punct);
        }

        /* Try to guess type of word and save it in variable "Type" */
        /* N...noun, V...Verb, A...adjective, R...article, P...preposition, M...Punctuation Mark */
        /* Not found - 'X' */
        private char GetType(ref Word w)
        {
            return 'X';
        }

        //save type info 
    }
}
