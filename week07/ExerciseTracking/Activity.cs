using System;

abstract class Activity
{
    private DateTime _date;
    private double _length; 

    public Activity(DateTime date, double length)
    {
        _date = date;
        _length = length;
    }

    public DateTime GetDate() => _date;
    public double GetLength() => _length;

    public abstract double GetDistanceMiles();
    public abstract double GetDistanceKm();
    public abstract double GetSpeedMph();
    public abstract double GetSpeedKph();
    public abstract double GetPaceMinutesPerMile();
    public abstract double GetPaceMinutesPerKm();

    public virtual void GetSummary()
    {
        Console.WriteLine($"{_date:dd MMM yyyy} {this.GetType().Name} ({_length} min) - " +
            $"Distance {GetDistanceMiles():F1} miles, Speed {GetSpeedMph():F1} mph, Pace: {GetPaceMinutesPerMile():F2} min per mile");
        Console.WriteLine($"{_date:dd MMM yyyy} {this.GetType().Name} ({_length} min): " +
            $"Distance {GetDistanceKm():F1} km, Speed {GetSpeedKph():F1} kph, Pace: {GetPaceMinutesPerKm():F2} min per km");
    }
}
