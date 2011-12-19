/* NS: TextParserTest */
/* FN: SplitsCorrectlyTest.cs */
/* FUNCTION: Test the text parser, if words are split correctly and if they are saved in list */

using System;
using NUnit.Framework;
using Server;

namespace TextParserTest
{
    [TestFixture]
    public class SplitsCorrectlyTest
    {
        private TextParser _Tp = null;

        /* Initialise */
        [SetUp]
        public void SetUp()
        {
            _Tp = new TextParser();
        }

        /* Test if words are split correctly */
        [Test]
        public void SplitTest()
        {
            _Tp = new TextParser();
            string s = "Das ist mein Testsatz.";
            _Tp.SplitSentence(s);
            Assert.IsNotEmpty(_Tp.AnalysedWords);
            Assert.That(_Tp.AnalysedWords[0].Value == "Das");
            Assert.That(_Tp.AnalysedWords[1].Value == "ist");
            Assert.That(_Tp.AnalysedWords[2].Value == "mein");
            Assert.That(_Tp.AnalysedWords[3].Value == "Testsatz");
            foreach (Word w in _Tp.AnalysedWords)
            { Console.WriteLine(w.Value); }
            Console.WriteLine("--End of sentence--");
        }
    }
}
