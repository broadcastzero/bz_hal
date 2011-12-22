using System;
using System.Collections.Generic;
using Interface;

namespace PluginNav
{
    public class PluginNav : IPlugin
    {
        public int GetPriority(List<Word> wordlist)
        {
            return 0;
        }

        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Antwort des Navigators";
            return answer;
        }
    }
}
