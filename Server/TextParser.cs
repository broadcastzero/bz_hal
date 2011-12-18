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
        /* PRIVATE VARS */
        /* PUBLIC VARS */
        public string ClientSentence_ { get; set; }
        public List<Word> Analysedword_ { get; set; }

        public void SplitSentence(string cs)
        {
            //pass sentence
            ClientSentence_ = cs;

            //split sentence into words and save it into list
            //while (true)
            //{
                //Word nword = new Word();
                //this.Analysedword.Add(nword);
            //}
            //save type info (N...noun, V...Verb, A...adjective, R...article, P...preposition)
        }
    }
}
