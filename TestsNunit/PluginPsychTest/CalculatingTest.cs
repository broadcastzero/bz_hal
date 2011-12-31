/* NS: PluginPsychTest */
/* FN: CalculatingTest.cs */
/* FUNCTION: Calculating tests for the psychiater plugin */

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Interface;
using PluginPsych;

namespace PluginPsychTest
{
    [TestFixture]
    public class CalculatingTest
    {
        private PluginPsycho _Psych = null;

        [SetUp]
        public void SetUp()
        {
            _Psych = new PluginPsycho();
        }

        [Test]
        public void CalculatingTest1()
        {
            List<Word> wlist = new List<Word>();

            Word w1 = new Word("Mein");
            Word w2 = new Word("Hund");
            w2.Type = 'N';
            Word w3 = new Word("ist");
            Word w4 = new Word("mein");
            Word w5 = new Word("Freund");
            w5.Type = 'N';
            Word w6 = new Word(".");

            wlist.Add(w1);
            wlist.Add(w2);
            wlist.Add(w3);
            wlist.Add(w4);
            wlist.Add(w5);
            wlist.Add(w6);

            string answ = _Psych.CalculateSentence(wlist);
            Assert.AreEqual(answ, "Hunde sind treue Freunde!");
            Console.WriteLine(answ);
        }

        [Test]
        public void CalculatingTest2()
        {
            List<Word> wlist = new List<Word>();

            Word w1 = new Word("Meine");
            Word w2 = new Word("Familie");
            w2.Type = 'N';
            Word w3 = new Word("gehört");
            Word w4 = new Word("zu");
            Word w5 = new Word("mir");
            Word w6 = new Word(".");

            wlist.Add(w1);
            wlist.Add(w2);
            wlist.Add(w3);
            wlist.Add(w4);
            wlist.Add(w5);
            wlist.Add(w6);

            string answ = _Psych.CalculateSentence(wlist);
            Assert.AreEqual(answ, "Eine intakte Familie ist die Vorraussetzung für ein intaktes Seelenleben.");
            Console.WriteLine(answ);
        }

        [TearDown]
        public void TearDown()
        {
            _Psych = null;
        }
    }
}
