using System.Collections.Generic;
using System.IO;
using GMapElements.Entities;
using GMapElements.Readers.Implementations;
using GMapElements.Readers.Interfaces;

namespace GMapElements
{
    public class GMap
    {
        public GMap(GHeader Header)
        {
            this.Header = Header;
            Sections    = new List<GSection>();
        }

        public GHeader         Header   { get; }
        public IList<GSection> Sections { get; }

        public static GMap Load(string FileName)
        {
            using (var fs = File.OpenRead(FileName))
            {
                return Load(fs);
            }
        }

        public static GMap Load(Stream MapStream)
        {
            return Load(MapStream, new MapReader(new HeaderReader(),
                                                 new PostReader(new TrackReader(new ObjectReader()))));
        }

        public static GMap Load(Stream MapStream, IMapReader Reader)
        {
            return Reader.Read(MapStream);
        }
    }
}
