using System;

namespace GMapElements.Entities
{
    public class GHeader : GElement
    {
        public GHeader(int PostsCount, ushort Number, DateTime ConversionDate)
        {
            this.PostsCount     = PostsCount;
            this.Number         = Number;
            this.ConversionDate = ConversionDate;
        }

        public int      PostsCount     { get; }
        public ushort   Number         { get; }
        public DateTime ConversionDate { get; }
    }
}
