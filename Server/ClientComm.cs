/* NS: Server */
/* FN: ClientComm.cs */
/* FUNCTION: Recieve sentence from client and send it to TextParser (if client doesn't want to quit!) */

using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using Interface;

namespace Server
{
    public class ClientComm
    {
        /* PRIVATE VARS */
        private TextParser _Tp;
        private PluginManager _Pm;
        private Socket _Sock;
        private NetworkStream _Stream;
        private StreamReader _Sr;
        private StreamWriter _Sw;
        /* PUBLIC VARS */

        /* CONSTRUCTOR */
        public ClientComm(Socket sock)
        {
            _Sock = sock;
            _Tp = new TextParser(); //has no constructor

            try
            {
                _Stream = new NetworkStream(_Sock);
                // precaches Plugins in constructor, if no plugin could be loaded -> Exception
                _Pm = new PluginManager();
                _Pm.LoadPlugins();
            }
            // PluginIns could not be loaded - quit!
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                if (_Stream != null)
                { _Stream.Close(); }
                Console.WriteLine("Will quit now...");
                Console.WriteLine("---------------------");
                Console.ReadLine();
                Environment.Exit(1);
            }
            catch (Exception) { throw; }

            // if server quits, destroy objects
            System.AppDomain.CurrentDomain.ProcessExit += new EventHandler(ServerQuittingHandler);
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ServerQuittingHandler);
        }

        void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /* Entry-class for each connected thread */
        public void WelcomeClient()
        {
            try
            {
                Console.WriteLine("A client connected from {0}", _Sock.RemoteEndPoint);
            }
            catch (Exception)
            { _Stream.Close(); throw; } //also calls Sock_.Close and Stream_.Close()

            // receive from client in loop
            do
            {
                try
                {
                    // structure of a StreamReader-message: 1) host, 2) Message
                    _Sr = new StreamReader(_Stream);
                    string host = _Sr.ReadLine();
                    string stringline = _Sr.ReadLine();

                    // quit if client has quit connection
                    if (stringline == null) { break; }

                    // does client want to quit? -> do not parse text, continue after loop
                    string raw = stringline.ToLower();
                    if (raw.Contains("quit")) { break; }

                    // send string to text parser
                    if (stringline != null)
                    {
                        _Tp.SplitSentence(stringline);
                        //output
                        Console.WriteLine(stringline);
                        this.SendToPluginManager(host, _Tp.AnalysedWords);
                    }
                }
                // is thrown, if sentence doesn't end with '.' or '?'
                catch (InvalidSentenceException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FileNotFoundException e)
                {
                    // there are no plugins - quit
                    Console.WriteLine(e.Message);
                    break;
                }
                catch (IOException)
                {
                    // client has quit, so quit too
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("IOException");
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    break;  //quit
                }
            } while (true);

            // Close open connections
            try
            {
                _Sr.Close(); // also calls Sock_.Close and Stream_.Close()
                _Sw.Close();
            }
            catch (Exception e) { Console.WriteLine("Das Objekt " + e.Source + " wurde bereits geschlossen."); }
            Console.WriteLine("---------------------------------");
            Console.WriteLine("A client has quit the connection.");
            Console.WriteLine("---------------------------------");
            // Thread will quit now
        }

        /* Send split sentence (List<Words>) to PluginManager, receive answer */
        private void SendToPluginManager(string host, List<Word> wordlist)
        {
            string answer = null;
            try
            {
                answer = _Pm.SendListToPlugins(wordlist);
            }
            catch (FileNotFoundException) //wrong path - quit!
            { throw; }
            
            // send received answer back to Client
            try
            {
                this.SendAnswerToClient(host, answer);
            }
            catch (Exception) //problem with clientconnection
            { throw; }
        }

        /* Send answer back to client */
        private void SendAnswerToClient(string host, string answ)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine(host);
            Console.WriteLine(answ);
            Console.WriteLine("---------------------------------");

            try
            {
                _Sw = new StreamWriter(_Stream);
                _Sw.WriteLine(answ);
                _Sw.Flush();
            }
            catch (Exception) //connection failed
            {
                throw;
            }
        }

        /* Cleanup when Server quits */
        private void ServerQuittingHandler(object sender, ConsoleCancelEventArgs args)
        {
            this._Pm = null;
            this._Tp = null;
            Console.WriteLine("---------------------");
            Console.WriteLine("Garbage Collector");
            Console.WriteLine("---------------------");
            GC.Collect(); //force GarbageCollector to do his work
            Console.WriteLine("finished!");
            Console.WriteLine("---------------------");
                
            Console.ReadLine();
        }

        /* Overload for ProcessExit */
        private void ServerQuittingHandler(object sender, EventArgs args)
        {
            this.ServerQuittingHandler(null, null);
        }
    }
}
