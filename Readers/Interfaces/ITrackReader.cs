using System.IO;
using GMapElements.Entities;
using JetBrains.Annotations;

namespace GMapElements.Readers.Interfaces
{
    public interface ITrackReader
    {
        [CanBeNull]
        GTrack Read([NotNull] Stream MapStream);
    }
}
