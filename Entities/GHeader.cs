using System;

namespace GMapElements.Entities
{
    public class GHeader : GElement
    {
        public GHeader(int PostsCount, ushort Number, DateTime ConversionDate,
            int postRecordLength, int trackRecordLength, int objectRecordLength)
        {
            this.PostsCount     = PostsCount;
            this.Number         = Number;
            this.ConversionDate = ConversionDate;
            PostRecordLength    = postRecordLength;
            TrackRecordLength   = trackRecordLength;
            ObjectRecordLength  = objectRecordLength;
        }

        public int      PostsCount     { get; }
        public ushort   Number         { get; }
        public DateTime ConversionDate { get; }

        public int PostRecordLength   { get; }
        public int TrackRecordLength  { get; }
        public int ObjectRecordLength { get; }
    }
}