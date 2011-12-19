﻿/* NS: Server */
/* FN: Server.cs */
/* FUNCTION: Wait for new connections and start a new thread for each. */

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class Server
    {
        /* PUBLIC VARS */
        public TcpListener TcpListener { get; set; }
        public Thread ClientThread { get; set; }
        public ClientComm CComm { get; set; }
        public Socket Sock { get; set; }

        /* Constructor */
        public Server()
        {
            //create listener - second param = port
            try
            {
                TcpListener = new TcpListener(IPAddress.Any, 8080);
            }
            catch (Exception) { throw; }
        }

        /* Wait for connections - threading function */
        public void ListenForClients()
        {
            while(true)
            {
                Console.WriteLine("Waiting for a new connection...");
                //blocks until a client connects to the server
                try
                {
                    TcpListener.Start();
                }
                catch (SocketException)   //if socket is invalid
                {
                    throw;
                }
                //create new socket
                Sock = TcpListener.AcceptSocket();

                //ClientComm-class is responsible for receiving sentences
                CComm = new ClientComm(Sock);

                /* create a thread to handle communication with connected client */
                ClientThread = new Thread(new ThreadStart(CComm.WelcomeClient));
                try
                {
                    ClientThread.Start();
                }
                catch (Exception)
                {
                    throw; //throw to main
                }
            }
        }
    }
}
