/* NS: Server */
/* FN: Server.cs */

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class Server
    {
        private TcpListener tcpListener;
        private Thread clientThread;

        /* Constructor */
        public Server()
        {
            //create listener - second param = port
            this.tcpListener = new TcpListener(IPAddress.Any, 8080);
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
                    this.tcpListener.Start();
                }
                catch (SocketException e)   //if socket is invalid
                {
                    throw e;
                }
                Socket sock = this.tcpListener.AcceptSocket();

                //ClientComm-Class is responsible for receiving sentences
                ClientComm ccomm = new ClientComm(sock);

                //create a thread to handle communication
                //with connected client
                this.clientThread = new Thread(new ThreadStart(ccomm.WelcomeClient));
                try
                {
                    clientThread.Start();
                }
                catch (Exception e)
                {
                    throw e; //throw to main
                }
            }
        }
    }
}
