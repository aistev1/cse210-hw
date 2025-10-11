using System;

public class ChecklistGoal : Goal
{
    private int _completed;
    private int _required;
    private int _bonus;

    public ChecklistGoal(string title, int pointsPer, int required, int bonus, int completed = 0)
        : base(title, pointsPer)
    {
        _completed = completed;
        _required = required;
        _bonus = bonus;
    }

    public override int RecordEvent()
    {
        if (_completed >= _required)
        {
            return 0;
        }
        _completed++;
        int award = Points;
        if (_completed == _required)
        {
            award += _bonus;
        }
        return award;
    }

    public override string GetDetailsString()
    {
        return $"[{(_completed >= _required ? "X" : " ")}] {Title} (Checklist) — Completed {_completed}/{_required}";
    }

    public override string Serialize()
    {
        return $"Checklist|{Title}|{Points}|{_required}|{_bonus}|{_completed}";
    }
}
