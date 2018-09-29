// <copyright file="RegisterMath.cs" company="Public Domain">
//     Copyright (c) 2018 Nelson Garcia. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace SnesXM
{
    using System;
    using System.ComponentModel;
    using static Cycles;

    public class RegisterMath
    {
        private Registers Registers
        {
            get;
        }

        private IMemoryReader MemoryReader
        {
            get;
        }

        private byte AL
        {
            get
            {
                return (byte)Registers.AL;
            }

            set
            {
                Registers.AL = value;
                SetZN(value);
            }
        }

        private ushort A
        {
            get
            {
                return (ushort)Registers.A;
            }

            set
            {
                Registers.A = value;
                SetZN(value);
            }
        }

        private byte XL
        {
            get
            {
                return (byte)Registers.XL;
            }

            set
            {
                Registers.XL = value;
                SetZN(value);
            }
        }

        private ushort X
        {
            get
            {
                return (ushort)Registers.X;
            }

            set
            {
                Registers.X = value;
                SetZN(value);
            }
        }

        private byte YL
        {
            get
            {
                return (byte)Registers.YL;
            }

            set
            {
                Registers.YL = value;
                SetZN(value);
            }
        }

        private ushort Y
        {
            get
            {
                return (ushort)Registers.Y;
            }

            set
            {
                Registers.Y = value;
                SetZN(value);
            }
        }

        private bool IsCarrySet
        {
            get
            {
                return Registers.IsCarrySet;
            }

            set
            {
                Registers.IsCarrySet = value;
            }
        }

        private bool IsDecimalMode
        {
            get
            {
                return Registers.IsDecimalMode;
            }

            set
            {
                Registers.IsDecimalMode = value;
            }
        }

        private bool IsOverflowSet
        {
            get
            {
                return Registers.IsOverflowSet;
            }

            set
            {
                Registers.IsOverflowSet = value;
            }
        }

        private bool IsNegativeSet
        {
            get
            {
                return Registers.IsNegativeSet;
            }

            set
            {
                Registers.IsNegativeSet = value;
            }
        }

        private bool IsZeroSet
        {
            get
            {
                return Registers.IsZeroSet;
            }

            set
            {
                Registers.IsZeroSet = value;
            }
        }

        private int CarryBit
        {
            get
            {
                return IsCarrySet ? 1 : 0;
            }
        }

        private byte OpenBus
        {
            get;
            set;
        }

        public void AddCycles(int cycles)
        {
            throw new NotImplementedException();
        }

        private void Adc(ushort value)
        {
            var result = IsDecimalMode ? AdcDecimal() : AdcHex();
            var overflowBit = ~(A ^ value) & (value ^ result) & 0x8000;

            IsOverflowSet = overflowBit != 0;
            A = result;

            ushort AdcHex()
            {
                var sum = A + value + CarryBit;
                IsCarrySet = sum >= 0x10000;
                return (ushort)sum;
            }

            ushort AdcDecimal()
            {
                (var l1, var l2, var l3, var l4) = GetDigits(A);
                (var r1, var r2, var r3, var r4) = GetDigits(value);

                var s1 = l1 + r1 + CarryBit;
                var s2 = l2 + r2;
                var s3 = l3 + r3;
                var s4 = l4 + r4;

                if (s1 > 0x0009)
                {
                    s1 -= 0x000A;
                    s1 &= 0x000F;
                    s2 += 0x0010;
                }

                if (s2 > 0x0090)
                {
                    s2 -= 0x00A0;
                    s2 &= 0x00F0;
                    s3 += 0x0100;
                }

                if (s3 > 0x0900)
                {
                    s3 -= 0x0A00;
                    s3 &= 0x0F00;
                    s4 += 0x1000;
                }

                IsCarrySet = s4 > 0x9000;
                if (IsCarrySet)
                {
                    s4 -= 0xA000;
                    s4 &= 0xF000;
                }

                var sum = s4 | s3 | s2 | s1;
                return (ushort)sum;

                (int n1, int n2, int n3, int n4) GetDigits(int number)
                {
                    return (
                        number & 0x000F,
                        number & 0x00F0,
                        number & 0x0F00,
                        number & 0xF000);
                }
            }
        }

        private void Adc(byte value)
        {
            var result = IsDecimalMode ? AdcDecimal() : AdcHex();

            var overflowBit = ~(AL ^ value) & (value ^ result) & 0x80;

            IsOverflowSet = overflowBit != 0;
            AL = result;

            byte AdcHex()
            {
                var sum = AL + value + CarryBit;
                IsCarrySet = sum >= 0x100;
                return (byte)sum;
            }

            byte AdcDecimal()
            {
                (var l1, var l2) = GetDigits(AL);
                (var r1, var r2) = GetDigits(value);

                var s1 = l1 + r1 + CarryBit;
                var s2 = l2 + r2;

                if (s1 > 0x09)
                {
                    s1 -= 0x0A;
                    s1 &= 0x0F;
                    s2 += 0x10;
                }

                IsCarrySet = s2 > 0x90;
                if (IsCarrySet)
                {
                    s2 -= 0xA0;
                    s2 &= 0xF0;
                    IsCarrySet = true;
                }

                var sum = s2 | s1;
                return (byte)sum;

                (int n1, int n2) GetDigits(int number)
                {
                    return (number & 0x0F, number & 0xF0);
                }
            }
        }

        private void And(ushort value)
        {
            A &= value;
        }

        private void And(byte value)
        {
            AL &= value;
        }

        private void Asl(int address, WrapMode wrapMode)
        {
            var value = ReadWord(address, wrapMode);
            var result = value << 1;

            IsCarrySet = result > 0xFFFF;
            WriteWordAddOneCycleSetZN(
                result,
                address,
                wrapMode,
                WriteOrder.Write10);
        }

        private void Asl(int address)
        {
            var value = ReadByte(address);
            var result = value << 1;

            IsCarrySet = result > 0xFF;
            WriteByteAddOneCycleSetZN(result, address);
        }

        private void Bit(ushort value)
        {
            IsOverflowSet = (value & 0x4000) != 0;
            IsNegativeSet = (value & 0x8000) != 0;
            IsZeroSet = (value & A) == 0;
        }

        private void Bit(byte value)
        {
            IsOverflowSet = (value & 0x40) != 0;
            IsNegativeSet = (value & 0x80) != 0;
            IsZeroSet = (value & AL) == 0;
        }

        private void Cmp(ushort value)
        {
            var result = A - value;
            IsCarrySet = result >= 0;
            SetZN((ushort)result);
        }

        private void Cmp(byte value)
        {
            var result = AL - value;
            IsCarrySet = result >= 0;
            SetZN((byte)result);
        }

        private void Cpx(ushort value)
        {
            var result = X - value;
            IsCarrySet = result >= 0;
            SetZN((ushort)result);
        }

        private void Cpx(byte value)
        {
            var result = XL - value;
            IsCarrySet = result >= 0;
            SetZN((byte)result);
        }

        private void Cpy(ushort value)
        {
            var result = Y - value;
            IsCarrySet = result >= 0;
            SetZN((ushort)result);
        }

        private void Cpy(byte value)
        {
            var result = YL - value;
            IsCarrySet = result >= 0;
            SetZN((byte)result);
        }

        private void Dec(int address, WrapMode wrapMode)
        {
            var value = ReadWord(address, wrapMode);
            var result = value - 1;

            WriteWordAddOneCycleSetZN(
                result,
                address,
                wrapMode,
                WriteOrder.Write10);
        }

        private void Dec(int address)
        {
            var value = ReadByte(address);
            var result = value - 1;

            WriteByteAddOneCycleSetZN(result, address);
        }

        private void Eor(ushort value)
        {
            A ^= value;
        }

        private void Eor(byte value)
        {
            AL ^= value;
        }

        private void Inc(int address, WrapMode wrapMode)
        {
            var value = ReadWord(address, wrapMode);
            var result = value + 1;

            WriteWordAddOneCycleSetZN(
                result,
                address,
                wrapMode,
                WriteOrder.Write10);
        }

        private void Inc(int address)
        {
            var value = ReadByte(address);
            var result = value + 1;

            WriteByteAddOneCycleSetZN(result, address);
        }

        private void Lda(ushort value)
        {
            A = value;
        }

        private void Lda(byte value)
        {
            AL = value;
        }

        private void Ldx(ushort value)
        {
            X = value;
        }

        private void Ldx(byte value)
        {
            XL = value;
        }

        private void Ldy(ushort value)
        {
            Y = value;
        }

        private void Ldy(byte value)
        {
            YL = value;
        }

        private void Lsr(int address, WrapMode wrapMode)
        {
            var value = ReadWord(address, wrapMode);
            var result = value >> 1;

            IsCarrySet = (value & 1) != 0;
            WriteWordAddOneCycleSetZN(
                result,
                address,
                wrapMode,
                WriteOrder.Write10);
        }

        private void Lsr(int address)
        {
            var value = ReadByte(address);
            var result = value >> 1;

            IsCarrySet = (value & 1) != 0;
            WriteByteAddOneCycleSetZN(result, address);
        }

        private void Ora(ushort value)
        {
            A |= value;
        }

        private void Ora(byte value)
        {
            AL |= value;
        }

        private void Rol(int address, WrapMode wrapMode)
        {
            var value = ReadWord(address, wrapMode);
            var result = (value << 1) | CarryBit;

            IsCarrySet = result > 0xFFFF;
            WriteWordAddOneCycleSetZN(
                result,
                address,
                wrapMode,
                WriteOrder.Write10);
        }

        private void Rol(int address)
        {
            var value = ReadByte(address);
            var result = (value << 1) | CarryBit;

            IsCarrySet = result > 0xFF;
            WriteByteAddOneCycleSetZN(result, address);
        }

        private void Ror(int address, WrapMode wrapMode)
        {
            var value = ReadWord(address, wrapMode);
            var result = (value >> 1) | (CarryBit << 0x0F);

            IsCarrySet = (value & 1) != 0;
            WriteWordAddOneCycleSetZN(
                result,
                address,
                wrapMode,
                WriteOrder.Write10);
        }

        private void Ror(int address)
        {
            var value = ReadByte(address);
            var result = (value >> 1) | (CarryBit << 7);

            IsCarrySet = (value & 1) != 0;
            WriteByteAddOneCycleSetZN(result, address);
        }

        private void Sbc(ushort value)
        {
            var result = IsDecimalMode ? SbcDecimal() : SbcHex();
            var overflowBit = (A ^ value) & (A ^ result) & 0x8000;

            IsOverflowSet = overflowBit != 0;
            A = result;

            ushort SbcHex()
            {
                var diff = A - value + CarryBit - 1;
                IsCarrySet = diff >= 0;
                return (ushort)diff;
            }

            ushort SbcDecimal()
            {
                (var l1, var l2, var l3, var l4) = GetDigits(A);
                (var r1, var r2, var r3, var r4) = GetDigits(value);

                var s1 = l1 - r1 + CarryBit - 1;
                var s2 = l2 - r2;
                var s3 = l3 - r3;
                var s4 = l4 - r4;

                if (s1 > 0x000F)
                {
                    s1 += 0x000A;
                    s1 &= 0x000F;
                    s2 -= 0x0010;
                }

                if (s2 > 0x00F0)
                {
                    s2 += 0x00A0;
                    s2 &= 0x00F0;
                    s3 -= 0x0100;
                }

                if (s3 > 0x0F00)
                {
                    s3 += 0x0A00;
                    s3 &= 0x0F00;
                    s4 -= 0x1000;
                }

                IsCarrySet = s4 <= 0xF000;
                if (!IsCarrySet)
                {
                    s4 += 0xA000;
                    s4 &= 0xF000;
                }

                var diff = s4 | s3 | s2 | s1;
                return (ushort)diff;

                (int n1, int n2, int n3, int n4) GetDigits(int number)
                {
                    return (
                        number & 0x000F,
                        number & 0x00F0,
                        number & 0x0F00,
                        number & 0xF000);
                }
            }
        }

        private void Sbc(byte value)
        {
            var result = IsDecimalMode ? AdcDecimal() : AdcHex();
            var overflowBit = (AL ^ value) & (AL ^ result) & 0x80;

            IsOverflowSet = overflowBit != 0;
            AL = result;

            byte AdcHex()
            {
                var diff = AL - value + CarryBit - 1;
                IsCarrySet = diff >= 0;
                return (byte)diff;
            }

            byte AdcDecimal()
            {
                (var l1, var l2) = GetDigits(AL);
                (var r1, var r2) = GetDigits(value);

                var s1 = l1 - r1 + CarryBit - 1;
                var s2 = l2 - r2;

                if (s1 > 0x0F)
                {
                    s1 += 0x0A;
                    s1 &= 0x0F;
                    s2 -= 0x10;
                }

                IsCarrySet = s2 <= 0xF0;
                if (!IsCarrySet)
                {
                    s2 += 0xA0;
                    s2 &= 0xF0;
                }

                var diff = s2 | s1;
                return (byte)diff;

                (int n1, int n2) GetDigits(int number)
                {
                    return (number & 0x0F, number & 0xF0);
                }
            }
        }

        private void Sta(int address, WrapMode wrapMode)
        {
            WriteWord(A, address, wrapMode);
        }

        private void Sta(int address)
        {
            WriteByte(AL, address);
        }

        private void Stx(int address, WrapMode wrapMode)
        {
            WriteWord(X, address, wrapMode);
        }

        private void Stx(int address)
        {
            WriteByte(XL, address);
        }

        private void Sty(int address, WrapMode wrapMode)
        {
            WriteWord(Y, address, wrapMode);
        }

        private void Sty(int address)
        {
            WriteByte(YL, address);
        }

        private void Stz(int address, WrapMode wrapMode)
        {
            WriteWord(0, address, wrapMode);
        }

        private void Stz(int address)
        {
            WriteByte(0, address);
        }

        private void Tsb(int address, WrapMode wrapMode)
        {
            var value = ReadWord(address, wrapMode);
            var result = value | A;

            IsZeroSet = (value & A) == 0;
            WriteWordAddOneCycle(
                result,
                address,
                wrapMode,
                WriteOrder.Write10);
        }

        private void Tsb(int address)
        {
            var value = ReadByte(address);
            var result = value | AL;

            IsZeroSet = (value & AL) == 0;
            WriteByteAddOneCycle(result, address);
        }

        private void Trb(int address, WrapMode wrapMode)
        {
            var value = ReadWord(address, wrapMode);
            var result = value & ~A;

            IsZeroSet = (value & A) == 0;
            WriteWordAddOneCycle(
                result,
                address,
                wrapMode,
                WriteOrder.Write10);
        }

        private void Trb(int address)
        {
            var value = ReadByte(address);
            var result = value & ~AL;

            IsZeroSet = (value & AL) == 0;
            WriteByteAddOneCycle(result, address);
        }

        private void SetZN(ushort value)
        {
            IsZeroSet = value == 0;
            IsNegativeSet = (value & 0x8000) != 0;
        }

        private void SetZN(byte value)
        {
            IsZeroSet = value == 0;
            IsNegativeSet = (value & 0x80) != 0;
        }

        private byte ReadByte(int address)
        {
            return MemoryReader.ReadByte(address);
        }

        private ushort ReadWord(int address, WrapMode wrapMode)
        {
            return MemoryReader.ReadWord(address, wrapMode);
        }

        private void WriteByte(int value, int address)
        {
            MemoryReader.WriteByte(value, address);
            OpenBus = (byte)value;
        }

        private void WriteWord(
            int value,
            int address,
            WrapMode wrapMode,
            WriteOrder writeOrder = WriteOrder.Write01)
        {
            MemoryReader.WriteWord(
                value,
                address,
                wrapMode,
                writeOrder);

            switch (writeOrder)
            {
                case WriteOrder.Write10:
                    OpenBus = (byte)value;
                    break;

                case WriteOrder.Write01:
                    OpenBus = (byte)(value >> 8);
                    break;

                default:
                    throw new InvalidEnumArgumentException(
                        nameof(writeOrder),
                        (int)writeOrder,
                        typeof(WriteOrder));
            }
        }

        private void WriteByteAddOneCycle(int value, int address)
        {
            AddCycles(OneCycle);
            WriteByte(value, address);
        }

        private void WriteWordAddOneCycle(
            int value,
            int address,
            WrapMode wrapMode,
            WriteOrder writeOrder = WriteOrder.Write01)
        {
            AddCycles(OneCycle);
            WriteWord(value, address, wrapMode, writeOrder);
        }

        private void WriteByteAddOneCycleSetZN(int value, int address)
        {
            WriteByteAddOneCycle(value, address);
            SetZN((byte)value);
        }

        private void WriteWordAddOneCycleSetZN(
            int value,
            int address,
            WrapMode wrapMode,
            WriteOrder writeOrder = WriteOrder.Write01)
        {
            WriteWordAddOneCycle(value, address, wrapMode, writeOrder);
            SetZN((ushort)value);
        }
    }
}
