/* NS: PluginTellBirthdayTest */
/* FN: CalculatingTest.cs */
/* FUNCTION: A plugin which tells which day the user's birthday was */

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Interface;
using PluginTellDay;

namespace PluginTellBirthdayTest
{
    [TestFixture]
    public class CalculatingTest
    {
        [Test ]
        public void GetDay()
        {
            PluginTellBirthday _Tell = new PluginTellBirthday();
            List<Word> wlist = new List<Word>();

            Word w1 = new Word("Welcher");
            Word w2 = new Word("Tag");
            w2.Type = 'N';
            Word w3 = new Word("Geburtstag");
            w3.Type = 'N';
            Word w4 = new Word("am");
            Word w5 = new Word("01/01/2000");
            w5.Type = 'N';
            Word w6 = new Word("?");

            wlist.Add(w1);
            wlist.Add(w2);
            wlist.Add(w3);
            wlist.Add(w4);
            wlist.Add(w5);
            wlist.Add(w6);

            string answ = _Tell.CalculateSentence(wlist);
            Console.WriteLine(answ);
        }
    }
}
