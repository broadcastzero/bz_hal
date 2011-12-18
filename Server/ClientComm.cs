/* NS: Server */
/* FN: ClientComm.cs */
/* FUNCTION: Recieve sentence from client and send it to TextParser (if client doesn't want to quit!) */

using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    public class ClientComm
    {
        /* PRIVATE VARS */
        private Socket _Sock;
        private NetworkStream _Stream;
        private StreamReader _Sr;
        /* PUBLIC VARS */
        public StreamReader Sr { get; set; }
        public Socket Sock { get; set; }
        public NetworkStream Stream { get; set; }

        public ClientComm(Socket sock)
        {
            Sock = sock;
        }

        /* Entry-class for each connected thread */
        public void WelcomeClient()
        {
            try
            {
                Console.WriteLine("A client connected from {0}", Sock.RemoteEndPoint);
                Stream = new NetworkStream(Sock);
                Sr = new StreamReader(Stream);
            }
            catch (Exception e)
            { throw e; }

            TextParser tp = new TextParser();

            //receive from client in loop
            string stringline;
            do
            {
                try
                {
                    stringline = sr.ReadLine();
                    /* SEND STRING TO TEXT PARSER */
                    if (stringline != null)
                    {

                        tp.SplitSentence();
                        /* UNTIL NOW, THE SERVER ONLY PRINTS WHAT HE HAS RECEIVED */
                        //Output
                        Console.WriteLine(stringline);
                    }
                }
                catch (Exception e)
                {
                    throw e;                  
                }
            } while (!stringline.Contains("quit"));

            Console.WriteLine("A client has quit the connection.");
            //Thread will be quitted now
        }
    }
}
