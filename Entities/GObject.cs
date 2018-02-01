namespace GMapElements.Entities
{
    public enum AlsnFrequency
    {
        Alsn25 = 25,
        Alsn50 = 50,
        Alsn75 = 75,
        NoAlsn = -1,
        Unknown = 0
    }

    public class GObject : GElement
    {
        public GObject(int Ordinate, int Length, GObjectType Type, string Name, AlsnFrequency AlsnFreq, int SpeedRestriction)
        {
            this.Ordinate         = Ordinate;
            this.Length           = Length;
            this.AlsnFreq         = AlsnFreq;
            this.Name             = Name;
            this.SpeedRestriction = SpeedRestriction;
            this.Type             = Type;
        }

        public int           Ordinate         { get; }
        public int           Length           { get; }
        public AlsnFrequency AlsnFreq         { get; }
        public string        Name             { get; }
        public int           SpeedRestriction { get; }
        public GObjectType   Type             { get; }

        public override string ToString()
        {
            var name = Name != null ? $" \"{Name}\"" : "";
            return $"{GetObjectTypeShortName(Type)}{name}";
        }

        private static string GetObjectTypeShortName(GObjectType t)
        {
            switch (t)
            {
                case GObjectType.Bridge:
                    return "МСТ";
                case GObjectType.Crossing:
                    return "ПРЕ";
                case GObjectType.DangerousPlace:
                    return "ОПМ";
                case GObjectType.DeadEnd:
                    return "ТПК";
                case GObjectType.GpuSaut:
                    return "ГПУ";
                case GObjectType.Platform:
                    return "ПЛФ";
                case GObjectType.Station:
                    return "СТЦ";
                case GObjectType.Switch:
                    return "СТР";
                case GObjectType.Tks:
                    return "ТКС";
                case GObjectType.TrafficLight:
                    return "СВФ";
                case GObjectType.Tunnel:
                    return "ТНЛ";
                case GObjectType.Unknown:
                    return "НЗВ";
                default:
                    return t.ToString();
            }
        }
    }
}
