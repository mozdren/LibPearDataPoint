using System;
using System.Threading;
using LibPearDataPoint;
using System.Collections.Generic;

namespace Sample3
{
    /// <summary>
    /// Program is testing subscriptions - imediate reaction to change of dataitem value (even distant values)
    /// </summary>
    class Program
    {
        static void Main()
        {
            // We are not creating endpoints, we are just want to subscribe to all items
            // and inform about changes
            Pear.Start();
            Thread.Sleep(3000); // wait for discovery of some distant datapoints

            Pear.Data.DataItemChanged += OnItemChanged;

            var subscribed = new List<string>();

            Console.WriteLine("Press Ctrl+C to stop ...");
            while (true)
            {
                var names = Pear.Data.GetNames(); // get distant datapoints
                foreach (var name in names)
                {
                    if (!subscribed.Contains(name))
                    {
                        if (Pear.Data.Subscribe(name))
                        {
                            subscribed.Add(name);
                            Console.WriteLine("Subscribed to {0}", name);
                        }
                    }
                }

                Thread.Sleep(1000);
            }
        }

        private static void OnItemChanged(DataItem changedDataItem)
        {
            Console.WriteLine("{0}: DataItem Changed Name: {1} Value: {2}", changedDataItem.LastUpdateTime, changedDataItem.Name, changedDataItem.Value);
        }
    }
}
