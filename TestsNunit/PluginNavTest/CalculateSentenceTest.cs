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
            catch (Exception e)
            { Console.WriteLine(e.Message); }

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
            Assert.That(1 == 3); // add senseful code here
        }

    }
}
