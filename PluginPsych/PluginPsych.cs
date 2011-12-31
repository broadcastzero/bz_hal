/* NS: PluginPsych */
/* FN: PluginPsych.cs */
/* FUNCTION: A plugin which gives psychological answers to client inputs */

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
        private int answerindex;
        private string[] answertypes;

        /* CONSTRUCTOR - add responsibles */
        public PluginPsych()
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

        /* GET ANSWER*/
        public string CalculateSentence(List<Word> wordlist)
        {
            string answer = "Das ist interessant, erzählen Sie mir mehr davon!";

            foreach (Word w in wordlist)
            {
                switch (w.Type)
                {
                    case 'N':
                        // normal answers
                        if (_Responsibles.Contains(w.Value))
                        {
                            switch (w.Value)
                            {
                                case "Vater": answer = "Sie sollten Ihren Vater wieder mal besuchen!";
                                    break;
                                case "Mutter": answer = "Ihre Mutter ist ein guter Mensch, denke ich.";
                                    break;
                                case "Kinder": answer = "Kinder sind ein Segen - aber leben Sie zuerst Ihr Leben!";
                                    break;
                                case "Famile": answer = "Eine intakte Familie ist die Vorraussetzung für ein intaktes Seelenleben.";
                                    break;
                                case "Haustier": answer = "Tiere brauchen viel Liebe, denken Sie immer daran!";
                                    break;
                                case "Hund": answer = "Hunde sind treue Freunde!";
                                    break;
                                case "Katze": answer = "Katzen mag ich am liebsten!";
                                    break;
                                case "Alkohol": answer = "Versprechen Sie mir, weniger Alkohol zu trinken?";
                                    break;
                                case "Sorgen": answer = "Welche Sorgen haben Sie denn genau?";
                                    break;
                                case "Geld": answer = "Ja, von dem Teufelszeug ist immer zu wenig da. Achja, ich nehme nur Bargeld!";
                                    break;
                                case "Leben": answer = "Leben...erzählen Sie mir nichts vom Leben...";
                                    break;
                                case "Antwort": answer = "42";
                                    break;
                            }
                        }
                        // backupanswers
                        else 
                        {
                            answer = answertypes[answerindex];
                            answerindex++;
                            if(answerindex == 5) 
                            { answerindex =0; }
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
