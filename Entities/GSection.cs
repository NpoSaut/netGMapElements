using System.Collections.Generic;

namespace GMapElements.Entities
{
    public class GSection
    {
        public GSection(int Id)
        {
            this.Id = Id;
            Posts   = new List<GPost>();
        }

        public int         Id    { get; }
        public List<GPost> Posts { get; }
    }
}
