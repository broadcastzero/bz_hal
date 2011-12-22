using System;
using System.Collections.Generic;
using Interface;

namespace PluginMath
{
    public class PluginMath : IPlugin
    {
        private int _pos;

        public int GetPriority(List<Word> wordlist)
        {
            _pos = 0;
            int pos = -1;
            // search for (C)alculating sign in wordlist
            foreach(Word w in wordlist)
            {
                if (w.Type == 'C')
                { pos = w.Position; break; }
            }

            // if there is none -> not responsible
            if (pos == -1) { return 0; }
            else
            { _pos = pos; return 10; }
        }

        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Wenn das eine Rechnung sein soll, dann kann ich damit nichts anfangen.";
            if (_pos != 0)
            {
                //try to convert the word before and after calculating sign into integer
                int outnum1 = 0;
                int outnum2 = 0;
                bool canConvert1 = false;
                bool canConvert2 = false;
                try
                {
                    canConvert1 = int.TryParse(wordlist[_pos - 1].Value, out outnum1);
                    canConvert2 = int.TryParse(wordlist[_pos + 1].Value, out outnum2);
                }
                catch (Exception)
                { answer = "Wenn du rechnen willst, dann achte darauf, dass vor und nach dem Operator Zahlen stehen!"; }

                if (canConvert1 == true && canConvert2 == true)
                {
                    int result = 0;
                    switch (wordlist[_pos].Value)
                    {
                        case "+": result = outnum1 + outnum2;
                            break;
                        case "-": result = outnum1 - outnum2;
                            break;
                        case "*": result = outnum1 * outnum2;
                            break;
                        case "/": result = outnum1 / outnum2;
                            break;
                        default:
                            break;
                    }
                    answer = "Da kommt wohl " + result + " raus!";
                }
                else { answer = "Damit kann ich nichts anfangen."; }
            }
            return answer;
        }
    }
}
