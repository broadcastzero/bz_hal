﻿/* NS: Server */
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
        public Socket Sock { get; set; }
        public NetworkStream Stream { get; set; }
        public StreamReader Sr { get; set; }
        public TextParser Tp { get; set; }

        /* CONSTRUCTOR */
        public ClientComm(Socket sock)
        {
            Sock = sock;
            Tp = new TextParser();
            try
            {
                Stream = new NetworkStream(Sock);
                Sr = new StreamReader(Stream);
            }
            catch (Exception) { throw; }
        }

        /* Entry-class for each connected thread */
        public void WelcomeClient()
        {
            try
            {
                Console.WriteLine("A client connected from {0}", Sock.RemoteEndPoint);
            }
            catch (Exception)
            { Sr.Close(); throw; }

            //receive from client in loop
            string stringline = "";
            do
            {
                try
                {
                    stringline = Sr.ReadLine();

                    //does client want to quit? -> do not parse text, continue after loop
                    string raw = stringline.ToLower();
                    if (raw.Contains("quit")) { break; }

                    //send string to text parser
                    if (stringline != null)
                    {
                        //Tp_.SplitSentence(stringline);
                        //output
                        Console.WriteLine(stringline);
                    }
                }
                //is thrown, if sentence doesn't end with '.' or '?'
                catch (InvalidSentenceException e)  
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine(e.Message);
                    break;  //quit
                }
            } while (true);

            //Close open connections
            Sr.Close(); //also calls Sock_.Close and Stream_.Close()
            Console.WriteLine("---------------------------------");
            Console.WriteLine("A client has quit the connection.");
            Console.WriteLine("---------------------------------");
            //Thread will quit now
        }
    }
}
