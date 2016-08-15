using System;
using System.Runtime.Serialization;

namespace LibPearDataPoint
{
    /// <summary>
    /// DataItem which is supposed to be shared over Ethernet
    /// </summary>
    [DataContract]
    internal class DataItem
    {
        /// <summary>
        /// Gets or sets the name. The name should be unique over Ethernet
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last update time.
        /// </summary>
        /// <value>
        /// The last update time.
        /// </value>
        [DataMember]
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [DataMember]
        public string Value { get; set; }
    }
}
