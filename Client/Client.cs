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
        /* FIELDS */
        private TcpClient tcpcli;
        private NetworkStream stream;
        private StreamWriter sw;

        /* METHODS */
        //connect to server//
        public void Connect()
        {
            this.tcpcli = new TcpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            try
            {
                tcpcli.Connect(serverEndPoint);
            }
            //quit, if connection is refused (Server is down, etc)
            catch (SocketException e)
            {
                tcpcli.Close();
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
                    sw.WriteLine("quit");
                    sw.Flush();
                }
                catch (Exception)
                {
                    //not connected any more, jump to finally and quit                
                }
                finally
                {   //quit
                    tcpcli.Close();
                    Environment.Exit(1);
                }
            };

            //get and send message
            string cont = "";
            this.stream = tcpcli.GetStream();
            this.sw = new StreamWriter(stream);
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
                if (sending.Contains("quit"))
                { break; }

                //send string to server
                try
                {
                    sw.WriteLine("host: {0}", tcpcli.Client.RemoteEndPoint); //RemoteEndPoint=Ip-adress+Port
                    sw.WriteLine(sending);  //send string
                    sw.WriteLine();
                    sw.Flush(); //write out everything, if there is still something in the buffer
                }
                catch (IOException e)
                { 
                    //connection lost, quit
                    tcpcli.Close();
                    Console.WriteLine("Connection lost.");
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                    Environment.Exit(0);
                }

                Console.WriteLine("Continue? (y/n)");
                do{
                cont = Console.ReadLine();
                } while (cont.Length == 0);
            } while(cont[0] == 'y');

            //send quit message to server.
            try
            {
                sw.WriteLine("quit");
                sw.Flush();
            }
            catch (IOException e)
            {
                //connection lost, quit
                Console.WriteLine("Connection lost.");
                Console.WriteLine(e.Message);              
            }
            finally
            { 
                //quit the application
                tcpcli.Close();
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
