using System.IO;
using JetBrains.Annotations;

namespace GMapElements.Readers.Interfaces
{
    public interface IMapReader
    {
        [NotNull]
        GMap Read([NotNull] Stream MapStream);
    }
}
