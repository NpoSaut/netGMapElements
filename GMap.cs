using System.Collections.Generic;
using System.IO;
using System.Linq;
using GMapElements.Entities;
using GMapElements.Readers.Implementations;
using GMapElements.Readers.Protocols;

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
            int CountUnknownObjects(GMap map)
            {
                return map.Sections
                          .SelectMany(s => s.Posts.SelectMany(p => p.Tracks.SelectMany(t => t.Objects)))
                          .Count(o => o.Type == GObjectType.Unknown);
            }

            var map1 = Load(MapStream, new OldProtocol());

            var map1UnknownObjectsCount = CountUnknownObjects(map1);
            if (map1UnknownObjectsCount == 0)
                return map1;

            MapStream.Seek(0, SeekOrigin.Begin);
            var map2 = Load(MapStream, new NewProtocol());
            var map2UnknownObjectsCount = CountUnknownObjects(map2);

            return map1UnknownObjectsCount < map2UnknownObjectsCount ? map1 : map2;
        }

        public static GMap Load(Stream MapStream, IProtocol Protocol)
        {
            var reader = Protocol.CreateReader();
            return reader.Read(MapStream);
        }
    }
}
