using System;
using System.Collections.Generic;
using NUnit.Framework;
using PluginNav;
using Interface;
using System.Threading;

namespace PluginNavTest
{
    [TestFixture]
    public class CalculateSentenceTest
    {
        private PluginNavi _Navi = null;

        [SetUp]
        public void SetUp()
        {
            _Navi = new PluginNavi();
            _Navi.PathToXml = "C:\\Users\\broadcastzero\\0 FH\\3. Semester\\SWE\\bz_hal\\Server\\bin\\Debug\\Map\\austria.osm";
        }

        /* Test if loading the xml-file works */
        [Test]
        public void LoadListTest()
        {
            try
            {
                _Navi.LoadMap();
            }
            catch (InvalidOperationException)
            { Console.WriteLine("Karte wird gerade neu aufbereitet."); }

            Assert.That(PluginNavi.Map.Count != 0);
        }

        /* When two Clients wish to load the map at the same time, throw InvalidOperationException */
        [Test]
        public void MapAlreadyLoadsTest()
        {
            ThreadStart del1 = new ThreadStart(_Navi.LoadMap);
            ThreadStart del2 = new ThreadStart(_Navi.LoadMap);

            Thread WorkThread1 = new Thread(del1);
            Thread WorkThread2 = new Thread(del2);

            // define delegate for exception handling
            bool exceptionWasThrown = false;
            UnhandledExceptionEventHandler unhandledExceptionHandler = (s, e) =>
            {
                if (!exceptionWasThrown)
                {
                    exceptionWasThrown = e.ExceptionObject.GetType() ==
                                             typeof(System.InvalidOperationException);
                }
            };

            AppDomain.CurrentDomain.UnhandledException += unhandledExceptionHandler;

            // start threads - exception should be thrown here
            WorkThread1.Start();
            WorkThread2.Start();

            //synchronise
            WorkThread1.Join();
            WorkThread2.Join();

            // ...and detach the event handler
            AppDomain.CurrentDomain.UnhandledException -= unhandledExceptionHandler;

            // make assertions
            Assert.IsTrue(exceptionWasThrown, "Die Karte wird gerade von jemand anders neu aufbereitet!");
        }

        /* Test if searching works */
        [Test]
        public void SearchTest()
        {
            List<Word> wordlist = new List<Word>();
            Word w1 = new Word("Wo");
            w1.Type = 'Q';

            Word w2 = new Word("ist");
            w2.Type = 'X';

            Word w3 = new Word("Merzenstein");
            w3.Type = 'N';

            Word w4 = new Word("?");
            w4.Type = 'M';

            wordlist.Add(w1);
            wordlist.Add(w2);
            wordlist.Add(w3);
            wordlist.Add(w4);

            _Navi.LoadMap();
            string answer = _Navi.CalculateSentence(wordlist);
            Assert.That(answer.Contains("Zwettl"));
            Console.WriteLine(answer);
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("Tearing down...");
            // free map memory
            _Navi.Dispose();
            GC.Collect();
        }
    }
}
