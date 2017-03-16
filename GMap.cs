using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GMapElements
{
    public class GMap
    {
        public GHeader Header { get; set; }
        public List<GSection> Sections { get; set; }

        public static GMap Load(Stream FromStream)
        {
            var h = GElement.FromStream<GHeader>(FromStream);

            return new GMap
                   {
                       Header = h,
                       Sections = LoadSections(FromStream, h.PostsCount).ToList()
                   };
        }

        private static IEnumerable<GSection> LoadSections(Stream str, int PostsCount)
        {
            var postCounter = 0;
            var sectionCounter = 1;
            while (postCounter < PostsCount)
            {
                var sec = new GSection { Id = postCounter, Posts = LoadPosts(str, sectionCounter).ToList() };
                postCounter += sec.Posts.Count;
                sectionCounter++;
                yield return sec;
            }
        }

        private static IEnumerable<GPost> LoadPosts(Stream str, int SectionId)
        {
            GPost p;
            while (true)
            {
                p = GElement.FromStream<GPost>(str);
                p.SectionId = SectionId;
                yield return p;
                if (p.Position == PositionInSection.End) yield break;
            }
        }
    }
}
