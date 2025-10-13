using System;

class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, double length, int laps)
        : base(date, length)
    {
        _laps = laps;
    }

    public override double GetDistanceKm() => _laps * 50.0 / 1000;
    public override double GetDistanceMiles() => GetDistanceKm() * 0.62;
    public override double GetSpeedKph() => GetDistanceKm() / GetLength() * 60;
    public override double GetSpeedMph() => GetDistanceMiles() / GetLength() * 60;
    public override double GetPaceMinutesPerKm() => GetLength() / GetDistanceKm();
    public override double GetPaceMinutesPerMile() => GetLength() / GetDistanceMiles();
}
