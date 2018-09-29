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
        public RegisterMath(IMemoryReader memoryReader)
        {
            Registers = new Registers();
            MemoryReader = memoryReader ??
                throw new ArgumentNullException(nameof(memoryReader));
        }

        public Registers Registers
        {
            get;
        }

        public IMemoryReader MemoryReader
        {
            get;
        }

        public byte AL
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

        public ushort A
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

        public byte XL
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

        public ushort X
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

        public byte YL
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

        public ushort Y
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

        public bool IsCarrySet
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

        public bool IsDecimalMode
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

        public bool IsOverflowSet
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

        public bool IsNegativeSet
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

        public bool IsZeroSet
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

        public int CarryBit
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

        public void Adc(ushort value)
        {
            int result;
            if (IsDecimalMode)
            {
                var carryWithA = BcdAdd(A, CarryBit);
                result = BcdAdd(carryWithA, value);
            }
            else
            {
                result = A + CarryBit + value;
            }

            IsCarrySet = result >= 0x10000;
            var overflowBit = ~(A ^ value) & (value ^ result) & 0x8000;

            IsOverflowSet = overflowBit != 0;
            A = (ushort)result;
        }

        public void Adc(byte value)
        {
            int result;
            if (IsDecimalMode)
            {
                result = BcdAdd(A, value);
                result = BcdAdd(result, CarryBit);
            }
            else
            {
                result = A + value + CarryBit;
            }

            IsCarrySet = result >= 0x100;
            var overflowBit = ~(AL ^ value) & (value ^ result) & 0x80;

            IsOverflowSet = overflowBit != 0;
            AL = (byte)result;
        }

        public void And(ushort value)
        {
            A &= value;
        }

        public void And(byte value)
        {
            AL &= value;
        }

        public void Asl(int address, WrapMode wrapMode)
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

        public void Asl(int address)
        {
            var value = ReadByte(address);
            var result = value << 1;

            IsCarrySet = result > 0xFF;
            WriteByteAddOneCycleSetZN(result, address);
        }

        public void Bit(ushort value)
        {
            IsOverflowSet = (value & 0x4000) != 0;
            IsNegativeSet = (value & 0x8000) != 0;
            IsZeroSet = (value & A) == 0;
        }

        public void Bit(byte value)
        {
            IsOverflowSet = (value & 0x40) != 0;
            IsNegativeSet = (value & 0x80) != 0;
            IsZeroSet = (value & AL) == 0;
        }

        public void Cmp(ushort value)
        {
            var result = A - value;
            IsCarrySet = result >= 0;
            SetZN((ushort)result);
        }

        public void Cmp(byte value)
        {
            var result = AL - value;
            IsCarrySet = result >= 0;
            SetZN((byte)result);
        }

        public void Cpx(ushort value)
        {
            var result = X - value;
            IsCarrySet = result >= 0;
            SetZN((ushort)result);
        }

        public void Cpx(byte value)
        {
            var result = XL - value;
            IsCarrySet = result >= 0;
            SetZN((byte)result);
        }

        public void Cpy(ushort value)
        {
            var result = Y - value;
            IsCarrySet = result >= 0;
            SetZN((ushort)result);
        }

        public void Cpy(byte value)
        {
            var result = YL - value;
            IsCarrySet = result >= 0;
            SetZN((byte)result);
        }

        public void Dec(int address, WrapMode wrapMode)
        {
            var value = ReadWord(address, wrapMode);
            var result = value - 1;

            WriteWordAddOneCycleSetZN(
                result,
                address,
                wrapMode,
                WriteOrder.Write10);
        }

        public void Dec(int address)
        {
            var value = ReadByte(address);
            var result = value - 1;

            WriteByteAddOneCycleSetZN(result, address);
        }

        public void Eor(ushort value)
        {
            A ^= value;
        }

        public void Eor(byte value)
        {
            AL ^= value;
        }

        public void Inc(int address, WrapMode wrapMode)
        {
            var value = ReadWord(address, wrapMode);
            var result = value + 1;

            WriteWordAddOneCycleSetZN(
                result,
                address,
                wrapMode,
                WriteOrder.Write10);
        }

        public void Inc(int address)
        {
            var value = ReadByte(address);
            var result = value + 1;

            WriteByteAddOneCycleSetZN(result, address);
        }

        public void Lda(ushort value)
        {
            A = value;
        }

        public void Lda(byte value)
        {
            AL = value;
        }

        public void Ldx(ushort value)
        {
            X = value;
        }

        public void Ldx(byte value)
        {
            XL = value;
        }

        public void Ldy(ushort value)
        {
            Y = value;
        }

        public void Ldy(byte value)
        {
            YL = value;
        }

        public void Lsr(int address, WrapMode wrapMode)
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

        public void Lsr(int address)
        {
            var value = ReadByte(address);
            var result = value >> 1;

            IsCarrySet = (value & 1) != 0;
            WriteByteAddOneCycleSetZN(result, address);
        }

        public void Ora(ushort value)
        {
            A |= value;
        }

        public void Ora(byte value)
        {
            AL |= value;
        }

        public void Rol(int address, WrapMode wrapMode)
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

        public void Rol(int address)
        {
            var value = ReadByte(address);
            var result = (value << 1) | CarryBit;

            IsCarrySet = result > 0xFF;
            WriteByteAddOneCycleSetZN(result, address);
        }

        public void Ror(int address, WrapMode wrapMode)
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

        public void Ror(int address)
        {
            var value = ReadByte(address);
            var result = (value >> 1) | (CarryBit << 7);

            IsCarrySet = (value & 1) != 0;
            WriteByteAddOneCycleSetZN(result, address);
        }

        public void Sbc(ushort value)
        {
            int result;
            if (IsDecimalMode)
            {
                var carryWithA = BcdAdd(A, CarryBit ^ 1);
                result = BcdSubtract(carryWithA, value);
            }
            else
            {
                result = A + (CarryBit ^ 1) - value;
            }

            var overflowBit = (A ^ value) & (A ^ result) & 0x8000;

            IsOverflowSet = overflowBit != 0;
            A = (ushort)result;
        }

        public void Sbc(byte value)
        {
            int result;
            if (IsDecimalMode)
            {
                var carryWithA = BcdAdd(A, CarryBit ^ 1);
                result = BcdSubtract(carryWithA, value);
            }
            else
            {
                result = A + (CarryBit ^ 1) - value;
            }

            var overflowBit = (A ^ value) & (A ^ result) & 0x80;

            IsOverflowSet = overflowBit != 0;
            A = (ushort)result;
        }

        public void Sta(int address, WrapMode wrapMode)
        {
            WriteWord(A, address, wrapMode);
        }

        public void Sta(int address)
        {
            WriteByte(AL, address);
        }

        public void Stx(int address, WrapMode wrapMode)
        {
            WriteWord(X, address, wrapMode);
        }

        public void Stx(int address)
        {
            WriteByte(XL, address);
        }

        public void Sty(int address, WrapMode wrapMode)
        {
            WriteWord(Y, address, wrapMode);
        }

        public void Sty(int address)
        {
            WriteByte(YL, address);
        }

        public void Stz(int address, WrapMode wrapMode)
        {
            WriteWord(0, address, wrapMode);
        }

        public void Stz(int address)
        {
            WriteByte(0, address);
        }

        public void Tsb(int address, WrapMode wrapMode)
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

        public void Tsb(int address)
        {
            var value = ReadByte(address);
            var result = value | AL;

            IsZeroSet = (value & AL) == 0;
            WriteByteAddOneCycle(result, address);
        }

        public void Trb(int address, WrapMode wrapMode)
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

        public void Trb(int address)
        {
            var value = ReadByte(address);
            var result = value & ~AL;

            IsZeroSet = (value & AL) == 0;
            WriteByteAddOneCycle(result, address);
        }

        private static bool IsValidBcd(int a)
        {
            var t1 = a + 0x06666666;
            var t2 = t1 ^ a;
            var t3 = t2 & 0x11111110;
            return t3 == 0;
        }

        private static int BcdAdd(int left, int right)
        {
            // If we assume that left is a valid BCD value, then this
            // addition should produce no carries.
            var t1 = left + 0x06666666;

            // Digits in this sum are correct for any sums that produced
            // a carry. Digits that didn't produce a carry will have an
            // excess value of 6.
            var t2 = t1 + right;

            // t2 and t3 will differ wherever there was a carry.
            var t3 = t1 ^ right;

            // Records where all carries took place in the sum.
            var t4 = t2 ^ t3;

            // Holds all positions where a carry didn't take place.
            var t5 = ~t4 & 0x11111110;

            // Each digit that didn't have a carry will be 6. Each digit
            // that did have a carry will be 0.
            var t6 = (t5 >> 2) | (t5 >> 3);

            // Remove the excess of 6 from each digit that didn't have a
            // carry in its addition.
            var result = t2 - t6;
            return result;
        }

        private static int BcdTensComplement(int value)
        {
            var t1 = -value;
            var t2 = t1 - 1;
            var t3 = t2 ^ 1;
            var t4 = t1 ^ t3;
            var t5 = ~t4 & 0x11111110;
            var t6 = (t5 >> 2) | (t5 >> 3);
            return t1 - t6;
        }

        private static int BcdSubtract(int left, int right)
        {
            var tensComplementRight = BcdTensComplement(right);
            var result = BcdAdd(left, tensComplementRight);
            return right;
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
