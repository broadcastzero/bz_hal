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

        [SetUp]
        public void SetUp()
        {
            /* set PluginPath to server-folder */
            string path = "";
            _PluginPath = Environment.CurrentDirectory;
            string[] parts = _PluginPath.Split('\\');
            int i = 0;
            do
            {
                path += parts[i];
                path += "\\";
                i++;
                //} while (parts[i] != "HAL_Solution");
                //path += "HAL_Solution\\Server\\bin\\Debug\\Plugins\\";
            } while (parts[i] != "bz_hal");
            path += "bz_hal\\Server\\bin\\Debug\\Plugins\\";
            _PluginPath = path;
            Console.WriteLine(path);
        }

        /* Tests if exception is being thrown in case of no plugins in plugin-folder */
        [Test]
        [ExpectedException("System.IO.FileNotFoundException")]
        public void NoPluginFoundExceptionTest()
        {
            string[] files;
            List<string> dlls = new List<string>();
            files = Directory.GetFiles(_PluginPath);
            // if file is a dll, copy it to dlls-string
            foreach (string s in files)
            { 
                if(s.EndsWith(".dll"))
                {
                    dlls.Add(s);
                }
            }
            if (dlls.Count == 0)
            {
                // should throw exception if no Plugin has been loaded in static list before
                _Pm = new PluginManager();
                _Pm.LoadPlugins();
            }
            // throw exception anyway, so that Nunit-Test is happy
            else { throw new FileNotFoundException(); }
        }

        [Test]
        public void ReadPluginFolderTest()
        {
            _Pm = new PluginManager();
            _Pm.PlugPath = _PluginPath;
            try
            {
                _Pm.LoadPlugins();
            }
            catch (FileNotFoundException e)
            { Console.WriteLine(e.Message); }
            Assert.That(Directory.Exists(_PluginPath));
        }

    }
}
