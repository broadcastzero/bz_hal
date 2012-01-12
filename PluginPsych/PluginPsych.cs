/* NS: PluginPsych */
/* FN: PluginPsych.cs */
/* FUNCTION: A plugin which gives psychological answers to client inputs */

using System;
using System.Collections.Generic;
using Interface;

namespace PluginPsych
{
    public class PluginPsycho : IPlugin
    {
        private string _Name = "Psychater";
        public string Name { get { return _Name; } }

        private List<string> _Responsibles = null;
        private int answerindex;
        private string[] answertypes;

        /* CONSTRUCTOR - add responsibles */
        public PluginPsycho()
        {
            // backup answers
            answerindex = 0;
            answertypes = new string[5];
            answertypes[0] = "Was genau ist Ihr Problem mit ";
            answertypes[1] = "Vielleicht sind Sie das Problem, und nicht ";
            answertypes[2] = "Meinen Sie wirklich das Problem ist ";
            answertypes[3] = "Erzählen Sie mir mehr von ";

            _Responsibles = new List<string>();
            _Responsibles.Add("Vater");
            _Responsibles.Add("Mutter");
            _Responsibles.Add("Kinder");
            _Responsibles.Add("Familie");
            _Responsibles.Add("Haustier");
            _Responsibles.Add("Hund");
            _Responsibles.Add("Katze");
            _Responsibles.Add("Alkohol");
            _Responsibles.Add("Sorgen");
            _Responsibles.Add("Geld");
            _Responsibles.Add("Leben");
            _Responsibles.Add("Antwort");
        }

        /* GET PRIORITY */
        public int GetPriority(List<Word> wordlist)
        {
            int prior = 4; //Standard-Prior - Psychiater is always responsible
            foreach (Word w in wordlist)
            {
                switch (w.Type)
                { 
                    case 'N': case 'S':
                        if (_Responsibles.Contains(w.Value))
                        {
                            prior += 5;
                        }
                        break;
                    default:
                        break;
                }
            }
            return prior;
        }

        /* GET ANSWER*/
        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Das ist interessant, erzählen Sie mir mehr davon!";
            bool found = false;
            foreach (Word w in wordlist)
            {
                switch (w.Type)
                {
                    // if noun or subject
                    case 'N': case 'S':
                        // normal answers
                        if (_Responsibles.Contains(w.Value))
                        {
                            switch (w.Value)
                            {
                                case "Vater": answer = "Sie sollten Ihren Vater wieder mal besuchen!"; found = true;
                                    break;
                                case "Mutter": answer = "Ihre Mutter ist ein guter Mensch, denke ich."; found = true;
                                    break;
                                case "Kinder": answer = "Kinder sind ein Segen - aber leben Sie zuerst Ihr Leben!"; found = true;
                                    break;
                                case "Familie": answer = "Eine intakte Familie ist die Vorraussetzung für ein intaktes Seelenleben."; found = true;
                                    break;
                                case "Haustier": answer = "Tiere brauchen viel Liebe, denken Sie immer daran!"; found = true;
                                    break;
                                case "Hund": answer = "Hunde sind treue Freunde!"; found = true;
                                    break;
                                case "Katze": answer = "Katzen mag ich am liebsten!"; found = true;
                                    break;
                                case "Alkohol": answer = "Versprechen Sie mir, weniger Alkohol zu trinken?"; found = true;
                                    break;
                                case "Sorgen": answer = "Welche Sorgen haben Sie denn genau?"; found = true;
                                    break;
                                case "Geld": answer = "Ja, von dem Teufelszeug ist immer zu wenig da. Achja, ich nehme nur Bargeld!"; found = true;
                                    break;
                                case "Leben": answer = "Leben...erzählen Sie mir nichts vom Leben..."; found = true;
                                    break;
                                case "Antwort": answer = "42"; found = true;
                                    break;
                                default: answer = answertypes[answerindex] + w.Value;
                                        answerindex++;
                                        if (answerindex == 5)
                                        { answerindex = 0; }
                                    break;
                            }
                        }
                        else if (found == true)
                        { break; }
                        // backupanswers
                        else
                        {
                            answer = answertypes[answerindex] + w.Value;
                            answerindex++;
                            if(answerindex == 5) 
                            { answerindex = 0; }
                        }
                        break;
                    default:
                        break;
                }
            }

            return answer;
        }
    }
}
