﻿using System;
using System.Collections.Generic;
using Interface;
using System.Threading;

namespace PluginNav
{
    public class PluginNavi : IPlugin
    {
        /* PRIVATE VARS */
        private static SortedList<string, string> _Map = null;

        /* CONSTRUCTOR */
        public PluginNavi()
        {
            _Map = new SortedList<string, string>();
        }

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

        /* Load OpenStreetMap into _Map variable */
        public string LoadMap()
        {
            // is _Map locked (loaded at the moment)?
            if (Monitor.TryEnter(_Map) == false)
            { return "Die Karte wird gerade von jemand anders neu aufbereitet!"; }
            lock (_Map)
            {
            
            }

            //return success
            string answer = "Die Karte wurde neu aufbereitet.";
            return answer;
        }

        /* Find out where Client wants to be navigated, search in internal List and return answer */
        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Wenn du navigieren willst, dann stelle deine Frage bitte präziser.";
            /* searching in list will be coded here */
            return answer;
        }
    }
}
