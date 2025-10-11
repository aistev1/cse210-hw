using System;

public abstract class Goal
{
    private string _title;
    private int _points;

    public Goal(string title, int points)
    {
        _title = title;
        _points = points;
    }

    public string Title { get => _title; }
    public int Points { get => _points; }

    public abstract int RecordEvent(); 
    public virtual string GetDetailsString()
    {
        return $"{Title}";
    }

    public abstract string Serialize();
}
