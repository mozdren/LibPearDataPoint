using System;
using System.Linq;

namespace LibPearDataPoint
{
    /// <summary>
    /// Configuration Helper class for simple access of configuration properties stored in xml files
    /// </summary>
    public partial class Configuration
    {
        #region private fields

        /// <summary>
        /// The static instance of basic configuration provider
        /// </summary>
        private static Configuration _basicConfiguration = new Configuration("Configuration.xml");

        #endregion

        #region Basic Configuration Properties

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version
        {
            get { return _basicConfiguration.GetValue("version"); }
        }

        /// <summary>
        /// Gets the port number range.
        /// </summary>
        /// <value>
        /// The port number range.
        /// </value>
        public Tuple<int, int> PortNumberRange
        {
            get
            {
                // get the values from configuration
                var range = _basicConfiguration.GetValues("portrange").ToList();
                if (range.Count() != 2)
                {
                    return new Tuple<int, int>(0, 0);
                }

                // try to parse the item to integers
                int first, second;
                if (!int.TryParse(range[0], out first) || !int.TryParse(range[1], out second))
                {
                    return new Tuple<int, int>(0, 0);
                }

                // return the items in correct order
                return first < second ? new Tuple<int, int>(first, second) : new Tuple<int, int>(second, first);
            }
        }

        /// <summary>
        /// Gets the minimum port number.
        /// </summary>
        /// <value>
        /// The minimum port number.
        /// </value>
        public int MinPortNumber
        {
            get { return PortNumberRange.Item1; }
        }

        /// <summary>
        /// Gets the maximum port number.
        /// </summary>
        /// <value>
        /// The maximum port number.
        /// </value>
        public int MaxPortNumber
        {
            get { return PortNumberRange.Item2; }
        }

        /// <summary>
        /// Gets the broadcast port number.
        /// </summary>
        /// <value>
        /// The broadcast port number.
        /// </value>
        public int BroadcastPortNumber
        {
            get
            {
                var brcPortNumberCfgString = _basicConfiguration.GetValue("broadcastport");
                int broadcastPortNumber;
                if (!int.TryParse(brcPortNumberCfgString, out broadcastPortNumber))
                {
                    return 0;
                }

                return broadcastPortNumber;
            }
        }

        #endregion
    }
}
