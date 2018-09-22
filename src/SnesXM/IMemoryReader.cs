// <copyright file="IMemoryReader.cs" company="Public Domain">
//     Copyright (c) 2018 Nelson Garcia. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace SnesXM
{
    public interface IMemoryReader
    {
        byte ReadByte(int address);

        ushort ReadWord(int address, WrapMode wrapMode);

        void WriteByte(int value, int address);

        void WriteWord(
            int value,
            int address,
            WrapMode wrapMode,
            WriteOrder writeOrder);
    }
}
