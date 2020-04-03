using System;
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

        private static IProtocol[] _supportedProtocols =
        {
            new Protocol1(),
            new Protocol2(),
            new Protocol3(),
        };

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
            
            
            var results = new List<GMap>();

            foreach (var protocol in _supportedProtocols)
            {
                try
                {
                    MapStream.Seek(0, SeekOrigin.Begin);
                    var map = Load(MapStream, protocol);
                    results.Add(map);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Не смогли загрузить карту, используя протокол {protocol.GetType().Name}: {e.Message}");
                }
            }

            var bestResult = results.OrderBy(CountUnknownObjects).FirstOrDefault();
            
            if (bestResult == null)
                throw new ApplicationException("Не удалось расшифровать карту. Файл карты повреждён, либо вышел новый формат электронной карты.");
            
            return bestResult;
        }

        public static GMap Load(Stream MapStream, IProtocol Protocol)
        {
            var reader = Protocol.CreateReader();
            return reader.Read(MapStream);
        }
    }
}
