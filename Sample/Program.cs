using System;
using System.Diagnostics;
using System.Threading;
using LibPearDataPoint;

namespace Sample
{
    /// <summary>
    /// 
    /// This is a basic example of an application that uses LibPearDataPoint library. Its purpose is to show
    /// how easy is to share data between applications over ethernet without any need of networking implementation.
    ///
    /// In short, few simple steps are needed:
    ///
    /// 1) Create a dataitem to be peared with name or optionally even initial data using Pear.Data.Create method
    /// 2) Update data as needed using Pear.Data.Update method
    /// 3) Read data from local or distant datapoints using string indexation e.g. Pear.Data["name.of.dataitem"]
    /// 
    /// NOTE: Run this app multiple times at once ...
    /// 
    /// </summary>
    class Program
    {
        static void Main()
        {
            // Initial data preparation
            var counter = 0; // counter used for counting cycles
            var rand = new Random(); // random numbers generator
            var processId = Process.GetCurrentProcess().Id; // getting current process id for identification
            var localName = string.Format("process{0}.RandomValue", processId); // creating unique system name

            // Data item creation
            Pear.Data.Create(localName, rand.Next());

            // Endless Loop
            Console.WriteLine("Press Ctrl+C to stop ...");
            while (true)
            {
                Thread.Sleep(1000); // sleep for one second before discovering new data
                var names = Pear.Data.GetNames(); // getting local and discovered dataitem names
                foreach (var name in names)
                {
                    int? value = Pear.Data[name];

                    if (value.HasValue)
                    {
                        Console.WriteLine("{0} -> {1}", name, value); // value was received
                    }
                    else
                    {
                        Console.WriteLine("{0} -> NO VALUE", name); // value not received or in a wrong format
                    }
                }

                // only every 10th cycle will update the local value
                if (counter++ == 10)
                {
                    counter = 0; // reset counter
                    Pear.Data.Update(localName, rand.Next());
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
