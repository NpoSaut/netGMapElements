using System;
using System.IO;
using GMapElements.Entities;
using GMapElements.Readers.Interfaces;

namespace GMapElements.Readers.Implementations
{
    public class HeaderReader : ReaderBase, IHeaderReader
    {
        public GHeader Read(Stream MapStream)
        {
            var data = ReadBytes(MapStream, 9);

            var postsCount     = BitConverter.ToInt16(data, 2);
            var number         = BitConverter.ToUInt16(data, 4);
            var conversionDate = new DateTime(data[6] | ((data[7] & 0xf0) << 4),
                                              data[7] & 0x0f,
                                              data[8]);

            return new GHeader(postsCount, number, conversionDate);
        }
    }
}
