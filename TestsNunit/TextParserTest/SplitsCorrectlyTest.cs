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

        /* Tests if type fits */
        [Test]
        public void TypeTest()
        {
            string s = "Mein Hund ist tot.";
            _Tp.SplitSentence(s);
            foreach (Word w in _Tp.AnalysedWords)
            {
                Assert.That(w.Type == 'X' || w.Type == 'S' || w.Type == 'M' || w.Type == 'N' || w.Type == 'V' || w.Type == 'A' || w.Type == 'R' || w.Type == 'P');
                Console.WriteLine(w.Type);
            }
            Assert.That(_Tp.AnalysedWords[1].Type == 'S');
            //Assert.That(_Tp.AnalysedWords[3].Type == 'N');
        }

        /* Test mathematics */
        [Test]
        public void MathTest()
        {
            string s = "Wie viel ist 1 + 1?";
            _Tp.SplitSentence(s);
            Assert.AreEqual(7, _Tp.AnalysedWords.Count);
            foreach (Word w in _Tp.AnalysedWords)
            { Console.WriteLine(w.Value); }
        }

        /* Test InvalidSentenceException - will be called if sentence doesn't end with '.' or '?' */
        [Test]
        [ExpectedException("Server.InvalidSentenceException")]
        public void InvalidSentenceExceptionTest()
        { 
            string s = "Das ist ein Test";
            _Tp.SplitSentence(s);
        }
    }
}
