﻿/* NS: PluginCount */
/* FN: PluginCount.cs */
/* FUNCTION: A plugin which counts the days since the users' birthday */

using System;
using System.Collections.Generic;
using Interface;

namespace PluginCount
{
    public class PluginCount : IPlugin
    {
        private string _Name = "Tageszähler";
        public string Name { get { return _Name; } }

        /* If math plugin finds a word with the type 'C', it knows that it can handle it */
        /* -> return priority 10 (highest) */
        public int GetPriority(List<Word> wordlist)
        {
            int prior = 0;
            foreach (Word w in wordlist)
            { 
                switch(w.Type)
                {
                    case 'N':
                        if (w.Value == "Tage")
                        { prior += 3; }
                        else if (w.Value == "Geburtstag")
                        { prior += 3; }
                        break;
                    default:
                        if (w.Value == "vergangen")
                        { prior += 3; }
                        break;
                }
            }
            return prior;
        }

        /* If math plugin has "won" the competition, calculate and return result string */
        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Ich habe mich leider verzählt...";
            return answer;
        }
    }
}
