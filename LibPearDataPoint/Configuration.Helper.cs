using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private static Configuration _basicConfiguration = new Configuration("Configuration.Basic.xml");

        #endregion

        #region Basic Configuration Properties

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public static string Version
        {
            get { return _basicConfiguration.GetValue("version"); }
        }

        #endregion
    }
}
