namespace LibPearDataPoint
{
    /// <summary>
    /// Extension methods over DataItem to provide easier access to values of a specific type.
    /// Just ask for the type e.g. AsInt to get integer. It is good practice to check if the
    /// value exists (if the result was correctly parsed using HasValue), because the return
    /// type is always nullable.
    /// 
    /// NOTE: This probably might be written in a better way (templates)... I have to
    ///       look into it later.
    ///       
    /// WARN: Unit Tests are Missing!!! Must be corrected as soon as possible!!!
    /// 
    /// </summary>
    internal static class DataItemExtensions
    {
        /// <summary>
        /// Gets the value as an int.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static int? AsInt(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            int output;

            if (!int.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>
        /// Gets the value as a float.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static float? AsFloat(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            float output;

            if (!float.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>
        /// Gets the value as a double.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static double? AsDouble(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            double output;

            if (!double.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>
        /// Gets the value as a long.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static double? AsLong(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            long output;

            if (!long.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>
        /// Gets the value as a bool.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static bool? AsBool(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            bool output;

            if (!bool.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>
        /// Gets the value as a byte.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static byte? AsByte(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            byte output;

            if (!byte.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>
        /// Gets the value as a short byte.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static sbyte? AsSByte(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            sbyte output;

            if (!sbyte.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>
        /// Gets the value as a character.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static char? AsChar(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            char output;

            if (!char.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>
        /// Gets the value as a decimal.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static decimal? AsDecimal(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            decimal output;

            if (!decimal.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>
        /// Gets the value as an unsigned int.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static uint? AsUInt(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            uint output;

            if (!uint.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>
        /// Gets the value as an unsigned long.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static ulong? AsULong(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            ulong output;

            if (!ulong.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>
        /// Gets the value as a short.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static short? AsShort(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            short output;

            if (!short.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>
        /// Gets the value as an unsigned short.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static ushort? AsUShort(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            ushort output;

            if (!ushort.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>
        /// Gets the value as a string.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static string AsString(this DataItem dataItem)
        {
            if (dataItem == null)
            {
                return null;
            }

            return dataItem.Value;
        }
    }
}
