﻿using System;
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

            foreach(Word w in wordlist)
            {
                if (w.Type == 'Q' && w.Value.ToLower() == "wo" && wo_counted == false)
                { priority += 5; wo_counted = true; }
                else if((w.Value.ToLower() == "strasse" || w.Value.ToLower() == "straße" || 
                    w.Value.ToLower() == "dorf") && str_counted == false)
                { priority += 2; str_counted = true; }
                //is it a question? -> priority++
                else if (w.Type == 'M' && w.Value == "?")
                { priority += 1; }
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