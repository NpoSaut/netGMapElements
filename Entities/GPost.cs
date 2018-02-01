using System.Collections.Generic;
using Geographics;

namespace GMapElements.Entities
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

    public class GPost : GElement
    {
        public GPost(int Ordinate, EarthPoint Point, OrdinateDirection Direction, PositionInSection Position, int SectionId)
        {
            this.Ordinate  = Ordinate;
            this.Point     = Point;
            this.Direction = Direction;
            this.Position  = Position;
            this.SectionId = SectionId;
            Tracks         = new List<GTrack>();
        }

        /// <summary>Линейная ордината</summary>
        public int Ordinate { get; }

        /// <summary>Широта и долгота точки</summary>
        public EarthPoint Point { get; }

        public OrdinateDirection Direction { get; }

        public IList<GTrack> Tracks { get; }

        public PositionInSection Position  { get; }
        public int               SectionId { get; }

        public override string ToString()
        {
            return $"{Ordinate:N0} : {Point}   - {Position}";
        }
    }
}
