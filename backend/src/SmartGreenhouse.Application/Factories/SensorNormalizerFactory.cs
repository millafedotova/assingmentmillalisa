using SmartGreenhouse.Domain.Enums;

public static class SensorNormalizerFactory
{
    public static ISensorNormalizer Create(SensorTypeEnum type) =>
        type switch
        {
            SensorTypeEnum.Temperature => new CelsiusNormalizer(),
            SensorTypeEnum.Humidity => new PercentageNormalizer(),
            SensorTypeEnum.Light => new LuxNormalizer(),
            SensorTypeEnum.SoilMoisture => new PercentageNormalizer(),
            _ => throw new ArgumentOutOfRangeException()
        };

    private class CelsiusNormalizer : ISensorNormalizer
    {
        public string CanonicalUnit => "°C";
        public double Normalize(double raw) => raw;
    }

    private class PercentageNormalizer : ISensorNormalizer
    {
        public string CanonicalUnit => "%";
        public double Normalize(double raw) => Math.Clamp(raw, 0, 100);
    }

    private class LuxNormalizer : ISensorNormalizer
    {
        public string CanonicalUnit => "lux";
        public double Normalize(double raw) => Math.Max(0, raw);
    }
}
