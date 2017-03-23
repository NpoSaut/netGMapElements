using System;
using System.IO;
using System.Linq;

namespace GMapElements
{
    public abstract class GElement
    {
        private int DataLength
        {
            get { return LengthOf(GetType()); }
        }

        public static T FromBytes<T>(byte[] Data) where T : GElement, new()
        {
            var res = new T();
            res.FillWithBytes(Data);
            return res;
        }

        protected abstract void FillWithBytes(byte[] Data);

        protected virtual void FillFromStream(Stream DataStream)
        {
            var buff = new byte[DataLength];
            DataStream.Read(buff, 0, buff.Length);
            FillWithBytes(buff);
        }

        internal static int LengthOf<T>() where T : GElement { return LengthOf(typeof(T)); }

        private static int LengthOf(Type T) { return T.GetCustomAttributes(typeof(GLengthAttribute), true).OfType<GLengthAttribute>().First().Length; }

        internal static T FromStream<T>(Stream str) where T : GElement, new()
        {
            var res = new T();
            res.FillFromStream(str);
            return res;
        }

        protected static byte[] ByteSubseq(byte[] data, int startIndex, int getLength, int echoLength)
        {
            var res = new byte[echoLength];
            Buffer.BlockCopy(data, startIndex, res, 0, getLength);
            return res;
        }

        protected static int SubInt(byte[] data, int startIndex, int length) { return BitConverter.ToInt32(ByteSubseq(data, startIndex, length, 4), 0); }
    }
}
