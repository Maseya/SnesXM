// <copyright file="Registers.cs" company="Public Domain">
//     Copyright (c) 2018 Nelson Garcia. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

/* Refer to https://github.com/michielvoo/SNES/wiki/CPU.
 */

namespace SnesXM
{
    public class Registers
    {
        private byte _db;
        private Pair16 _p;
        private Pair16 _a;
        private Pair16 _d;
        private Pair16 _s;
        private Pair16 _x;
        private Pair16 _y;
        private Pair32 _pc;

        /// <summary>
        /// Gets or sets the data bank register.
        /// </summary>
        public int Db
        {
            get
            {
                return _db;
            }

            set
            {
                _db = (byte)value;
            }
        }

        /// <summary>
        /// Gets or sets the P register. This value is the integer
        /// representation of the <see cref="ProcessorStatus"/> enum.
        /// </summary>
        public int P
        {
            get
            {
                return _p.Word;
            }

            set
            {
                _p.Word = value;
            }
        }

        /// <summary>
        /// Gets or sets the low value of the <see cref="P"/> register.
        /// (Processor Status).
        /// </summary>
        public int PL
        {
            get
            {
                return _p.Low;
            }

            set
            {
                _p.Low = value;
            }
        }

        /// <summary>
        /// Gets or sets the high value of the <see cref="P"/> register.
        /// (Processor Status).
        /// </summary>
        public int PH
        {
            get
            {
                return _p.High;
            }

            set
            {
                _p.High = value;
            }
        }

        /// <summary>
        /// Gets or sets the processor status of the CPU. This is the
        /// enum representation of the <see cref="P"/> register.
        /// </summary>
        public ProcessorStatus ProcessorStatus
        {
            get
            {
                return (ProcessorStatus)_p.Word;
            }

            set
            {
                _p.Word = (int)value;
            }
        }

        public bool IsCarrySet

        {
            get
            {
                return HasProcessorState(ProcessorStatus.Carry);
            }

            set
            {
                SetProcessorState(ProcessorStatus.Carry, value);
            }
        }

        public bool IsZeroSet
        {
            get
            {
                return HasProcessorState(ProcessorStatus.Zero);
            }

            set
            {
                SetProcessorState(ProcessorStatus.Zero, value);
            }
        }

        public bool IsIrqMode
        {
            get
            {
                return HasProcessorState(ProcessorStatus.Irq);
            }

            set
            {
                SetProcessorState(ProcessorStatus.Irq, value);
            }
        }

        public bool IsDecimalMode
        {
            get
            {
                return HasProcessorState(ProcessorStatus.Decimal);
            }

            set
            {
                SetProcessorState(ProcessorStatus.Decimal, value);
            }
        }

        public bool IsIndexFlagSet
        {
            get
            {
                return HasProcessorState(ProcessorStatus.IndexFlag);
            }

            set
            {
                SetProcessorState(ProcessorStatus.IndexFlag, value);
            }
        }

        public bool IsMemoryFlagSet
        {
            get
            {
                return HasProcessorState(ProcessorStatus.MemoryFlag);
            }

            set
            {
                SetProcessorState(ProcessorStatus.MemoryFlag, value);
            }
        }

        public bool IsOverflowSet
        {
            get
            {
                return HasProcessorState(ProcessorStatus.Overflow);
            }

            set
            {
                SetProcessorState(ProcessorStatus.Overflow, value);
            }
        }

        public bool IsNegativeSet
        {
            get
            {
                return HasProcessorState(ProcessorStatus.Negative);
            }

            set
            {
                SetProcessorState(ProcessorStatus.Negative, value);
            }
        }

        public bool IsEmulationMode
        {
            get
            {
                return HasProcessorState(ProcessorStatus.Emulation);
            }

            set
            {
                SetProcessorState(ProcessorStatus.Emulation, value);
            }
        }

        /// <summary>
        /// Gets or sets the general purpose register.
        /// </summary>
        public int A
        {
            get
            {
                return _a.Word;
            }

            set
            {
                _a.Word = value;
            }
        }

        public int AL
        {
            get
            {
                return _a.Low;
            }

            set
            {
                _a.Low = value;
            }
        }

        public int AH
        {
            get
            {
                return _a.High;
            }

            set
            {
                _a.High = value;
            }
        }

        /// <summary>
        /// Gets or sets the Direct Page address for direct addressing
        /// mode.
        /// </summary>
        public int D
        {
            get
            {
                return _d.Word;
            }

            set
            {
                _d.Word = value;
            }
        }

        public int DL
        {
            get
            {
                return _d.Low;
            }

            set
            {
                _d.Low = value;
            }
        }

        public int DH
        {
            get
            {
                return _d.High;
            }

            set
            {
                _d.High = value;
            }
        }

        /// <summary>
        /// Gets or sets the stack pointer register.
        /// </summary>
        public int S
        {
            get
            {
                return _s.Word;
            }

            set
            {
                _s.Word = value;
            }
        }

        public int SL
        {
            get
            {
                return _s.Low;
            }

            set
            {
                _s.Low = value;
            }
        }

        public int SH
        {
            get
            {
                return _s.High;
            }

            set
            {
                _s.High = value;
            }
        }

        public int X
        {
            get
            {
                return _x.Word;
            }

            set
            {
                _x.Word = value;
            }
        }

        public int XL
        {
            get
            {
                return _x.Low;
            }

            set
            {
                _x.Low = value;
            }
        }

        public int XH
        {
            get
            {
                return _x.High;
            }

            set
            {
                _x.High = value;
            }
        }

        public int Y
        {
            get
            {
                return _y.Word;
            }

            set
            {
                _y.Word = value;
            }
        }

        public int YL
        {
            get
            {
                return _y.Low;
            }

            set
            {
                _y.Low = value;
            }
        }

        public int YH
        {
            get
            {
                return _y.High;
            }

            set
            {
                _y.High = value;
            }
        }

        /// <summary>
        /// Gets or sets the program counter.
        /// </summary>
        public int Pc
        {
            get
            {
                return _pc.Value;
            }

            set
            {
                _pc.Value = value;
            }
        }

        public int PcL
        {
            get
            {
                return _pc.Low;
            }

            set
            {
                _pc.Low = value;
            }
        }

        public int PcH
        {
            get
            {
                return _pc.High;
            }

            set
            {
                _pc.High = value;
            }
        }

        public int PcW
        {
            get
            {
                return _pc.Word;
            }

            set
            {
                _pc.Word = value;
            }
        }

        public int PcB
        {
            get
            {
                return _pc.Bank;
            }

            set
            {
                _pc.Bank = value;
            }
        }

        public void Reset()
        {
            SL = 0xFF;
            P = 0;
            A = 0;
            X = 0;
            Y = 0;
        }

        private bool HasProcessorState(ProcessorStatus flags)
        {
            return (ProcessorStatus & flags) == flags;
        }

        private void SetProcessorState(ProcessorStatus bit, bool value)
        {
            if (value)
            {
                ProcessorStatus |= bit;
            }
            else
            {
                ProcessorStatus &= ~bit;
            }
        }
    }
}
