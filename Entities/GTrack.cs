using System.Collections.Generic;

namespace GMapElements.Entities
{
    public class GTrack : GElement
    {
        public GTrack(int Number)
        {
            this.Number = Number;
            Objects     = new List<GObject>();
        }

        public int Number { get; }

        public IList<GObject> Objects { get; }

        public override string ToString()
        {
            return $"{Number} | {string.Join(", ", Objects)}";
        }
    }
}
