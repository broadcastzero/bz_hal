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
        private List<string> _Subject;
        private List<string> _Articles;
        private List<string> _QuestionWords;
        private List<string> _Possessive;

        /* PUBLIC VARS */
        public List<Word> AnalysedWords { get; set; }

        /* METHODS */
        /* CONSTRUCTOR - initialize word lists */
        public TextParser()
        {
            _Subject = new List<string> { "ich", "du", "er", "sie", "es", "wir", "ihr" };
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
            int pos = 0;
            foreach(string w in words)
            {
                Word nword = new Word(w);
                nword.Position = pos++;
                nword.Type = this.GetType(nword, mark);
                AnalysedWords.Add(nword);
            }
            //remove newline - add punctuation mark instead
            Word punct = new Word(mark.ToString());
            punct.Type = 'M';
            AnalysedWords.RemoveAt(AnalysedWords.Count-1);
            AnalysedWords.Add(punct);
        }

        /* Try to guess type of word and save it in variable "Type" */
        /* S...subject, N...noun, V...Verb, A...adjective, R...article, P...preposition, M...Punctuation Mark, Q...Question word */
        /* Not found - 'X' */
        private char GetType(Word w, char mark)
        {
            //will be the case at the last word (is deleted later)
            if (w.Value.Length == 0)
            { return 'X'; }

            //save first character and then make everything lowercase
            char firstletter = w.Value[0];
            w.Value = w.Value.ToLower();

            if (_Subject.Contains(w.Value.ToLower()))
            { return 'S'; }
            //if first letter is uppercase -> noun
            else if (w.Value[0] == char.ToUpper(firstletter) && w.Position != 0)
            { return 'N'; }
            else if (_QuestionWords.Contains(w.Value.ToLower()) && mark == '?')
            { return 'Q'; }
            else if (_Articles.Contains(w.Value.ToLower()) && mark == '.')
            { return 'R'; }
            else if (_Articles.Contains(w.Value.ToLower()) && mark == '?' && w.Position != 0)
            { return 'R'; }
            else { return 'X'; }
        }
    }
}
//using Antonyms!