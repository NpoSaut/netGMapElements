using System.Collections.Generic;

namespace GMapElements
{
    [GLength(5)]
    public class GTrack : GContainer<GObject>
    {
        public int Number { get; private set; }

        internal bool IsValid
        {
            get { return ChildrenStartAddress != 0xffffff; }
        }

        public IList<GObject> Objects
        {
            get { return Children; }
        }

        protected override void FillWithBytes(byte[] Data)
        {
            Number = Data[0];
            ChildrenCount = Data[1];
            ChildrenStartAddress = SubInt(Data, 2, 3);
        }

        public override string ToString() { return string.Format("{0} | {1}", Number, string.Join(", ", Objects)); }
    }
}
