using System;
using System.Collections.Generic;
using Interface;

namespace PluginPsych
{
    public class PluginPsych : IPlugin
    {
        private string _Name = "Psychater";
        public string Name { get { return _Name; } }

        private List<string> _Responsibles = null;

        /* CONSTRUCTOR - add responsibles */
        public PluginPsych()
        {
            _Responsibles = new List<string>();
            _Responsibles.Add("Vater");
            _Responsibles.Add("Mutter");
            _Responsibles.Add("Kinder");
            _Responsibles.Add("Familie");
            _Responsibles.Add("Haustier");
            _Responsibles.Add("Hund");
            _Responsibles.Add("Katze");
        }


        public int GetPriority(List<Word> wordlist)
        {
            int prior = 0;
            foreach (Word w in wordlist)
            {
                switch (w.Type)
                { 
                    case 'N':
                        if (_Responsibles.Contains(w.Value))
                        {
                            prior += 3;
                        }
                        break;
                    default:
                        break;
                }
            }
            return prior;
        }

        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Antwort des Psychaters";
            return answer;
        }
    }
}
