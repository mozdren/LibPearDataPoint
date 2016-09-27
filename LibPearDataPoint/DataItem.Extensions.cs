using System;

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
    /// </summary>
    internal static class DataItemExtensions
    {
        #region Getters

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
        internal static long? AsLong(this DataItem dataItem)
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

        /// <summary>
        /// Gets the value as an unsigned short.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns></returns>
        internal static DateTime? AsDateTime(this DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Value))
            {
                return null;
            }

            DateTime output;

            if (!DateTime.TryParse(dataItem.Value, out output))
            {
                return null;
            }

            return output;
        }

        #endregion

        #region Setters

        /// <summary>
        /// Sets int data to dataitem
        /// </summary>
        /// <param name="dataItem">data item</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, int value)
        {
            dataItem.Value = value.ToString();
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets float data to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, float value)
        {
            dataItem.Value = value.ToString();
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets double data to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, double value)
        {
            dataItem.Value = value.ToString();
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets long data to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, long value)
        {
            dataItem.Value = value.ToString();
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets bool data to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, bool value)
        {
            dataItem.Value = value.ToString();
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets byte data to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, byte value)
        {
            dataItem.Value = value.ToString();
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets short byte data to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, sbyte value)
        {
            dataItem.Value = value.ToString();
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets char data to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, char value)
        {
            dataItem.Value = value.ToString();
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets decimal data to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, decimal value)
        {
            dataItem.Value = value.ToString();
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets unsigned int data to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, uint value)
        {
            dataItem.Value = value.ToString();
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets unsigned long data to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, ulong value)
        {
            dataItem.Value = value.ToString();
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets short data to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, short value)
        {
            dataItem.Value = value.ToString();
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets unsigned short data to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, ushort value)
        {
            dataItem.Value = value.ToString();
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets string data to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, string value)
        {
            dataItem.Value = value;
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets DateTime to dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void Set(this DataItem dataItem, DateTime value)
        {
            dataItem.Value = value.ToString("O");
            dataItem.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Sets value if the data to be set are supported by the dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">value to be set</param>
        internal static void SetSupported(this DataItem dataItem, object value)
        {
            if (value == null)
            {
                throw new InvalidOperationException("value to be set cannot be null");
            }

            var valueType = value.GetType().ToString();
            switch (valueType)
            {
                case GlobalConstants.Types.Bool: dataItem.Set((bool)value); break;
                case GlobalConstants.Types.Byte: dataItem.Set((byte)value); break;
                case GlobalConstants.Types.Char: dataItem.Set((char)value); break;
                case GlobalConstants.Types.Decimal: dataItem.Set((decimal)value); break;
                case GlobalConstants.Types.Double: dataItem.Set((double)value); break;
                case GlobalConstants.Types.Int: dataItem.Set((int)value); break;
                case GlobalConstants.Types.Long: dataItem.Set((long)value); break;
                case GlobalConstants.Types.SByte: dataItem.Set((sbyte)value); break;
                case GlobalConstants.Types.Short: dataItem.Set(value.ToString()); break;
                case GlobalConstants.Types.Single: dataItem.Set((float)value); break;
                case GlobalConstants.Types.String: dataItem.Set(value.ToString()); break;
                case GlobalConstants.Types.UInt: dataItem.Set((uint)value); break;
                case GlobalConstants.Types.ULong: dataItem.Set((ulong)value); break;
                case GlobalConstants.Types.UShort: dataItem.Set((ushort)value); break;
                case GlobalConstants.Types.DateTime: dataItem.Set((DateTime)value); break;
                default: throw new InvalidCastException("The value is not of a supported type");
            }
        }

        #endregion
    }
}
