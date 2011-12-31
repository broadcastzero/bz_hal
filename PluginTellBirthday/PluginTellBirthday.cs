/* NS: PluginCount */
/* FN: PluginCount.cs */
/* FUNCTION: A plugin which tells which day the user's birthday was */

using System;
using System.Collections.Generic;
using Interface;

namespace PluginCount
{
    public class PluginCount : IPlugin
    {
        private string _Name = "Geburtstagsermittler";
        public string Name { get { return _Name; } }

        /* GET PRIORITY */
        public int GetPriority(List<Word> wordlist)
        {
            int prior = 0;
            foreach (Word w in wordlist)
            { 
                switch(w.Type)
                {
                    case 'N':
                        if (w.Value == "Tag")
                        { prior += 3; }
                        else if (w.Value == "Geburtstag")
                        { prior += 3; }
                        break;
                    default:
                        if (w.Value == "am")
                        { prior += 1; }
                        break;
                }
            }
            return prior;
        }

        /* If plugin has "won" the competition, calculate and return result string */
        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Ich habe mich leider verzählt...bitte gib dein Geburtsdatum in dem Format DD/MM/YYYY ein!";
            int day, month, year;

            // try to get birthday
            foreach (Word w in wordlist)
            {
                if (w.Value[2] == '/' && w.Value[5] == '/' && w.Value.Length == 10)
                {
                    char[] trimmer = {'/'};
                    string[] result = w.Value.Split(trimmer);

                    bool success = int.TryParse(result[0], out day);
                    if (success == false) { answer = "Ich soll einen Buchstaben als Zahl verarbeiten? No way!"; }

                    success = int.TryParse(result[1], out month);
                    if (success == false) { answer = "Ich soll einen Buchstaben als Zahl verarbeiten? No way!"; }

                    success = int.TryParse(result[2], out year);
                    if (success == false) { answer = "Ich soll einen Buchstaben als Zahl verarbeiten? No way!"; }

                    break;
                }
            }

            // count days since birthday


            return answer;
        }
    }
}
