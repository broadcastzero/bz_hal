using System;
using System.Collections.Generic;
using Interface;

namespace PluginMath
{
    public class PluginMath : IPlugin
    {
        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Antwort des Mathematikers";
            return answer;
        }
    }
}
