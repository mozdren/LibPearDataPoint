using System;
using System.Threading;
using LibPearDataPoint;

namespace Sample4
{
    /// <summary>
    /// Program waits until data from Example4 are available and then shows them
    /// HINT: Run this app first, then run the Example4
    /// </summary>
    class Program
    {
        static void Main()
        {
            // Wait for data to be available
            Console.WriteLine("Waiting for data...");
            Pear.Data.WaitFor("Example4");

            // Get the data
            var dataItem = Pear.Data["Example4"];
            if (dataItem == null)
            {
                Console.WriteLine("ERROR: Could not retrieve data!");
                return;
            }

            // Write the data to console
            Console.WriteLine("Data found: {0} = {1}", dataItem.Name, dataItem.Value);

            // Keep the app running
            Console.WriteLine("Press Ctrl + C to stop...");
            while (true) // serve forever
            {
                Thread.Sleep(1000);
            }
        }
    }
}
