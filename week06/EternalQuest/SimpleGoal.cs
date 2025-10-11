using System;

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string title, int points, bool isComplete = false) : base(title, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (_isComplete)
        {
            return 0;
        }
        _isComplete = true;
        return Points;
    }

    public override string GetDetailsString()
    {
        return $"{(_isComplete ? "[X]" : "[ ]")} {Title} (Simple)";
    }

    public override string Serialize()
    {
        return $"Simple|{Title}|{Points}|{(_isComplete ? 1 : 0)}";
    }
}
