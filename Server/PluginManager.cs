/* NS: Server */
/* FN: PluginManager.cs */
/* FUNCTION: Receive analysed wordlist from ClientComm, scan existing plugins and send wordlist to them. */
/*              Receive priority answer from plugins and choose the one with the highest priority. */
/*              Return answer to ClientComm-class */

using System;
using System.Collections.Generic;

namespace Server
{
    public class PluginManager
    {
        /* PRIVATE VARS */
        private string _PlugPath;
        private static List<string> _PluginList; //pluginlist has only to be created 1 time

        /* CONSTRUCTOR - precache plugings, throw exception if no plugins could be loaded */
        public PluginManager()
        {
            _PlugPath = Environment.CurrentDirectory + "\\Plugins";
            Console.WriteLine(_PlugPath);
            _PluginList = new List<string>();
            /* read folder */
        }

        /* Send wordlist to plugins and return answerstring to ClientComm */
        public string SendListToPlugins(List<Word> wlist)
        {
            foreach (Word w in wlist)
            { Console.WriteLine(w.Value + "-" + w.Type + "-" + w.Position); }
            string answer = "Hier koennte Ihre Antwort stehen.";
            return answer;
        }
    }
}
