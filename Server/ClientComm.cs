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
        private Socket sock;

        public ClientComm(Socket c_sock)
        {
            this.sock = c_sock;
        }

        /* Entry-class for each connected thread */
        public void WelcomeClient()
        {
            Console.WriteLine("A client connected from {0}", sock.RemoteEndPoint);
            NetworkStream stream = new NetworkStream(this.sock);
            StreamReader sr = new StreamReader(stream);

            //receive from client in loop
            string stringline;
            do
            {
                try
                {
                    stringline = sr.ReadLine();
                    /* SEND STRING TO TEXT PARSER */
                    TextParser tp = new TextParser(stringline);
                    tp.SplitSentence();
                    /* UNTIL NOW, THE SERVER ONLY PRINTS WHAT HE HAS RECEIVED */
                    //Output
                    Console.WriteLine(stringline);
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
