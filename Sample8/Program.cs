using System;
using System.Threading;
using LibPearDataPoint;

namespace Sample4
{
    /// <summary>
    /// Program waits until data from Example7 are available and then shows them
    /// HINT: Run this app first, then run the Example7
    /// </summary>
    class Program
    {
        static void Main()
        {
            // Wait for data to be available
            Console.WriteLine("Waiting for data...");
            Peer.WaitFor("Example7");

            // Get the data
            var dataItem = Peer.Get("Example7");
            if (dataItem == null)
            {
                Console.WriteLine("ERROR: Could not retrieve data!");
                return;
            }

            string[] array = dataItem;

            if (array == null)
            {
                Console.WriteLine("Could not read array");
            }

            // Write the data to console
            Console.WriteLine("Data found: {0}[], length: {1} - {2}", dataItem.Name, array.Length, dataItem.Value);
            foreach (var item in array)
            {
                Console.WriteLine("Item: {0}", item);
            }

            // Keep the app running
            Console.WriteLine("Press Ctrl + C to stop...");
            while (true) // serve forever
            {
                Thread.Sleep(1000);
            }
        }
    }
}
