using System;

class Running : Activity
{
    private double _distanceMiles;

    public Running(DateTime date, double length, double distanceMiles)
        : base(date, length)
    {
        _distanceMiles = distanceMiles;
    }

    public override double GetDistanceMiles() => _distanceMiles;
    public override double GetDistanceKm() => _distanceMiles / 0.62;
    public override double GetSpeedMph() => GetDistanceMiles() / GetLength() * 60;
    public override double GetSpeedKph() => GetDistanceKm() / GetLength() * 60;
    public override double GetPaceMinutesPerMile() => GetLength() / GetDistanceMiles();
    public override double GetPaceMinutesPerKm() => GetLength() / GetDistanceKm();
}
