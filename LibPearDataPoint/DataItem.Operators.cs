using System;

namespace LibPearDataPoint
{
    /// <summary>
    /// This partial implementation of the DataItem class is taking care about the conversions and oprators in general.
    /// </summary>
    public partial class DataItem
    {
        #region Basic Casting Operators

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
        /// Implicit conversion to short
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>short value</returns>
        public static implicit operator short(DataItem dataItem)
        {
            var result = dataItem.AsShort();

            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidCastException("Cannot implicitly convert to short");
        }

        /// <summary>
        /// Implicit conversion to unsigned short
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>unsigned short value</returns>
        public static implicit operator ushort(DataItem dataItem)
        {
            var result = dataItem.AsUShort();

            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidCastException("Cannot implicitly convert to unsigned short");
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
        /// <returns>string value</returns>
        public static implicit operator string(DataItem dataItem)
        {
            return dataItem.ToString();
        }

        /// <summary>
        /// Implicit conversion to DateTime
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>datetime value</returns>
        public static implicit operator DateTime(DataItem dataItem)
        {
            return DateTime.Parse(dataItem.Value);
        }

        #endregion

        #region Nullable Casting Operators

        /// <summary>
        /// Implicit Conversion from dataitem to int
        /// </summary>
        /// <param name="dataItem">data item</param>
        /// <returns>integer value</returns>
        public static implicit operator int?(DataItem dataItem)
        {
            return dataItem.AsInt();
        }

        /// <summary>
        /// Implicit conversion to unsigned int
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>unsigned int value</returns>
        public static implicit operator uint?(DataItem dataItem)
        {
            return dataItem.AsUInt();
        }

        /// <summary>
        /// Implicit conversion to short
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>short value</returns>
        public static implicit operator short?(DataItem dataItem)
        {
            return dataItem.AsShort();
        }

        /// <summary>
        /// Implicit conversion to unsigned short
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>unsigned short value</returns>
        public static implicit operator ushort?(DataItem dataItem)
        {
            return dataItem.AsUShort();
        }

        /// <summary>
        /// Implicit conversion to long
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>long value</returns>
        public static implicit operator long?(DataItem dataItem)
        {
            return dataItem.AsLong();
        }

        /// <summary>
        /// Implicit conversion to unsigned long
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>unsigned long value</returns>
        public static implicit operator ulong?(DataItem dataItem)
        {
            return dataItem.AsULong();
        }

        /// <summary>
        /// Implicit conversion to byte
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>byte value</returns>
        public static implicit operator byte?(DataItem dataItem)
        {
            return dataItem.AsByte();
        }

        /// <summary>
        /// Implicit conversion to sbyte
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>signed byte value</returns>
        public static implicit operator sbyte?(DataItem dataItem)
        {
            return dataItem.AsSByte();
        }

        /// <summary>
        /// Implicit conversion to char
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>char value</returns>
        public static implicit operator char?(DataItem dataItem)
        {
            return dataItem.AsChar();
        }

        /// <summary>
        /// Implicit conversion do decimal
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>decimal value</returns>
        public static implicit operator decimal?(DataItem dataItem)
        {
            return dataItem.AsDecimal();
        }

        /// <summary>
        /// Implicit conversion to double
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>double value</returns>
        public static implicit operator double?(DataItem dataItem)
        {
            return dataItem.AsDouble();
        }

        /// <summary>
        /// Implicit conversion to float
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>float value</returns>
        public static implicit operator float?(DataItem dataItem)
        {
            return dataItem.AsFloat();
        }

        /// <summary>
        /// Implicit conversion to boolean
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>boolean value</returns>
        public static implicit operator bool?(DataItem dataItem)
        {
            return dataItem.AsBool();
        }

        /// <summary>
        /// Implicit conversion to DateTime
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>datetime value</returns>
        public static implicit operator DateTime?(DataItem dataItem)
        {
            return dataItem.AsDateTime();
        }

        #endregion

        #region Implicit operators for arrays

        /// <summary>
        /// Implicit Conversion from dataitem to int array
        /// </summary>
        /// <param name="dataItem">data item</param>
        /// <returns>integer array</returns>
        public static implicit operator int[] (DataItem dataItem)
        {
            return dataItem.AsIntArray();
        }

        /// <summary>
        /// Implicit conversion to unsigned int array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>unsigned int array</returns>
        public static implicit operator uint[] (DataItem dataItem)
        {
            return dataItem.AsUIntArray();
        }

        /// <summary>
        /// Implicit conversion to short array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>short array</returns>
        public static implicit operator short[] (DataItem dataItem)
        {
            return dataItem.AsShortArray();
        }

        /// <summary>
        /// Implicit conversion to unsigned short array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>unsigned short array</returns>
        public static implicit operator ushort[] (DataItem dataItem)
        {
            return dataItem.AsUShortArray();
        }

        /// <summary>
        /// Implicit conversion to long array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>long array</returns>
        public static implicit operator long[] (DataItem dataItem)
        {
            return dataItem.AsLongArray();
        }

        /// <summary>
        /// Implicit conversion to unsigned long array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>unsigned long array</returns>
        public static implicit operator ulong[] (DataItem dataItem)
        {
            return dataItem.AsULongArray();
        }

        /// <summary>
        /// Implicit conversion to byte array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>byte array</returns>
        public static implicit operator byte[] (DataItem dataItem)
        {
            return dataItem.AsByteArray();
        }

        /// <summary>
        /// Implicit conversion to sbyte array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>signed byte array</returns>
        public static implicit operator sbyte[] (DataItem dataItem)
        {
            return dataItem.AsSByteArray();
        }

        /// <summary>
        /// Implicit conversion to char array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>char array</returns>
        public static implicit operator char[] (DataItem dataItem)
        {
            return dataItem.AsCharArray();
        }

        /// <summary>
        /// Implicit conversion do decimal array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>decimal array</returns>
        public static implicit operator decimal[] (DataItem dataItem)
        {
            return dataItem.AsDecimalArray();
        }

        /// <summary>
        /// Implicit conversion to double array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>double array</returns>
        public static implicit operator double[] (DataItem dataItem)
        {
            return dataItem.AsDoubleArray();
        }

        /// <summary>
        /// Implicit conversion to float array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>float array</returns>
        public static implicit operator float[] (DataItem dataItem)
        {
            return dataItem.AsFloatArray();
        }

        /// <summary>
        /// Implicit conversion to boolean array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>boolean array</returns>
        public static implicit operator bool[] (DataItem dataItem)
        {
            return dataItem.AsBoolArray();
        }

        /// <summary>
        /// Implicit conversion to DateTime array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>datetime array</returns>
        public static implicit operator DateTime[] (DataItem dataItem)
        {
            return dataItem.AsDateTimeArray();
        }

        /// <summary>
        /// Implicit conversion to string array
        /// </summary>
        /// <param name="dataItem">dataitem</param>
        /// <returns>string array</returns>
        public static implicit operator string[] (DataItem dataItem)
        {
            return dataItem.AsStringArray();
        }

        #endregion
    }
}
