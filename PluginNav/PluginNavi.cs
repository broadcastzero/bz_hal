using System;
using System.Collections.Generic;
using Interface;
using System.Threading;
using System.Xml;

namespace PluginNav
{
    public class PluginNavi : IPlugin, IDisposable
    {
        /* PRIVATE VARS */
        // a SortedList requires less memory than a Dictionary, but requires more time to lookup
        // to improve -> quit after first not matching street
        // for there might be more than one city with this streetname
        // see http://www.dotnetperls.com/sortedlist 
        // and http://wiki.openstreetmap.org/wiki/Points_of_interest for further details

        private static SortedList<string, string> _Map = null;
        private string _Name;

        /* PUBLIC VARS */
        public bool disposed;
        public string PathToXml;

        public static SortedList<string, string> Map
        {
            get { return _Map; }
            set { }
        }

        public string Name { get { return _Name; } }

        /* CONSTRUCTOR */
        public PluginNavi()
        {
            this.disposed = false;
            _Name = "Navigator";
            PathToXml = "./Map/austria.osm";
            _Map = new SortedList<string, string>();
        }

        /* Calculate priority of Navigator */
        public int GetPriority(List<Word> wordlist)
        {
            int priority = 0;

            // defines, whether string "wo" or streetname have been found before - only count once!
            bool wo_counted = false;
            bool str_counted = false;
            bool isquestion = false;
            int loadmap=0;

            foreach(Word w in wordlist)
            {
                if (w.Type == 'Q' && w.Value.ToLower() == "wo" && wo_counted == false)
                { priority += 5; wo_counted = true; isquestion = true; }
                else if((w.Value.ToLower().Contains("strasse") || w.Value.ToLower().Contains("straße") || 
                    w.Value.ToLower().Contains("dorf")) && str_counted == false)
                { priority += 2; str_counted = true; }
                //is it a question? -> priority++
                else if (w.Type == 'M' && w.Value == "?")
                { priority += 1; }
                // if map shall be reloaded
                else if (w.Value.ToLower() == "karte" || w.Value.ToLower() == "neu" ||
                    w.Value.ToLower() == "auf" || w.Value.ToLower() == "bereite")
                {
                    loadmap++;
                }
            }

            // if everything fits -> 10 (highest)
            if (wo_counted == true && str_counted == true && isquestion == true)
            { priority = 10; }
            // if map shall be reloaded
            else if (loadmap > 3) { priority = 10; }

            return priority;
        }

        /* Load OpenStreetMap into _Map variable */
        public void LoadMap()
        {
            Console.WriteLine("Karte wird geladen...");
            // is _Map locked (loaded at the moment)? -> return error
            if (Monitor.TryEnter(_Map) == false)
            { throw new InvalidOperationException("Die Karte wird gerade von jemand anders neu aufbereitet!"); }

            // make it threadsafe
            lock (_Map)
            {
                // get new instance
                _Map.Clear();
                Console.WriteLine("Die Karte wird neu aufbereitet...");
                //for (int i = 0; i <= 900000000; i++) ;
                //_Map.Add("Alserstrasse", "Wien");
                //_Map.Add("Linzerstrasse", "Tirol");

                // Example for Point of Interest: 
                // <node...>
                // <tag k="name" v="Graz" />
                // </node>
                try
                {
                    XmlReader reader = XmlReader.Create(this.PathToXml);
                }
                catch (Exception)
                { throw; } //forward to Pluginmanager

            }

            //return success
            //string answer = "Die Karte wurde neu aufbereitet.";
            //return answer;
        }

        /* Find out where Client wants to be navigated, search in internal List and return answer */
        public string CalculateSentence(List<Word> wordlist)
        {
            // Standard answer
            string answer = "Wenn du navigieren willst, dann stelle deine Frage bitte präziser.";

            // shall map be reloaded?
            int load = 0;
            foreach(Word w in wordlist)
            {
                if (w.Value.ToLower() == "karte" || w.Value.ToLower() == "neu" ||
                    w.Value.ToLower() == "auf" || w.Value.ToLower() == "bereite")
                {
                    load++;
                }
            }

            // is list empty? -> load!
            if (_Map.Count == 0 || load > 3)
            {
                try
                {
                    this.LoadMap();
                }
                catch (System.InvalidOperationException e)
                {
                    answer = e.Message;
                    Console.WriteLine(e.Message); 
                }
            }

            // get street from list
            //foreach 

            /* searching in list will be coded here */
            return answer;
        }

        /* DISPOSE - clear Map-List! */
        public void Dispose()
        {
            // only first time!
            if (!this.disposed)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
                this.disposed = true;
            }
        }
        // overloaded Dispose-function
        protected virtual void Dispose(bool disposing)
        {
            // clear inner ressources
            if (disposing)
            {
                _Map.Clear();
                _Map = null;
                Console.WriteLine("Maplist has been cleared by disposing");
            }

            // clear external ressources (files, database, ...)
            // -> there are none!
            // Dispose probably won't be called from Server-Application => in this case, clear map-list anyway
            if (_Map != null)
            {
                _Map.Clear();
                _Map = null;
                Console.WriteLine("Maplist has been cleared by destructing");
            }
        }

        /* DESTRUCTOR - call Dispose(bool disposing) */
        ~PluginNavi()
        {
            this.Dispose(false);
        }
    }
}
