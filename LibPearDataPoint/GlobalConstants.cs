namespace LibPearDataPoint
{

    /// <summary>
    /// Global constants
    /// </summary>
    internal static class GlobalConstants
    {
        /// <summary>
        /// String constants for types
        /// </summary>
        internal static class Types
        {
            public const string Bool = "System.Boolean";
            public const string Byte = "System.Byte";
            public const string SByte = "System.SByte";
            public const string Char = "System.Char";
            public const string Decimal = "System.Decimal";
            public const string Double = "System.Double";
            public const string Single = "System.Single";
            public const string Int = "System.Int32";
            public const string UInt = "System.UInt32";
            public const string Long = "System.Int64";
            public const string ULong = "System.UInt64";
            public const string Object = "System.Object";
            public const string Short = "System.Int16";
            public const string UShort = "System.UInt16";
            public const string String = "System.String";
            public const string DateTime = "System.DateTime";

            public const string BoolArray = "System.Boolean[]";
            public const string ByteArray = "System.Byte[]";
            public const string SByteArray = "System.SByte[]";
            public const string CharArray = "System.Char[]";
            public const string DecimalArray = "System.Decimal[]";
            public const string DoubleArray = "System.Double[]";
            public const string SingleArray = "System.Single[]";
            public const string IntArray = "System.Int32[]";
            public const string UIntArray = "System.UInt32[]";
            public const string LongArray = "System.Int64[]";
            public const string ULongArray = "System.UInt64[]";
            public const string ObjectArray = "System.Object[]";
            public const string ShortArray = "System.Int16[]";
            public const string UShortArray = "System.UInt16[]";
            public const string StringArray = "System.String[]";
            public const string DateTimeArray = "System.DateTime[]";
        }

        /// <summary>
        /// Command constants used in messages
        /// </summary>
        internal static class Commands
        {
            /// <summary>
            /// Request for data
            /// </summary>
            internal const string Get = "GET";

            /// <summary>
            /// Request for updata of data
            /// </summary>
            internal const string Update = "UPDATE";

            /// <summary>
            /// Information about event that changed distant data
            /// </summary>
            internal const string ChangeEvent = "EVCHNG";

            /// <summary>
            /// Request for subscription
            /// </summary>
            internal const string Subscribe = "SUBS";

            /// <summary>
            /// Ping the service
            /// </summary>
            internal const string Ping = "PING";
        }

        /// <summary>
        /// Special Separator Strings
        /// </summary>
        internal static class Separators
        {
            /// <summary>
            /// Array Prefix
            /// </summary>
            internal const string ArrayPrefix = "{{[[";

            /// <summary>
            /// Array Items Separator
            /// </summary>
            internal const string ArraySeparator = "]]|[[";

            /// <summary>
            /// Array Postfix
            /// </summary>
            internal const string ArrayPostfix = "]]}}";
        }
    }
}
