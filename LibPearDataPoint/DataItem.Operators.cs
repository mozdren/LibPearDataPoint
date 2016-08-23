using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPearDataPoint
{
    /// <summary>
    /// This partial implementation of the DataItem class is taking care about the conversions and oprators in general.
    /// </summary>
    public partial class DataItem
    {
        /// <summary>
        /// Implicit Conversion from dataitem to int
        /// </summary>
        /// <param name="dataItem">data item</param>
        /// <returns>integer value</returns>
        public static implicit operator int(DataItem dataItem)
        {
            var result = dataItem.AsInt();

            if (result.HasValue) 
            {
                return result.Value;
            }
            
            throw new InvalidCastException("Cannot implicitly convert to int");
        }

        /// <summary>
        /// Implicit conversion to unsigned int
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>unsigned int value</returns>
        public static implicit operator uint(DataItem dataItem)
        {
            var result = dataItem.AsUInt();

            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidCastException("Cannot implicitly convert to unsigned int");
        }

        /// <summary>
        /// Implicit conversion to long
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>long value</returns>
        public static implicit operator long(DataItem dataItem)
        {
            var result = dataItem.AsLong();

            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidCastException("Cannot implicitly convert to long");
        }

        /// <summary>
        /// Implicit conversion to unsigned long
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>unsigned long value</returns>
        public static implicit operator ulong(DataItem dataItem)
        {
            var result = dataItem.AsULong();

            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidCastException("Cannot implicitly convert to unsigned long");
        }

        /// <summary>
        /// Implicit conversion to byte
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>byte value</returns>
        public static implicit operator byte(DataItem dataItem)
        {
            var result = dataItem.AsByte();

            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidCastException("Cannot implicitly convert to int");
        }

        /// <summary>
        /// Implicit conversion to sbyte
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>signed byte value</returns>
        public static implicit operator sbyte(DataItem dataItem)
        {
            var result = dataItem.AsSByte();

            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidCastException("Cannot implicitly convert to signed byte");
        }

        /// <summary>
        /// Implicit conversion to char
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>char value</returns>
        public static implicit operator char(DataItem dataItem)
        {
            var result = dataItem.AsChar();

            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidCastException("Cannot implicitly convert to char");
        }

        /// <summary>
        /// Implicit conversion do decimal
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>decimal value</returns>
        public static implicit operator decimal(DataItem dataItem)
        {
            var result = dataItem.AsDecimal();

            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidCastException("Cannot implicitly convert to decimal");
        }

        /// <summary>
        /// Implicit conversion to double
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>double value</returns>
        public static implicit operator double(DataItem dataItem)
        {
            var result = dataItem.AsDouble();

            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidCastException("Cannot implicitly convert to double");
        }

        /// <summary>
        /// Implicit conversion to float
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>float value</returns>
        public static implicit operator float(DataItem dataItem)
        {
            var result = dataItem.AsFloat();

            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidCastException("Cannot implicitly convert to float");
        }

        /// <summary>
        /// Implicit conversion to boolean
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>boolean value</returns>
        public static implicit operator bool(DataItem dataItem)
        {
            var result = dataItem.AsBool();

            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidCastException("Cannot implicitly convert to bool");
        }

        /// <summary>
        /// Implicit conversion to string
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public static implicit operator string(DataItem dataItem)
        {
            return dataItem.ToString();
        }
    }
}
