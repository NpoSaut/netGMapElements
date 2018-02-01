using System.IO;
using GMapElements.Entities;
using JetBrains.Annotations;

namespace GMapElements.Readers.Interfaces
{
    public interface IObjectReader
    {
        [NotNull]
        GObject Read([NotNull] Stream MapStream);
    }
}
