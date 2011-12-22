using System;
using System.Collections.Generic;
using Interface;

namespace PluginMath
{
    public class PluginMath : IPlugin
    {
        public int GetPriority(List<Word> wordlist)
        {
            return 1;
        }

        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Antwort des Mathematikers";
            return answer;
        }
    }
}
