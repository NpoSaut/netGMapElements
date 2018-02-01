﻿using System.IO;
using GMapElements.Entities;
using GMapElements.Readers.Interfaces;
using JetBrains.Annotations;

namespace GMapElements.Readers.Implementations
{
    public class TrackReader : ReaderBase, ITrackReader
    {
        private const int ElementLength = 5;
        private readonly IObjectReader _objectReader;

        public TrackReader(IObjectReader ObjectReader)
        {
            _objectReader = ObjectReader;
        }

        public GTrack Read(Stream MapStream)
        {
            var data                 = ReadBytes(MapStream, ElementLength);
            var number               = data[0];
            var childrenCount        = data[1];
            var childrenStartAddress = SubInt(data, 2, 3);

            if (childrenStartAddress == 0xffffff)
                return null;

            var track = new GTrack(number);

            var previousPosition = MapStream.Position;
            MapStream.Seek(childrenStartAddress, SeekOrigin.Begin);
            for (var i = 0; i < childrenCount; i++)
            {
                var obj = _objectReader.Read(MapStream);
                track.Objects.Add(obj);
            }

            MapStream.Seek(previousPosition, SeekOrigin.Begin);

            return track;
        }
    }
}