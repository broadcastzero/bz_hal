using System;
using System.Collections.Generic;
using Interface;

namespace PluginNav
{
    public class PluginNavi : IPlugin
    {
        /* Calculate priority of Navigator */
        public int GetPriority(List<Word> wordlist)
        {
            int priority = 0;
            // defines, whether string "wo" or streetname have been found before - only count once!
            bool wo_counted = false;
            bool str_counted = false;
            bool isquestion = false;

            foreach(Word w in wordlist)
            {
                if (w.Type == 'Q' && w.Value.ToLower() == "wo" && wo_counted == false)
                { priority += 5; wo_counted = true; isquestion = true; }
                else if((w.Value.ToLower().Contains("strasse") || w.Value.ToLower().Contains("straße") || 
                    w.Value.ToLower().Contains("dorf")) && str_counted == false)
                { priority += 2; str_counted = true; }
                //is it a question? -> priority++
                else if (w.Type == 'M' && w.Value == "?")
                { priority += 1; }
            }

            // if everything fits -> 10 (highest)
            if (wo_counted == true && str_counted == true && isquestion == true)
            { priority = 10; }

            return priority;
        }

        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Antwort des Navigators";
            return answer;
        }
    }
}
