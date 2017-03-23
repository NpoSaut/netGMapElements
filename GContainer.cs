using System;
using System.Collections.Generic;
using System.IO;

namespace GMapElements
{
    public abstract class GContainer<TChild> : GElement
        where TChild : GElement, new()
    {
        protected List<TChild> Children { get; private set; }
        protected int ChildrenStartAddress { get; set; }
        protected int ChildrenCount { get; set; }

        protected virtual int GetChildrenCount(Stream DataStream) { return ChildrenCount; }

        protected override void FillFromStream(Stream DataStream)
        {
            base.FillFromStream(DataStream);
            LoadChildren(DataStream);
        }

        private void LoadChildren(Stream DataStream)
        {
            if (!DataStream.CanSeek) throw new ArgumentException("Поток данных должен поддерживать функцию поиска", "DataStream");

            var startPosition = DataStream.Position;
            Children = new List<TChild>();

            DataStream.Seek(ChildrenStartAddress, SeekOrigin.Begin);
            var cCount = GetChildrenCount(DataStream);

            for (var i = 0; i < cCount; i++)
            {
                var child = FromStream<TChild>(DataStream);
                Children.Add(child);
            }
            DataStream.Seek(startPosition, SeekOrigin.Begin);
        }
    }
}
