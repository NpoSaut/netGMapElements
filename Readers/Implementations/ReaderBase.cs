using System;
using System.IO;

namespace GMapElements.Readers.Implementations
{
    public abstract class ReaderBase
    {
        protected byte[] ReadBytes(Stream FromStream, int Length)
        {
            var data = new byte[Length];
            var readen = FromStream.Read(data, 0, Length);
            if (readen != Length)
                throw new IndexOutOfRangeException("Чё-то не вышло прочитать столько, сколько надо");
            return data;
        }

        protected byte[] ByteSubseq(byte[] data, int startIndex, int getLength, int echoLength)
        {
            var res = new byte[echoLength];
            Buffer.BlockCopy(data, startIndex, res, 0, getLength);
            return res;
        }

        protected int SubInt(byte[] data, int startIndex, int length)
        {
            return BitConverter.ToInt32(ByteSubseq(data, startIndex, length, 4), 0);
        }
    }
}
