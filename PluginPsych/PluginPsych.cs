﻿using System;
using System.Collections.Generic;
using Interface;

namespace PluginPsych
{
    public class PluginPsych : IPlugin
    {
        private string _Name = "Psychater";
        public string Name { get { return _Name; } }

        public int GetPriority(List<Word> wordlist)
        {

            return 1;
        }

        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Antwort des Psychaters";
            return answer;
        }
    }
}
