using System;
using System.Collections.Generic;
using NUnit.Framework;
using PluginNav;
using Interface;

namespace PluginNavTest
{
    [TestFixture]
    public class CalculateSentenceTest
    {
        private PluginNavi navi = null;

        [SetUp]
        public void SetUp()
        {
            navi = new PluginNavi();
        }

        /* Test if loading the xml-file works */
        [Test]
        public void LoadListTest()
        { 
            
        }

        /* Test if searching works */
        [Test]
        public void SearchTest()
        {
            Assert.That(1 == 3); // add senseful code here
        }

    }
}
