/* NS: Client */
/* FN: Client.cs */
/* FUNCTION: Connect to server, ask for sentence and sent it to the server. Receive answer from the server afterwards. */

using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    public class Client
    {
        /* PRIVATE VARS */
        private TcpClient _Tcpcli;
        private NetworkStream _Stream;
        private StreamWriter _Sw;
        private StreamReader _Sr;

        /* METHODS */
        //connect to server//
        public void Connect()
        {
            this._Tcpcli = new TcpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            try
            {
                _Tcpcli.Connect(serverEndPoint);
            }
            //quit, if connection is refused (Server is down, etc)
            catch (SocketException e)
            {
                _Tcpcli.Close();
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Environment.Exit(1);
            }
            Console.WriteLine("Connected.");

            //continue with private class method "SendMessage", see below
            this.SendMessage();
        }

        //not public, because class relies on tcp-client object
        //call from outer would cause error
        private void SendMessage()
        {
            //if client quits unexpectedely (i.e. strg+c),
            //delegate-method will be run, which sends quit-message to server before quitting
            Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs e)
            {
                try
                {
                    _Sw.WriteLine("quit");
                    _Sw.Flush();
                }
                catch (Exception)
                {
                    //not connected any more, jump to finally and quit                
                }
                finally
                {   //quit
                    _Tcpcli.Close();
                    Environment.Exit(1);
                }
            };

            //get and send message
            string cont = "";
            this._Stream = _Tcpcli.GetStream();
            this._Sw = new StreamWriter(_Stream);
            this._Sr = new StreamReader(_Stream);

            do{
                Console.WriteLine("Please enter a sentence: ");
                string sending = "";

                //request input
                do
                {
                    sending = Console.ReadLine();
                } while (sending.Length == 0);  //skip returns

                //if client wants to quit, skip normal sending process and continue after while
                //with sending quit-message to server
                string raw = sending.ToLower();
                if (raw.Contains("quit"))
                { break; }

                //send string to server
                try
                {
                    _Sw.WriteLine("host: {0}", _Tcpcli.Client.RemoteEndPoint); //RemoteEndPoint=Ip-adress+Port
                    _Sw.WriteLine(sending);  //send string
                    _Sw.WriteLine();
                    _Sw.Flush(); //write out everything, if there is still something in the buffer
                }
                catch (IOException e)
                { 
                    //connection lost, quit
                    _Tcpcli.Close();
                    Console.WriteLine("Connection lost.");
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                    Environment.Exit(0);
                }

                //get and print out answer from server
                try
                {
                    this.ReceiveAnswer();
                }
                catch (Exception)
                {
                    this.QuitConnection();
                }

                //another sentence?
                Console.WriteLine("Continue? (y/n)");
                do{
                cont = Console.ReadLine();
                } while (cont.Length == 0);
            } while(cont[0] == 'y');

            this.QuitConnection();
        }

        private void ReceiveAnswer()
        {
            //read
            try
            {
                string answer =_Sr.ReadLine();
                if (answer != null)
                {
                    Console.WriteLine(answer);
                }
                else { Console.WriteLine("Keine Antwort empfangen."); }
            }
            catch (Exception)
            { _Sr.Close(); throw; }
        }

        private void QuitConnection()
        {
            //send quit message to server.
            try
            {
                _Sw.WriteLine("quit");
                _Sw.Flush();
            }
            catch (Exception e)
            {
                //connection lost, quit
                Console.WriteLine("Connection lost.");
                Console.WriteLine(e.Message);
            }
            finally
            {
                //quit the application
                _Tcpcli.Close();
                //close reading end
                try
                {
                    _Sr.Close();
                    _Sw.Close();
                }
                catch (Exception e) 
                { 
                    Console.WriteLine("Der Stream " + e.Source + "wurde bereits geschlossen.");
                }
                Console.WriteLine();
                Console.WriteLine("-------------------------");
                Console.WriteLine("You quit the connection.");
                Console.WriteLine("-------------------------");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}
