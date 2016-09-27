using System;
using System.Threading;
using LibPearDataPoint;

namespace Sample2
{
    /// <summary>
    /// 
    /// This sample program should work together with first sample program. In this case no local point is created,
    /// but all distant endpoints are set to 0 every 20 seconds.
    /// 
    /// </summary>
    class Program
    {
        static void Main()
        {
            // We are not creating endpoints and we are just want to read and update
            // distant data, and herefore, we call Start at the beginning of the application
            // to initialize Pear
            Pear.Start();
            Thread.Sleep(3000); // wait for discovery of distant datapoints

            Console.WriteLine("Press Ctrl+C to stop ...");
            while (true)
            {
                var names = Pear.Data.GetNames(); // get distant datapoints
                foreach (var name in names)
                {
                    Pear.Data.Update(name, 0); // set distant datapoint value to zero
                }

                Thread.Sleep(20000);
            }
        }
    }
}
