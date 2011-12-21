/* NS: Server */
/* FN: PluginManager.cs */
/* FUNCTION: Receive analysed wordlist from ClientComm, scan existing plugins and send wordlist to them. */
/*              Receive priority answer from plugins and choose the one with the highest priority. */
/*              Return answer to ClientComm-class */

using System;
using System.Collections.Generic;
using System.IO;

namespace Server
{
    public class PluginManager
    {
        /* PRIVATE VARS */
        private string _PlugPath;
        /* PUBLIC VARS */
        public static List<string> PluginList = null; //pluginlist has only to be created 1 time

        /* CONSTRUCTOR - precache plugings, throw exception if no plugins could be loaded */
        public PluginManager()
        {
            //work now with absolute path (because Nunit test would throw exception when using CurrentDir
            //maybe change to Environment.CurrentDirectory later!
            _PlugPath = "C:\\Users\\broadcastzero\\0 FH\\3. Semester\\GPR3\\bz_hal\\Server\\bin\\Debug\\Plugins\\";
            if (!Directory.Exists(_PlugPath))
            {
                throw new FileNotFoundException("Das Plugin-Verzeichnis konnte nicht geoeffnet werden!");
            }
            Console.WriteLine(_PlugPath);
            //if list already exists, there is no work to be done
            if (PluginList != null)
            {
                return;
            }
            PluginList = new List<string>();
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
