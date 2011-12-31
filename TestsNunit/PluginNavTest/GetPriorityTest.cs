/* NS: PluginNavTest */
/* FN: GetPriorityTest.cs */
/* FUNCTION: Test the Navi Plugin, if priority is calculated correctly */

using System;
using System.Collections.Generic;
using NUnit.Framework;
using PluginNav;
using Interface;

namespace PluginNavTest
{
    [TestFixture]
    public class GetPriorityTest
    {
        public List<Word> wordlist = null;
        public PluginNavi navi = null;

        [SetUp]
        public void SetUp()
        {
            this.wordlist = new List<Word>();
            navi = new PluginNavi();
        }

        /* Check highest priority */
        [Test]
        public void GetAPriorityTest()
        {
            Word w1 = new Word("Wo");
            w1.Type = 'Q';
            wordlist.Add(w1);
            Word w2 = new Word("ist");
            w2.Type = 'V';
            wordlist.Add(w2);
            Word w3 = new Word("die");
            w3.Type = 'R';
            wordlist.Add(w3);
            Word w4 = new Word("Alserstrasse");
            w4.Type = 'N';
            wordlist.Add(w4);
            Word w5 = new Word("?");
            w5.Type = 'M';
            wordlist.Add(w5);

            int prior = navi.GetPriority(wordlist);
            Assert.AreEqual(10, prior);
        }

        /* Check medium priority */
        [Test]
        public void GetAnotherPriorityTest()
        {
            Word w1 = new Word("Ich");
            w1.Type = 'S';
            wordlist.Add(w1);
            Word w2 = new Word("habe");
            w2.Type = 'V';
            wordlist.Add(w2);
            Word w3 = new Word("eine");
            w3.Type = 'X';
            wordlist.Add(w3);
            Word w4 = new Word("Straße");
            w4.Type = 'N';
            wordlist.Add(w4);
            Word w5 = new Word("!");
            w5.Type = 'M';
            wordlist.Add(w5);

            int prior = navi.GetPriority(wordlist);
            Assert.AreEqual(2, prior);
        }

        /* Check if "Straßen" is not counted 2 times */
        [Test]
        public void GetLastPriorityTest()
        {
            Word w1 = new Word("Straßen");
            w1.Type = 'S';
            wordlist.Add(w1);
            Word w2 = new Word("sind");
            w2.Type = 'V';
            wordlist.Add(w2);
            Word w3 = new Word("Straßen");
            w3.Type = 'N';
            wordlist.Add(w3);
            Word w4 = new Word("Kind");
            w4.Type = 'N';
            wordlist.Add(w4);
            Word w5 = new Word("!");
            w5.Type = 'M';
            wordlist.Add(w5);

            int prior = navi.GetPriority(wordlist);
            Assert.AreEqual(2, prior);
        }
    }
}
