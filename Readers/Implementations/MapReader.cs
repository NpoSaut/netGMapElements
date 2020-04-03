using System;
using System.IO;
using GMapElements.Entities;
using GMapElements.Readers.Interfaces;

namespace GMapElements.Readers.Implementations
{
    public class MapReader : ReaderBase, IMapReader
    {
        private readonly IHeaderReader _headerReader;
        private readonly Func<GHeader, IPostReader> _postReaderFactory;

        public MapReader(IHeaderReader HeaderReader, Func<GHeader, IPostReader> postReaderFactoryFactory)
        {
            _headerReader      = HeaderReader;
            _postReaderFactory = postReaderFactoryFactory;
        }

        public GMap Read(Stream MapStream)
        {
            var header     = _headerReader.Read(MapStream);
            var postReader = _postReaderFactory(header);

            var map = new GMap(header);

            var postCounter    = 0;
            var sectionCounter = 0;

            while (postCounter < header.PostsCount)
            {
                var   section = new GSection(++sectionCounter);
                GPost post;
                do
                {
                    post = postReader.Read(MapStream, section.Id);
                    section.Posts.Add(post);
                    postCounter++;
                } while (post.Position != PositionInSection.End);

                map.Sections.Add(section);
            }

            return map;
        }
    }
}