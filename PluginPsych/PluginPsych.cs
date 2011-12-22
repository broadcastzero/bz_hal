using System;
using System.Collections.Generic;
using Interface;

namespace PluginPsych
{
    public class PluginPsych : IPlugin
    {
        public int GetPriority(List<Word> wordlist)
        {
            return 0;
        }

        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Antwort des Psychaters";
            return answer;
        }
    }
}
