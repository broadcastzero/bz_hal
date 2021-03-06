﻿/* NS: PluginTellDay */
/* FN: PluginTellBirthday.cs */
/* FUNCTION: A plugin which tells which day the user's birthday was */

using System;
using System.Collections.Generic;
using Interface;

namespace PluginTellDay
{
    public class PluginTellBirthday : IPlugin
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
            int day=0;
            int month = 0;
            int year = 0;

            // try to get birthday
            foreach (Word w in wordlist)
            {
                if (w.Value.Length == 10)
                {
                    if (w.Value.ToString()[2] == '/' && w.Value.ToString()[5] == '/')
                    {
                        char[] trimmer = { '/' };
                        string[] result = new string[3];
                        result = w.Value.Split(trimmer);

                        bool success = int.TryParse(result[0], out day);
                        if (success == false) { return "Ich soll einen Buchstaben als Zahl verarbeiten? No way!"; }

                        success = int.TryParse(result[1], out month);
                        if (success == false) { return "Ich soll einen Buchstaben als Zahl verarbeiten? No way!"; }

                        success = int.TryParse(result[2], out year);
                        if (success == false) { return "Ich soll einen Buchstaben als Zahl verarbeiten? No way!"; }

                        break;
                    }
                }
            }

            // do vars contain value?
            if (day == 0 || month == 0 || year == 0)
            { return "Ich habe mich verzählt..nur wieso?"; }
            else
            {
                // which day was it?
                DateTime Birthday = new DateTime(year, month, day);
                answer = "Der " + day + "/" + month + "/" + year +" war ein " + Birthday.DayOfWeek.ToString() +"!";
                // happy birthday-easteregg
                if (Birthday.Day == DateTime.Today.Day && Birthday.Month == DateTime.Today.Month)
                { answer += " \nOh, du hast heute Geburtstag. Ich wünsche dir alles Gute!"; }
            }

            return answer;
        }
    }
}
