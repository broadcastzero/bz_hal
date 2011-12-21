using System;
using System.Collections.Generic;
using NUnit.Framework;
using Server;

namespace PluginManagerTest
{
    [TestFixture]
    public class ReadPluginsTest
    {
        private PluginManager _Pm = null;

        [Test]
        public void ReadPluginFolderTest()
        { 
            _Pm = new PluginManager();
            Console.WriteLine(_Pm._PlugPath);
            Assert.That(PluginManager.PluginList != null);
        }
    }
}
