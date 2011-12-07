/* NS: SocketCli */
/* FN: Program.cs */

using System;

namespace SocketCli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("Welcome to our HAL-client.");
            Console.WriteLine("Have fun!");
            Console.WriteLine("--------------------------");
            Console.WriteLine();
            Client tcpcli = new Client();
            tcpcli.Connect();

            Console.ReadLine();
        }
    }
}
