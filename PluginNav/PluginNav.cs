using System;
using System.Collections.Generic;
using Interface;

namespace PluginNav
{
    public class PluginNav : IPlugin
    {
        /* Calculate priority of Navigator */
        public int GetPriority(List<Word> wordlist)
        {
            int priority = 0;
            // defines, whether string "wo" has been found before
            bool wo_counted = false;
            bool str_counted = false;

            foreach(Word w in wordlist)
            {
                if (w.Value.ToLower() == "wo" && wo_counted == false)
                { priority += 5; wo_counted = true; }
                else if((w.Value.ToLower() == "strasse" || w.Value.ToLower() == "straße" || 
                    w.Value.ToLower() == "dorf") && str_counted == false)
                { priority += 2; str_counted = true; }
            }

            return priority;
        }

        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Antwort des Navigators";
            return answer;
        }
    }
}
