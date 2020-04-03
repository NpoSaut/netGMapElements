using System;
using System.IO;
using GMapElements.Entities;
using GMapElements.Readers.Interfaces;

namespace GMapElements.Readers.Implementations
{
    public class HeaderReader : ReaderBase, IHeaderReader
    {
        private readonly int _postRecordLength;
        private readonly int _trackRecordLength;
        private readonly int _objectRecordLength;

        public HeaderReader(int PostRecordLength, int TrackRecordLength, int ObjectRecordLength)
        {
            _postRecordLength   = PostRecordLength;
            _trackRecordLength  = TrackRecordLength;
            _objectRecordLength = ObjectRecordLength;
        }

        public GHeader Read(Stream MapStream)
        {
            var data = ReadBytes(MapStream, 9);

            var postsCount = BitConverter.ToInt16(data, 2);
            var number     = BitConverter.ToUInt16(data, 4);
            var conversionDate = new DateTime(data[6] | ((data[7] & 0xf0) << 4),
                                              data[7] & 0x0f,
                                              data[8]);

            return new GHeader(postsCount, number, conversionDate,
                               _postRecordLength,
                               _trackRecordLength,
                               _objectRecordLength);
        }
    }
}