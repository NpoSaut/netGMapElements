using System;
using System.IO;
using GMapElements.Entities;
using GMapElements.Readers.Interfaces;

namespace GMapElements.Readers.Implementations
{
    public class ExtendedHeaderReader : ReaderBase, IHeaderReader
    {
        public GHeader Read(Stream MapStream)
        {
            ReadBytes(MapStream, 9); // FF-ки, которые добавили для совместимости со старым форматом

            var data = ReadBytes(MapStream, 14);

            var postsCount = BitConverter.ToInt16(data, 2);
            var number     = BitConverter.ToUInt16(data, 4);
            var conversionDate = new DateTime(data[6] | ((data[7] & 0xf0) << 4),
                                              data[7] & 0x0f,
                                              data[8]);

            var postRecordLength   = data[11];
            var trackRecordLength  = data[12];
            var objectRecordLength = data[13];

            return new GHeader(postsCount, number, conversionDate,
                postRecordLength, 
                trackRecordLength, 
                objectRecordLength);
        }
    }
}