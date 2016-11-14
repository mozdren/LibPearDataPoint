using System;
using System.Threading;
using LibPearDataPoint;

namespace Sample7
{
    /// <summary>
    /// Program provides some data used by Example8
    /// </summary>
    class Program
    {
        static void Main()
        {
            // intitialize and discover the distant items
            Peer.Start();
            Thread.Sleep(1000);

            // Create data that should be created
            if (!Peer.Create("Example7", new string[] { "first", "second", "third" }))
            {
                Console.WriteLine("DataItem with Example7 cannot be created, it probably already exists!");
            }

            Console.WriteLine("Press Ctrl+C to stop ...");

            while (true) // serve forever
            {
                Thread.Sleep(1000);
            }
        }
    }
}
