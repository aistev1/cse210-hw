using System;

public class EternalGoal : Goal
{
    public EternalGoal(string title, int points) : base(title, points) { }

    public override int RecordEvent()
    {
        return Points;
    }

    public override string GetDetailsString()
    {
        return $"[∞] {Title} (Eternal)";
    }

    public override string Serialize()
    {
        return $"Eternal|{Title}|{Points}";
    }
}
