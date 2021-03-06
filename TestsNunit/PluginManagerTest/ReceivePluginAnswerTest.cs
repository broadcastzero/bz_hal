﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using Server;
using Interface;

namespace PluginManagerTest
{
    [TestFixture]
    public class ReceivePluginAnswerTest
    {
        private PluginManager _Pm = null;
        private TextParser _Tp = null;

        [SetUp]
        public void SetUp()
        {
            this._Tp = new TextParser();
            this._Pm = new PluginManager();
            // set PluginPath differently (for Directory is not the same!)
            this._Pm.PlugPath = "C:\\Users\\broadcastzero\\0 FH\\3. Semester\\SWE\\bz_hal\\Server\\bin\\Debug\\Plugins";
            this._Pm.LoadPlugins();
        }

        [Test]
        public void GeneralAnswerTest()
        {
            string input = "Mein Hund ist tot.";
            this._Tp.SplitSentence(input);
            string answer = null;
            answer = _Pm.SendListToPlugins(this._Tp.AnalysedWords);
            Assert.That(answer != null);
            Console.WriteLine("------------------------------");
            Console.WriteLine(answer);
            Console.WriteLine("------------------------------");
        }

        [Test]
        public void MathTest()
        {
            string input = "Wieviel ist 12 * 12?";
            this._Tp.SplitSentence(input);
            string answer = null;
            answer = _Pm.SendListToPlugins(this._Tp.AnalysedWords);
            Assert.That(answer != null);
            Console.WriteLine("------------------------------");
            Console.WriteLine(answer);
            Console.WriteLine("------------------------------");

            Assert.That(answer == "Da kommt wohl 144 raus!");
        }
    }
}
