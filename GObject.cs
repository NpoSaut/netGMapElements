using System.Collections.Generic;
using System.Text;

namespace GMapElements
{
    public enum AlsnFrequency
    {
        Alsn25 = 25,
        Alsn50 = 50,
        Alsn75 = 75,
        NoAlsn = -1,
        Unknown = 0
    }

    [GLength(20)]
    public class GObject : GElement
    {
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
                { 11, GObjectType.DeadEnd }
            };

        public int Ordinate { get; private set; }
        public int Length { get; private set; }
        public AlsnFrequency AlsnFreq { get; private set; }
        public string Name { get; private set; }
        public int SpeedRestriction { get; private set; }
        public GObjectType Type { get; private set; }

        protected override void FillWithBytes(byte[] Data)
        {
            Type = _objectTypeCodes.ContainsKey(Data[0]) ? _objectTypeCodes[Data[0]] : GObjectType.Unknown;
            Length = SubInt(Data, 1, 2);
            Ordinate = SubInt(Data, 7, 3);
            SpeedRestriction = Data[5];
            AlsnFreq = AlsnFromCode(Data[6]);
            Name = Encoding.GetEncoding(1251).GetString(Data, 10, 8).Trim();
        }

        private AlsnFrequency AlsnFromCode(int alsnCode)
        {
            switch (alsnCode)
            {
                case (0):
                    return AlsnFrequency.Alsn25;
                case (1):
                    return AlsnFrequency.Alsn50;
                case (2):
                    return AlsnFrequency.Alsn75;
                case (3):
                    return AlsnFrequency.NoAlsn;
                default:
                    return AlsnFrequency.Unknown;
            }
        }

        public override string ToString() { return string.Format("{0}{1}", GetObjectTypeShortName(Type), Name != null ? string.Format(" \"{0}\"", Name) : ""); }

        public static string GetObjectTypeShortName(GObjectType t)
        {
            switch (t)
            {
                case (GObjectType.Bridge):
                    return "МСТ";
                case (GObjectType.Crossing):
                    return "ПРЕ";
                case (GObjectType.DangerousPlace):
                    return "ОПМ";
                case (GObjectType.DeadEnd):
                    return "ТПК";
                case (GObjectType.GpuSaut):
                    return "ГПУ";
                case (GObjectType.Platform):
                    return "ПЛФ";
                case (GObjectType.Station):
                    return "СТЦ";
                case (GObjectType.Switch):
                    return "СТР";
                case (GObjectType.Tks):
                    return "ТКС";
                case (GObjectType.TrafficLight):
                    return "СВФ";
                case (GObjectType.Tunnel):
                    return "ТНЛ";
                case (GObjectType.Unknown):
                    return "НЗВ";
                default:
                    return t.ToString();
            }
        }
    }
}
