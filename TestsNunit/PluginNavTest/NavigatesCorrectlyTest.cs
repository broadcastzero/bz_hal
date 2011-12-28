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
        private List<Word> wordlist = null;
        private PluginNavi navi = null;

        [SetUp]
        public void SetUp()
        {
            this.wordlist = new List<Word>();
            navi = new PluginNavi();
        }

        public void GetPriorityTest()
        {
            Word w1 = new Word("Wo");
            w1.Type = 'Q';
            wordlist.Add(w1);
            Word w2 = new Word("ist");
            w1.Type = 'V';
            wordlist.Add(w2);
            Word w3 = new Word("die");
            w1.Type = 'R';
            wordlist.Add(w3);
            Word w4 = new Word("Alserstrasse");
            w1.Type = 'N';
            wordlist.Add(w4);
            Word w5 = new Word("?");
            w1.Type = 'M';
            wordlist.Add(w5);

            int prior = navi.GetPriority(wordlist);
            Assert.AreEqual(prior, 7);
        }
    }
}
