/* NS: Server */
/* FN: Program.cs */
/* FUNCTION: Create server instance and call method "Server::ListenForClients" */

using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("This is the server-part of our little HAL-chatbot.");
            Console.WriteLine("Have fun ;)");
            Console.WriteLine("--------------------------------------------------");

            //handle only root exception, all others are handled by classes
            try
            {
                Server tcpserv = new Server();
                tcpserv.ListenForClients();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error -" + e.Message);
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
                Environment.Exit(1);
            }

            Console.ReadLine();
        }
    }
}
