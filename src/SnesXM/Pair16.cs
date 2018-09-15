// <copyright file="Pair16.cs" company="Public Domain">
//     Copyright (c) 2018 Nelson Garcia. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace SnesXM
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct Pair16 :
        IEquatable<Pair16>,
        IComparable<Pair16>,
        IComparable,
        IFormattable,
        IConvertible
    {
        public const int SizeOf = sizeof(ushort);

        [FieldOffset(0)]
        private byte _low;

        [FieldOffset(1)]
        private byte _high;

        [FieldOffset(0)]
        private ushort _word;

        public Pair16(int low, int high)
            : this()
        {
            Low = low;
            High = high;
        }

        public Pair16(int word)
            : this()
        {
            Word = word;
        }

        public int Low
        {
            get
            {
                return _low;
            }

            set
            {
                _low = (byte)value;
            }
        }

        public int High
        {
            get
            {
                return _high;
            }

            set
            {
                _high = (byte)value;
            }
        }

        public int Word
        {
            get
            {
                return _word;
            }

            set
            {
                _word = (ushort)value;
            }
        }

        private IConvertible Convertible
        {
            get
            {
                return Word;
            }
        }

        public static implicit operator Pair16(int value)
        {
            return new Pair16(value);
        }

        public static implicit operator int(Pair16 value)
        {
            return value.Word;
        }

        public static bool operator ==(Pair16 left, Pair16 right)
        {
            return left.Word == right.Word;
        }

        public static bool operator !=(Pair16 left, Pair16 right)
        {
            return left.Word != right.Word;
        }

        public static bool operator <(Pair16 left, Pair16 right)
        {
            return left.Word < right.Word;
        }

        public static bool operator >(Pair16 left, Pair16 right)
        {
            return left.Word > right.Word;
        }

        public static bool operator <=(Pair16 left, Pair16 right)
        {
            return left.Word <= right.Word;
        }

        public static bool operator >=(Pair16 left, Pair16 right)
        {
            return left.Word >= right.Word;
        }

        public int CompareTo(Pair16 other)
        {
            return Word.CompareTo(other.Word);
        }

        public int CompareTo(object obj)
        {
            if (obj is Pair16 value)
            {
                return CompareTo(value);
            }

            if (obj is IConvertible convertible)
            {
                var converted = convertible.ToInt32(
                    CultureInfo.CurrentCulture);

                return converted.CompareTo(Word);
            }

            return Word.CompareTo(obj);
        }

        public bool Equals(Pair16 other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (obj is Pair16 value)
            {
                return Equals(obj, value);
            }

            if (obj is IConvertible convertible)
            {
                var converted = convertible.ToInt32(null);
                return converted.Equals(Word);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Word.GetHashCode();
        }

        public override string ToString()
        {
            return Word.ToString("X4");
        }

        public string ToString(string format)
        {
            return Word.ToString(format);
        }

        public TypeCode GetTypeCode()
        {
            return Convertible.GetTypeCode();
        }

        object IConvertible.ToType(
            Type conversionType,
            IFormatProvider provider)
        {
            return Convertible.ToType(conversionType, provider);
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convertible.ToBoolean(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convertible.ToSByte(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convertible.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convertible.ToChar(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convertible.ToInt16(provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convertible.ToUInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convertible.ToInt32(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convertible.ToUInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convertible.ToInt64(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convertible.ToUInt64(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convertible.ToDateTime(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convertible.ToSingle(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convertible.ToDouble(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convertible.ToDecimal(provider);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return Word.ToString(formatProvider);
        }

        public string ToString(
            string format,
            IFormatProvider formatProvider)
        {
            return Word.ToString(format, formatProvider);
        }
    }
}
