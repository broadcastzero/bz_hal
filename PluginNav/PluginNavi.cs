using System;
using System.Collections.Generic;
using Interface;
using System.Threading;
using System.Xml;
using System.Diagnostics;

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
        public bool disposed { get; set; }
        public string PathToXml { get; set; }

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
            // stop time elapsing while loading map
            Stopwatch watch = new Stopwatch();
            watch.Start();
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

                // Example for Point of Interest: 
                // <node...>
                // ...
                // <tag k="is_in" v="Steiermark"
                // ...
                // <tag k="name" v="Graz" />
                // </node>

                XmlReader reader = null;
                bool check = false; // if nodename = <tag>, check if it is a PointOfInterest, ignore otherwise
                try
                {
                    reader = XmlReader.Create(this.PathToXml);
                    string lastcity = null;
                    string laststreet = null;

                    // read xml-file - get next <tag>
                    while (reader.ReadToFollowing("tag"))
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.HasAttributes) // (<osm>, <node>,) <tag>
                        {
                            // run through all attributes until you find "is_in" (city)
                            while(reader.MoveToNextAttribute())
                            {
                                string name = null;
                                string att = null;
                                name = reader.Name; // k, v -> check .osm-file, line 10.947 i.e.
                                att = reader.Value; // name, Point, positive, motorway_junction, Brannenburg, ... (Value!)
                                // only combination k + name is useful for us
                                if (name == "k" && att == "is_in")
                                { check = true; break; }
                            }

                            // if city has been found, save city and get street
                            if (check == true)
                            {
                                string city = null;
                                string street = null;

                                // save city
                                while (reader.MoveToNextAttribute())
                                { 
                                    if(reader.Name == "v")
                                    { city = reader.Value; }
                                }

                                // get streetname (is stored in one of the following tags)
                                // if not, reader will land at next <node> => not found, break
                                bool found = false;
                                while (reader.Read() && reader.Value != "node" && found == false)
                                {
                                    while (reader.MoveToNextAttribute())
                                    {
                                        // no city is stored here - continue with next tag
                                        if (reader.Name == "k" && reader.Value != "name")
                                        { break; }

                                        // city is stored - read next attribute, which will be v=<cityname>
                                        if (reader.Name == "v")
                                        { street = reader.Value; found = true; }
                                    }
                                }

                                // if data has been found - save in list
                                if (city != null && street != null)
                                {
                                    // avoid double entries
                                    if (lastcity == city && laststreet == street)
                                    { continue; }

                                    int i=0;
                                    // if a street with this name already exists, number it
                                    // i.e. Linzerstraße, Linzerstraße(1), Linzerstraße(2)
                                    // must be split later to receive multiple data!!
                                    string addon = "";
                                    while (_Map.ContainsKey(street) && _Map.ContainsKey(street+addon))
                                    {
                                        i++;
                                        addon = "(" + i + ")";
                                    }
                                    if (addon.Length != 0)
                                    {
                                        street += addon;
                                    }
                                    _Map.Add(street, city);
                                    lastcity = city;
                                    laststreet = street;
                                    Console.WriteLine(street + " in " + city + " added.");
                                }
                            } // end save city and street

                            check = false; // set to false again
                        }
                    }
                }
                catch (Exception)
                {                   
                    throw; //forward to Pluginmanager
                }
                finally
                {
                    if (reader != null)
                    { reader.Close(); }
                }
            } //end lock

            watch.Stop();
            Console.WriteLine("Finished loading map. Time needed: {0}", watch.Elapsed);
            // success
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
            if(_Map.Count == 0 && load <= 3) {return "Die Karte wurde noch nicht aufbereitet!";}

            if (load > 3)
            {
                try
                {
                    this.LoadMap();
                }
                // failure
                catch (System.InvalidOperationException e)
                {
                    answer = e.Message;
                    Console.WriteLine(e.Message);
                    return answer;
                }
                // success - if client only wanted map to load, return. otherwise, continue searching.

                return "Die Karte wurde neu aufbereitet.";
            }

            // get street from list
            //_Map.IndexOfKey();

            /* searching in list will be coded here */
            return answer;
        }

        /* DISPOSE - clear Map-List! */
        public void Dispose()
        {
            // only first time!
            if (!this.disposed)
            {
                PluginNavi.Dispose(true);
                GC.SuppressFinalize(this);
                this.disposed = true;
            }
        }
        // overloaded Dispose-function
        protected static void Dispose(bool disposing)
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
            PluginNavi.Dispose(false);
        }
    }
}
