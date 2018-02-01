using System.IO;
using GMapElements.Entities;
using JetBrains.Annotations;

namespace GMapElements.Readers.Interfaces
{
    public interface IPostReader
    {
        [NotNull]
        GPost Read([NotNull] Stream MapStream, int SectionId);
    }
}
