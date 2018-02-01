using System;
using System.IO;
using System.Linq;
using Geographics;
using GMapElements.Entities;
using GMapElements.Readers.Interfaces;

namespace GMapElements.Readers.Implementations
{
    public class PostReader : ReaderBase, IPostReader
    {
        private const int HeaderLength = 15;

        private readonly ITrackReader _trackReader;

        public PostReader(ITrackReader TrackReader)
        {
            _trackReader = TrackReader;
        }

        public GPost Read(Stream MapStream, int SectionId)
        {
            var postData = ReadBytes(MapStream, HeaderLength);

            var ordinate  = BitConverter.ToInt32(postData.Take(3).Concat(new byte[1]).ToArray(), 0);
            var flags     = postData[3];
            var direction = DecodeDirection((flags >> 7) & 0x01);
            var position  = (PositionInSection)(flags & 0x03);
            var crossing = (flags & (1 << 6)) > 0;
            var point     = new EarthPoint(BitConverter.ToInt32(postData, 4) * 10e-9 * 180 / Math.PI,
                                           BitConverter.ToInt32(postData, 8) * 10e-9 * 180 / Math.PI);
            var childrenStartAddress = SubInt(postData, 12, 3);

            var post = new GPost(ordinate, point, direction, position, SectionId, crossing);

            var previousPosition = MapStream.Position;
            MapStream.Seek(childrenStartAddress, SeekOrigin.Begin);
            var tracksCount = MapStream.ReadByte();
            for (var i = 0; i < tracksCount; i++)
            {
                var track = _trackReader.Read(MapStream);
                if (track != null)
                    post.Tracks.Add(track);
            }

            MapStream.Seek(previousPosition, SeekOrigin.Begin);

            return post;
        }

        private OrdinateDirection DecodeDirection(int DirectionCode)
        {
            switch (DirectionCode)
            {
                case 0:
                    return OrdinateDirection.Increasing;
                case 1:
                    return OrdinateDirection.Decreasing;
            }

            throw new ArgumentException("Недопустимое значение кода направления", nameof(DirectionCode));
        }
    }
}
