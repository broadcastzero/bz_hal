using System;
using System.Collections.Generic;
using NUnit.Framework;
using PluginNav;
using Interface;

namespace PluginNavTest
{
    [TestFixture]
    public class NavigatesCorrectlyTest
    {
        public List<Word> wordlist = null;
        public PluginNavi navi = null;

        [SetUp]
        public void SetUp()
        {
            this.wordlist = new List<Word>();
            navi = new PluginNavi();
        }

        [Test]
        public void GetPriorityTest()
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
            Assert.AreEqual(8, prior);
        }
    }
}
