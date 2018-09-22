// <copyright file="ProcessorStatus.cs" company="Public Domain">
//     Copyright (c) 2018 Nelson Garcia. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace SnesXM
{
    using System;

    [Flags]
    public enum ProcessorStatus
    {
        None = 0,
        Carry = 1 << 0,
        Zero = 1 << 1,
        Irq = 1 << 2,
        Decimal = 1 << 3,
        IndexFlag = 1 << 4,
        MemoryFlag = 1 << 5,
        Overflow = 1 << 6,
        Negative = 1 << 7,
        Emulation = 1 << 8,
    }
}
