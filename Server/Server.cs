/* NS: Server */
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
        public TcpListener TcpListener_ { get; set; }
        public Thread ClientThread_ { get; set; }
        public ClientComm CComm_ { get; set; }
        public Socket Sock_ { get; set; }

        /* Constructor */
        public Server()
        {
            //create listener - second param = port
            try
            {
                TcpListener_ = new TcpListener(IPAddress.Any, 8080);
            }
            catch (Exception e)
            { throw e; }
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
                    TcpListener_.Start();
                }
                catch (SocketException e)   //if socket is invalid
                {
                    throw e;
                }
                //create new socket
                Sock_ = TcpListener_.AcceptSocket();

                //ClientComm-class is responsible for receiving sentences
                CComm_ = new ClientComm(Sock_);

                /* create a thread to handle communication with connected client */
                ClientThread_ = new Thread(new ThreadStart(CComm_.WelcomeClient));
                try
                {
                    ClientThread_.Start();
                }
                catch (Exception e)
                {
                    throw e; //throw to main
                }
            }
        }
    }
}
