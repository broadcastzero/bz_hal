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
        /* PUBLIC VARS */
        public Socket Sock_ { get; set; }
        public NetworkStream Stream_ { get; set; }
        public StreamReader Sr_ { get; set; }
        public TextParser Tp_ { get; set; }

        /* CONSTRUCTOR */
        public ClientComm(Socket sock)
        {
            Sock_ = sock;
            Tp_ = new TextParser();
            try
            {
                Stream_ = new NetworkStream(Sock_);
                Sr_ = new StreamReader(Stream_);
            }
            catch (Exception) { throw; }
        }

        /* Entry-class for each connected thread */
        public void WelcomeClient()
        {
            try
            {
                Console.WriteLine("A client connected from {0}", Sock_.RemoteEndPoint);
            }
            catch (Exception)
            { Sr_.Close(); throw; }

            //receive from client in loop
            string stringline = "";
            do
            {
                try
                {
                    stringline = Sr_.ReadLine();
                    /* SEND STRING TO TEXT PARSER */
                    if (stringline != null)
                    {
                        Tp_.SplitSentence(stringline);
                        /* OUTPUT */
                        Console.WriteLine(stringline);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine(e.Message);
                    break;
                }
            } while (!stringline.Contains("quit"));

            //Close open connections
            Sr_.Close(); //also calls Sock_.Close and Stream_.Close()
            Console.WriteLine("---------------------------------");
            Console.WriteLine("A client has quit the connection.");
            Console.WriteLine("---------------------------------");
            //Thread will quit now
        }
    }
}
