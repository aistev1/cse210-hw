using System;

class Cycling : Activity
{
    private double _speedMph;

    public Cycling(DateTime date, double length, double speedMph)
        : base(date, length)
    {
        _speedMph = speedMph;
    }

    public override double GetDistanceMiles() => _speedMph * GetLength() / 60;
    public override double GetDistanceKm() => GetDistanceMiles() / 0.62;
    public override double GetSpeedMph() => _speedMph;
    public override double GetSpeedKph() => _speedMph / 0.62;
    public override double GetPaceMinutesPerMile() => 60 / _speedMph;
    public override double GetPaceMinutesPerKm() => 60 / GetSpeedKph();
}
