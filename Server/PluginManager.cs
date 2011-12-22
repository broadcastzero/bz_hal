/* NS: Server */
/* FN: PluginManager.cs */
/* FUNCTION: Receive analysed wordlist from ClientComm, scan existing plugins and send wordlist to them. */
/*              Receive priority answer from plugins and choose the one with the highest priority. */
/*              Return answer to ClientComm-class */

using System;
using System.Collections.Generic;
using System.IO;
using Interface;
using System.Reflection;
using System.Threading;

namespace Server
{
    public class PluginManager
    {
        /* PRIVATE VARS */
        private string _PlugPath;

        /* PUBLIC VARS */
        public static List<string> PluginList = null; //pluginlist has only to be created 1 time
        // in here, loaded Plugins will be stored
        public Dictionary<string, object> InterfaceInstances;

        /* CONSTRUCTOR - precache plugings, throw exception if no plugins could be loaded */
        public PluginManager()
        {
            // work now with absolute path (because Nunit test would throw exception when using CurrentDir
            // maybe change to Environment.CurrentDirectory later!
            _PlugPath = "C:\\Users\\broadcastzero\\0 FH\\3. Semester\\SWE\\0 Hauptuebung\\HAL_Solution\\Server\\bin\\Debug\\Plugins\\";
            InterfaceInstances = new Dictionary<string, object>();
        }

        public void LoadPlugins()
        {
            if (!Directory.Exists(_PlugPath))
            {
                throw new FileNotFoundException("Das Plugin-Verzeichnis konnte nicht geoeffnet werden!");
            }

            // if static list does not exist, create new instance
            if (PluginList == null)
            {
                PluginList = new List<string>();
            }

            // read folder
            try
            {
                string[] files = Directory.GetFiles(_PlugPath);
                //add to list if it is a .dll-file and doesn't already exist
                Console.WriteLine("---------------------");
                Console.WriteLine("Loading Plugins...");
                Console.WriteLine("---------------------");
                foreach (string f in files)
                {
                    if (f.EndsWith(".dll") && !PluginList.Contains(f))
                    { PluginList.Add(f); Console.WriteLine(f + " added."); }
                }
                Console.WriteLine("---------------------");
            }
            catch (Exception)
            { throw new FileNotFoundException("Die Plugins konnten nicht eingelesen werden!"); }

            // if List does not contain any plugins, quit
            if (PluginList.Count == 0)
            { throw new FileNotFoundException("Kein Plugin gefunden!"); }

            // else - load Plugins dynamically (jump to DynLoad method)
            this.DynLoad();

            // if Directory of loaded Plugins is empty - throw exception
            if (InterfaceInstances.Count == 0)
            { throw new FileNotFoundException("Keines der " + PluginList.Count + " Plugin(s) konnte erfolgreich eingelesen werden!"); }
        }

        /* Loads plugins which are stored in PluginList as string (path) dynamically */
        private void DynLoad()
        {
            // this part has to be protected from other threads
            // for our PluginList must not change while iterating!
            lock (PluginList)
            {
                // load all plugins which are stored as pathstring in PluginList
                foreach (string plug in PluginList)
                {
                    Assembly assembly = Assembly.LoadFrom(plug);

                    // get all classes of this assembly
                    foreach (Type type in assembly.GetTypes())
                    {
                        // only use public and not abstract types
                        if (type.IsPublic && !type.IsAbstract)
                        {
                            // get all classes which implement IPlugin
                            Console.WriteLine(typeof(IPlugin).IsAssignableFrom(type));
                            if (type.GetInterface("IPlugin") != null)
                            {
                                Console.WriteLine("LOADS!");
                                /*try
                                {
                                    object activedInstance = Activator.CreateInstance(type);
                                    if (activedInstance != null)
                                    {
                                        InterfaceInstances.Add(type.Name, activedInstance);
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Plugin " + plug + " could not be loaded");
                                }*/
                            }
                            else { Console.WriteLine("not valid"); }
                         }
                    }
                }
            } //end of lock
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
