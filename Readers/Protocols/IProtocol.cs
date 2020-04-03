using GMapElements.Readers.Implementations;
using GMapElements.Readers.Interfaces;

namespace GMapElements.Readers.Protocols
{
    public interface IProtocol
    {
        IMapReader CreateReader();
    }

    public class Protocol1 : IProtocol
    {
        public IMapReader CreateReader()
        {
            return new MapReader(new HeaderReader(15, 5, 20),
                                 h => new PostReader(h.PostRecordLength,
                                                     new TrackReader(h.TrackRecordLength,
                                                                     new ObjectReader(h.ObjectRecordLength))));
        }
    }

    public class Protocol2 : IProtocol
    {
        public IMapReader CreateReader()
        {
            return new MapReader(new HeaderReader(15, 5, 22),
                                 h => new PostReader(h.PostRecordLength,
                                                     new TrackReader(h.TrackRecordLength,
                                                                     new ObjectReader(h.ObjectRecordLength))));
        }
    }

    public class Protocol3 : IProtocol
    {
        public IMapReader CreateReader()
        {
            return new MapReader(new ExtendedHeaderReader(),
                                 h => new PostReader(h.PostRecordLength,
                                                     new TrackReader(h.TrackRecordLength,
                                                                     new ObjectReader(h.ObjectRecordLength))));
        }
    }
}