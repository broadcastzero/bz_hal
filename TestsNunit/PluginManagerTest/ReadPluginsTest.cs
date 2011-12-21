using System;
using System.Collections.Generic;
using NUnit.Framework;
using Server;
using System.IO;

namespace PluginManagerTest
{
    [TestFixture]
    public class ReadPluginsTest
    {
        private PluginManager _Pm = null;
        private string _PluginPath = null;

        [Test]
        public void ReadPluginFolderTest()
        { 
            //set PluginPath to server-folder
            string path="";
            _PluginPath = Environment.CurrentDirectory;
            string[] parts = _PluginPath.Split('\\');
            int i = 0;
            do
            {
                path += parts[i];
                path += "\\";
                i++;
            } while (parts[i] != "bz_hal");
            path += "bz_hal\\Server\\bin\\Debug\\Plugins\\";
            Console.WriteLine(path);
            Console.WriteLine(_PluginPath);

            _Pm = new PluginManager();
            Assert.That(PluginManager.PluginList != null);
           Assert.That(Directory.Exists(path));
        }
    }
}
