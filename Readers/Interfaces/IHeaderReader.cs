using System.IO;
using GMapElements.Entities;
using JetBrains.Annotations;

namespace GMapElements.Readers.Interfaces
{
    public interface IHeaderReader
    {
        [NotNull]
        GHeader Read([NotNull] Stream MapStream);
    }
}
