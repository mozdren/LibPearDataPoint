using System;
using System.Threading;
using LibPearDataPoint;

namespace Sample4
{
    /// <summary>
    /// Program provides some data used by Example5
    /// </summary>
    class Program
    {
        static void Main()
        {
            // intitialize and discover the distant items
            Pear.Start();
            Thread.Sleep(1000);

            // Create data that should be created
            if (!Pear.Data.Create("Example4", "Data"))
            {
                Console.WriteLine("DataItem with Example4 cannot be created, it probably already exists!");
            }

            Console.WriteLine("Press Ctrl+C to stop ...");

            while (true) // serve forever
            {
                Thread.Sleep(1000);
            }
        }
    }
}
