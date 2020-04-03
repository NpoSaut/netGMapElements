using System;
using System.IO;
using GMapElements.Entities;
using GMapElements.Readers.Interfaces;

namespace GMapElements.Readers.Implementations
{
    public class TrackReader : ReaderBase, ITrackReader
    {
        private int _recordLength;
        private readonly IObjectReader _objectReader;

        public TrackReader(int recordLength, IObjectReader ObjectReader)
        {
            _objectReader = ObjectReader;
            _recordLength = recordLength;
        }

        public GTrack Read(Stream MapStream)
        {
            var data                 = ReadBytes(MapStream, _recordLength);
            var number               = data[0];
            var childrenCount        = data[1];
            var childrenStartAddress = SubInt(data, 2, 3);

            if (childrenStartAddress == 0xffffff)
                return null;

            var track = new GTrack(number);

            if (childrenCount > 0)
            {
                if (childrenStartAddress == 0)
                    throw new IndexOutOfRangeException();

                var previousPosition = MapStream.Position;
                MapStream.Seek(childrenStartAddress, SeekOrigin.Begin);
                for (var i = 0; i < childrenCount; i++)
                {
                    var obj = _objectReader.Read(MapStream);
                    track.Objects.Add(obj);
                }

                MapStream.Seek(previousPosition, SeekOrigin.Begin);
            }

            return track;
        }
    }
}
