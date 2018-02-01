using GMapElements.Readers.Implementations;
using GMapElements.Readers.Interfaces;

namespace GMapElements.Readers.Protocols
{
    public interface IProtocol
    {
        IMapReader CreateReader();
    }

    public class OldProtocol : IProtocol
    {
        public IMapReader CreateReader()
        {
            return new MapReader(new HeaderReader(),
                                 new PostReader(new TrackReader(new ObjectReader(20))));
        }
    }

    public class NewProtocol : IProtocol
    {
        public IMapReader CreateReader()
        {
            return new MapReader(new HeaderReader(),
                                 new PostReader(new TrackReader(new ObjectReader(22))));
        }
    }
}
