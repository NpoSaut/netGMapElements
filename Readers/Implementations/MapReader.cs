using System.IO;
using GMapElements.Entities;
using GMapElements.Readers.Interfaces;

namespace GMapElements.Readers.Implementations
{
    public class MapReader : ReaderBase, IMapReader
    {
        private readonly IHeaderReader _headerReader;
        private readonly IPostReader _postReader;

        public MapReader(IHeaderReader HeaderReader, IPostReader PostReader)
        {
            _headerReader = HeaderReader;
            _postReader   = PostReader;
        }

        public GMap Read(Stream MapStream)
        {
            var header = _headerReader.Read(MapStream);
            var map    = new GMap(header);

            var postCounter    = 0;
            var sectionCounter = 0;

            while (postCounter < header.PostsCount)
            {
                var   section = new GSection(++sectionCounter);
                GPost post;
                do
                {
                    post = _postReader.Read(MapStream, section.Id);
                    section.Posts.Add(post);
                    postCounter++;
                } while (post.Position != PositionInSection.End);

                map.Sections.Add(section);
            }

            return map;
        }
    }
}
