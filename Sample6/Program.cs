using LibPearDataPoint;
using System;
using System.Threading;

namespace Sample6
{
    /// <summary>
    /// This sample does not create any dataitems. It waits until it finds distant dataitems.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // wait for 10 seconds to discover distant dataitems
            Console.WriteLine("Waiting for discovery of distant dataitems ...");
            if (Pear.Data.WaitForDistant(timeoutSeconds: 10))
            {
                // Distant dataitems were found in the specified limit of 10 seconds
                Console.WriteLine("Distant dataitems discovered");
            }
            else
            {
                // No distant datatapoints were found in the specified limit of 10 seconds
                Console.WriteLine("No distant dataitems discovered");
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
