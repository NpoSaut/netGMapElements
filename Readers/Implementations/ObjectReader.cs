using System.Collections.Generic;
using System.IO;
using System.Text;
using GMapElements.Entities;
using GMapElements.Readers.Interfaces;

namespace GMapElements.Readers.Implementations
{
    public class ObjectReader : ReaderBase, IObjectReader
    {
        private const int RecordLength = 20;

        private static readonly Dictionary<int, GObjectType> _objectTypeCodes =
            new Dictionary<int, GObjectType>
            {
                { 1, GObjectType.TrafficLight },
                { 2, GObjectType.Station },
                { 3, GObjectType.DangerousPlace },
                { 4, GObjectType.Bridge },
                { 5, GObjectType.Crossing },
                { 6, GObjectType.Platform },
                { 7, GObjectType.Tunnel },
                { 8, GObjectType.Switch },
                { 9, GObjectType.Tks },
                { 10, GObjectType.GpuSaut },
                { 11, GObjectType.DeadEnd },
                { 15, GObjectType.BrakeCheck },
                { 16, GObjectType.NeutralInsertion },
                { 17, GObjectType.CurrentSection }
            };

        public GObject Read(Stream MapStream)
        {
            var data = ReadBytes(MapStream, RecordLength);

            var type             = _objectTypeCodes.ContainsKey(data[0]) ? _objectTypeCodes[data[0]] : GObjectType.Unknown;
            var length           = SubInt(data, 1, 2);
            var ordinate         = SubInt(data, 7, 3);
            var speedRestriction = data[5];
            var alsnFreq         = AlsnFromCode(data[6]);
            var name             = Encoding.GetEncoding(1251).GetString(data, 10, 8).Trim('\0', ' ');

            return new GObject(ordinate, length, type, name, alsnFreq, speedRestriction);
        }

        private AlsnFrequency AlsnFromCode(int alsnCode)
        {
            switch (alsnCode)
            {
                case 0:
                    return AlsnFrequency.Alsn25;
                case 1:
                    return AlsnFrequency.Alsn50;
                case 2:
                    return AlsnFrequency.Alsn75;
                case 3:
                    return AlsnFrequency.NoAlsn;
                default:
                    return AlsnFrequency.Unknown;
            }
        }
    }
}
