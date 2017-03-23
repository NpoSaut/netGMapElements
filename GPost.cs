using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Geographics;

namespace GMapElements
{
    public enum OrdinateDirection
    {
        Increasing = 1,
        Decreasing = -1
    }

    public enum PositionInSection
    {
        Middle = 0,
        Start = 1,
        End = 2
    }

    [GLength(15)]
    public class GPost : GContainer<GTrack>
    {
        /// <summary>Линейная ордината</summary>
        public int Ordinate { get; private set; }

        /// <summary>Широта и долгота точки</summary>
        public EarthPoint Point { get; private set; }

        public OrdinateDirection Direction { get; private set; }

        public IList<GTrack> Tracks
        {
            get { return Children.Where(t => t.IsValid).ToList(); }
        }

        public PositionInSection Position { get; private set; }
        public int SectionId { get; set; }

        protected override void FillWithBytes(byte[] Data)
        {
            Ordinate = BitConverter.ToInt32(Data.Take(3).Concat(new byte[1]).ToArray(), 0);

            var flags = Data[3];

            Direction = DecodeDirection((flags >> 7) & 0x01);
            Position = (PositionInSection)(flags & 0x03);

            Point = new EarthPoint(BitConverter.ToInt32(Data, 4) * 10e-9 * 180 / Math.PI, BitConverter.ToInt32(Data, 8) * 10e-9 * 180 / Math.PI);

            ChildrenStartAddress = SubInt(Data, 12, 3) + 1;
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
            throw new ArgumentException("Недопустимое значение кода направления", "DirectionCode");
        }

        protected override int GetChildrenCount(Stream DataStream)
        {
            DataStream.Seek(-1, SeekOrigin.Current);
            return DataStream.ReadByte();
        }

        public override string ToString() { return string.Format("{0:N0} : {1}   - {2}", Ordinate, Point, Position); }
    }
}
