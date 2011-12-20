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
        //R - Articles
        private List<string> _Articles;
        private List<string> _QuestionWords;

        /* PUBLIC VARS */
        public List<Word> AnalysedWords { get; set; }

        /* METHODS */
        /* CONSTRUCTOR - initialize word lists */
        public TextParser()
        {
            _Articles = new List<string> { "der", "die", "das" };
            _QuestionWords = new List<string> { "wo", "wer", "was", "wie", "wann", "wieso", "weshalb", "warum", "wen", "wem", "wessen", "woher", "woran" };
        }

        /* Split sentence and add it to List<Word> AnalysedWords */
        public void SplitSentence(string sentence)
        {
            //pass and split sentence
            char mark; //punctuation mark
            int len = 0;
            len = sentence.Length;

            //save punctuation mark
            if (sentence.LastIndexOf('.') == len - 1) { mark = '.'; }
            else if (sentence.LastIndexOf('?') == len - 1) { mark = '?'; }
            else 
            { 
                string errmsg = "Der Satz endet nicht mit einem '.' oder '?' - woher soll ich wissen, was du meinst?";
                throw new InvalidSentenceException(errmsg); 
            }

            char[] seps = { ' ', '.', '?' };
            string[] words = sentence.Split(seps);

            //create word-instance for each word and save it into list
            AnalysedWords = new List<Word>();
            foreach(string w in words)
            {
                Word nword = new Word(w);
                nword.Type = this.GetType(ref nword, mark);
                AnalysedWords.Add(nword);
            }
            //remove newline - add punctuation mark instead
            Word punct = new Word(mark.ToString());
            punct.Type = 'M';
            AnalysedWords.RemoveAt(AnalysedWords.Count-1);
            AnalysedWords.Add(punct);
        }

        /* Try to guess type of word and save it in variable "Type" */
        /* N...noun, V...Verb, A...adjective, R...article, P...preposition, M...Punctuation Mark, Q...Question word */
        /* Not found - 'X' */
        private char GetType(ref Word w, char mark)
        {
            if(_QuestionWords.Contains(w.Value.ToLower()) && mark == '?')
            { return 'Q'; }
            else if(_Articles.Contains(w.Value.ToLower()))
            { return 'R';}
            else { return 'X'; }
        }
    }
}
//using Antonyms!