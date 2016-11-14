using System;
using System.Collections.Generic;
using System.Linq;

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

        #region Array Getters

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static int[] AsIntArray(this DataItem dataItem)
        {
            if (dataItem == null || 
                string.IsNullOrWhiteSpace(dataItem.Value) || 
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<int>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                int output;

                if (!int.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }
            
            return ret.ToArray();
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static float[] AsFloatArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<float>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                float output;

                if (!float.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static double[] AsDoubleArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<double>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                double output;

                if (!double.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static long[] AsLongArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<long>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                long output;

                if (!long.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static bool[] AsBoolArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<bool>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                bool output;

                if (!bool.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static byte[] AsByteArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<byte>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                byte output;

                if (!byte.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static sbyte[] AsSByteArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<sbyte>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                sbyte output;

                if (!sbyte.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static char[] AsCharArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<char>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                char output;

                if (!char.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static decimal[] AsDecimalArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<decimal>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                decimal output;

                if (!decimal.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static uint[] AsUIntArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<uint>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                uint output;

                if (!uint.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static ulong[] AsULongArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<ulong>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                ulong output;

                if (!ulong.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static short[] AsShortArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<short>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                short output;

                if (!short.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static ushort[] AsUShortArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<ushort>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                ushort output;

                if (!ushort.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static string[] AsStringArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return new string[] { };
            }

            return stringToParse.Split(GlobalConstants.Separators.ArraySeparator);
        }

        /// <summary>
        /// Returns dataitem value as an array.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        /// <returns>array</returns>
        internal static DateTime[] AsDateTimeArray(this DataItem dataItem)
        {
            if (dataItem == null ||
                string.IsNullOrWhiteSpace(dataItem.Value) ||
                !dataItem.Value.StartsWith(GlobalConstants.Separators.ArrayPrefix) ||
                !dataItem.Value.EndsWith(GlobalConstants.Separators.ArrayPostfix))
            {
                return null;
            }

            var ret = new List<DateTime>();

            var stringToParse = dataItem.Value.Replace(GlobalConstants.Separators.ArrayPrefix, "").Replace(GlobalConstants.Separators.ArrayPostfix, "");
            if (string.IsNullOrWhiteSpace(stringToParse))
            {
                return ret.ToArray();
            }

            var stringArray = stringToParse.Split(GlobalConstants.Separators.ArraySeparator);

            foreach (var stringItem in stringArray)
            {
                DateTime output;

                if (!DateTime.TryParse(stringItem, out output))
                {
                    return null;
                }

                ret.Add(output);
            }

            return ret.ToArray();
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

            if (valueType.EndsWith("[]"))
            {
                dataItem.SetSupportedArray((object[])value);
                return;
            }

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

        #region Array Setters

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, bool[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, byte[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, char[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, decimal[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, double[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, int[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, long[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, sbyte[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, short[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, float[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, string[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, uint[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, ulong[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, ushort[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array);
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets a value which represents an array from the input
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="array">array to be set</param>
        internal static void Set(this DataItem dataItem, DateTime[] array)
        {
            var data = string.Join(GlobalConstants.Separators.ArraySeparator, array.Select(i => i.ToString("O")));
            dataItem.Value = string.Format("{0}{1}{2}", GlobalConstants.Separators.ArrayPrefix, data, GlobalConstants.Separators.ArrayPostfix);
        }

        /// <summary>
        /// Sets enumeration if the data to be set are supported by the dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">values to be set</param>
        internal static void SetSupported(this DataItem dataItem, IEnumerable<object> values)
        {
            dataItem.SetSupported(values.ToArray());
        }

        /// <summary>
        /// Sets array if the data to be set are supported by the dataitem
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <param name="value">values to be set</param>
        internal static void SetSupportedArray(this DataItem dataItem, object[] values)
        {
            if (values == null)
            {
                throw new InvalidOperationException("array to be set cannot be null");
            }

            var valueType = values.GetType().ToString();
            switch (valueType)
            {
                case GlobalConstants.Types.BoolArray:
                    dataItem.Set(values.Select(v => (bool)v).ToArray());
                    break;
                case GlobalConstants.Types.ByteArray:
                    dataItem.Set(values.Select(v => (byte)v).ToArray());
                    break;
                case GlobalConstants.Types.CharArray:
                    dataItem.Set(values.Select(v => (char)v).ToArray());
                    break;
                case GlobalConstants.Types.DecimalArray:
                    dataItem.Set(values.Select(v => (Decimal)v).ToArray());
                    break;
                case GlobalConstants.Types.DoubleArray:
                    dataItem.Set(values.Select(v => (Double)v).ToArray());
                    break;
                case GlobalConstants.Types.IntArray:
                    dataItem.Set(values.Select(v => (int)v).ToArray());
                    break;
                case GlobalConstants.Types.LongArray:
                    dataItem.Set(values.Select(v => (long)v).ToArray());
                    break;
                case GlobalConstants.Types.SByteArray:
                    dataItem.Set(values.Select(v => (sbyte)v).ToArray());
                    break;
                case GlobalConstants.Types.ShortArray:
                    dataItem.Set(values.Select(v => (short)v).ToArray());
                    break;
                case GlobalConstants.Types.SingleArray:
                    dataItem.Set(values.Select(v => (float)v).ToArray());
                    break;
                case GlobalConstants.Types.StringArray:
                    dataItem.Set(values.Select(v => v.ToString()).ToArray());
                    break;
                case GlobalConstants.Types.UIntArray:
                    dataItem.Set(values.Select(v => (uint)v).ToArray());
                    break;
                case GlobalConstants.Types.ULongArray:
                    dataItem.Set(values.Select(v => (ulong)v).ToArray());
                    break;
                case GlobalConstants.Types.UShortArray:
                    dataItem.Set(values.Select(v => (ushort)v).ToArray());
                    break;
                case GlobalConstants.Types.DateTimeArray:
                    dataItem.Set(values.Select(v => (DateTime)v).ToArray());
                    break;
                default:
                    throw new InvalidCastException("The type of array values is not supported");
            }
        }

        #endregion
    }
}
