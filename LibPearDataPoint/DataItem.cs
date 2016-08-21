using System;
using System.Runtime.Serialization;

namespace LibPearDataPoint
{
    /// <summary>
    /// DataItem which is supposed to be shared over Ethernet
    /// </summary>
    [DataContract]
    internal class DataItem : ICloneable
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

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DataItem" /> is reliable.
        /// It is true, if the result was aquired without problems.
        /// </summary>
        /// <value>
        ///   <c>true</c> if reliable; otherwise, <c>false</c>.
        /// </value>
        public bool IsReliable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is local.
        /// It returns false if the result was aquired from a remote DataPoint
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is local; otherwise, <c>false</c>.
        /// </value>
        public bool IsLocal { get; set; }

        /// <summary>
        /// This method returns an exact clone of itself
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new DataItem
            {
                IsLocal = IsLocal,
                IsReliable = IsReliable,
                LastUpdateTime = LastUpdateTime,
                Name = Name,
                Value = Value
            };
        }

        /// <summary>
        /// Returns a string value of the dataitem
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
