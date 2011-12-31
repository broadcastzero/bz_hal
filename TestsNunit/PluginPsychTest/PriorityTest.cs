/* NS: PluginPsychTest */
/* FN: PluginPsych.cs */
/* FUNCTION: Prioritiy tests for the psychiater plugin */

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Interface;
using PluginPsych;

namespace PluginPsychTest
{    
    [TestFixture]
    public class PriorityTest
    {
        private PluginPsycho _Psych = null;

        [SetUp]
        public void SetUp()
        {
            _Psych = new PluginPsycho();
        }

        [Test]
        public void GetPriorityTest()
        {
            List<Word> _wlist = new List<Word>();

            Word w1 = new Word("Meine");
            Word w2 = new Word("Familie");
            w2.Type = 'N';
            Word w3 = new Word("ist");
            Word w4 = new Word("mir");
            Word w5 = new Word("wichtig");
            Word w6 = new Word(".");

            _wlist.Add(w1);
            _wlist.Add(w2);
            _wlist.Add(w3);
            _wlist.Add(w4);
            _wlist.Add(w5);
            _wlist.Add(w6);

            int prior = _Psych.GetPriority(_wlist);
            Assert.AreEqual(5, prior);
        }

        [Test]
        public void GetAnotherPriorityTest()
        {
            List<Word> _wlist = new List<Word>();

            Word w1 = new Word("Mein");
            Word w2 = new Word("Hund");
            w2.Type = 'N';
            Word w3 = new Word("gehört");
            Word w4 = new Word("zur");
            Word w5 = new Word("Familie");
            w5.Type = 'N';
            Word w6 = new Word(".");

            _wlist.Add(w1);
            _wlist.Add(w2);
            _wlist.Add(w3);
            _wlist.Add(w4);
            _wlist.Add(w5);
            _wlist.Add(w6);

            int prior = _Psych.GetPriority(_wlist);
            Assert.AreEqual(10, prior);
        }

        [TearDown]
        public void TearDown()
        {
            _Psych = null;
        }
    }
}
