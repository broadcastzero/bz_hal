using System;
using System.Collections.Generic;
using Interface;
using System.Threading;

namespace PluginNav
{
    public class PluginNavi : IPlugin
    {
        /* PRIVATE VARS */
        private static SortedList<string, string> _Map = null;
        private string _Name = "Navigator";

        /* PUBLIC VARS */
        public static SortedList<string, string> Map
        {
            get { return _Map; }
            set { }
        }

        public string Name { get { return _Name; } }

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
            int loadmap=0;

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
                // if map shall be reloaded
                else if (w.Value.ToLower() == "karte" || w.Value.ToLower() == "neu" ||
                    w.Value.ToLower() == "auf" || w.Value.ToLower() == "bereite")
                {
                    loadmap++;
                }
            }

            // if everything fits -> 10 (highest)
            if (wo_counted == true && str_counted == true && isquestion == true)
            { priority = 10; }
            // if map shall be reloaded
            else if (loadmap > 3) { priority = 10; }

            return priority;
        }

        /* Load OpenStreetMap into _Map variable */
        public void LoadMap()
        {
            // is _Map locked (loaded at the moment)? -> return error
            if (Monitor.TryEnter(_Map) == false)
            { throw new InvalidOperationException("Die Karte wird gerade von jemand anders neu aufbereitet!"); }

            // make it threadsafe
            lock (_Map)
            {
                // get new instance
                _Map.Clear();
                Console.WriteLine("Die Karte wird neu aufbereitet...");

                _Map.Add("Alserstrasse", "Wien");
                _Map.Add("Linzerstrasse", "Tirol");
                // add some code here
            }

            //return success
            //string answer = "Die Karte wurde neu aufbereitet.";
            //return answer;
        }

        /* Find out where Client wants to be navigated, search in internal List and return answer */
        public string CalculateSentence(List<Word> wordlist)
        {
            // Standard answer
            string answer = "Wenn du navigieren willst, dann stelle deine Frage bitte präziser.";

            // is list empty? -> load!
            if (_Map.Count == 0)
            { this.LoadMap(); }

            /* searching in list will be coded here */
            return answer;
        }
    }
}
