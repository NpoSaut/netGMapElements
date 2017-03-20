using System;

namespace GMapElements
{
    [GLength(9)]
    public class GHeader : GElement
    {
        public int PostsCount { get; private set; }
        public ushort Number { get; private set; }
        public DateTime ConversionDate { get; private set; }

        protected override void FillWithBytes(byte[] Data)
        {
            PostsCount = BitConverter.ToInt16(Data, 2);
            Number = BitConverter.ToUInt16(Data, 4);
            var conversionYear = Data[6] | ((Data[7] & 0xf0) << 4);
            var conversionMonth = Data[7] & 0x0f;
            int conversionDay = Data[8];
            ConversionDate = new DateTime(conversionYear, conversionMonth, conversionDay);
        }
    }
}
