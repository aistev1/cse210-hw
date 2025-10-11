using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/*
  GoalManager responsibilities:
  - Hold the list of goals (encapsulation via private member)
  - Provide creation, listing, recording, save/load, and scoring logic
  - Implements creative extensions: simple leveling and badges
*/

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    // Badges / leveling thresholds (creativity)
    private int _level = 1;
    private int _xpPerLevel = 1000;

    public void CreateGoalFromInput()
    {
        Console.WriteLine("Choose goal type: 1=Simple, 2=Eternal, 3=Checklist");
        var t = Console.ReadLine();
        Console.Write("Title: ");
        var title = Console.ReadLine();
        Console.Write("Points (per event or for completion): ");
        if (!int.TryParse(Console.ReadLine(), out int pts)) pts = 100;
        switch (t)
        {
            case "1":
                _goals.Add(new SimpleGoal(title, pts));
                break;
            case "2":
                _goals.Add(new EternalGoal(title, pts));
                break;
            case "3":
                Console.Write("Required times to complete: ");
                if (!int.TryParse(Console.ReadLine(), out int req)) req = 3;
                Console.Write("Bonus points when completed: ");
                if (!int.TryParse(Console.ReadLine(), out int bonus)) bonus = 500;
                _goals.Add(new ChecklistGoal(title, pts, req, bonus));
                break;
            default:
                Console.WriteLine("Unknown type, creating Eternal by default.");
                _goals.Add(new EternalGoal(title, pts));
                break;
        }
    }

    public void ListGoals()
    {
        if (!_goals.Any())
        {
            Console.WriteLine("No goals yet.");
            return;
        }
        for (int i = 0; i < _goals.Count; i++)
        {
            var g = _goals[i];
            Console.WriteLine($"{i+1}. {g.GetDetailsString()}");
        }
    }

    public void RecordEventInteractive()
    {
        ListGoals();
        if (!_goals.Any()) return;
        Console.Write("Enter goal number to record event: ");
        if (!int.TryParse(Console.ReadLine(), out int idx)) return;
        idx--;
        if (idx < 0 || idx >= _goals.Count) return;
        var g = _goals[idx];
        int earned = g.RecordEvent(); 
        _score += earned;
        UpdateLevelAndBadges();
        Console.WriteLine($"You earned {earned} points. Total score: {_score}");
    }

    public void ShowScoreAndBadges()
    {
        Console.WriteLine($"Score: {_score}    Level: {_level}");
        var badges = GetBadges();
        Console.WriteLine("Badges: " + (badges.Any() ? string.Join(", ", badges) : "(none)"));
    }

    private void UpdateLevelAndBadges()
    {
        int newLevel = (_score / _xpPerLevel) + 1;
        if (newLevel > _level)
        {
            _level = newLevel;
            Console.WriteLine($"Congratulations! You reached level {_level}!");
        }
    }

    private List<string> GetBadges()
    {
        var badges = new List<string>();
        if (_level >= 2) badges.Add("Apprentice");
        if (_level >= 3) badges.Add("Adept");
        if (_level >= 5) badges.Add("Master");
        return badges;
    }

    public void SaveToFile(string path)
    {
        try
        {
            var lines = _goals.Select(g => g.Serialize()).ToList();
            lines.Insert(0, $"SCORE|{_score}|{_level}");
            File.WriteAllLines(path, lines);
            Console.WriteLine($"Saved { _goals.Count } goals to {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Save failed: " + ex.Message);
        }
    }

    public void LoadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("No save file found.");
            return;
        }
        try
        {
            var lines = File.ReadAllLines(path);
            _goals.Clear();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split('|');
                if (parts[0] == "SCORE")
                {
                    if (parts.Length >= 3)
                    {
                        if (int.TryParse(parts[1], out int sc)) _score = sc;
                        if (int.TryParse(parts[2], out int lvl)) _level = lvl;
                    }
                    continue;
                }
                switch (parts[0])
                {
                    case "Simple":
                        bool isComplete = parts.Length >= 4 && parts[3] == "1";
                        _goals.Add(new SimpleGoal(parts[1], int.Parse(parts[2]), isComplete));
                        break;
                    case "Eternal":
                        _goals.Add(new EternalGoal(parts[1], int.Parse(parts[2])));
                        break;
                    case "Checklist":
                        int points = int.Parse(parts[2]);
                        int required = int.Parse(parts[3]);
                        int bonus = int.Parse(parts[4]);
                        int completed = parts.Length >=6 ? int.Parse(parts[5]) : 0;
                        _goals.Add(new ChecklistGoal(parts[1], points, required, bonus, completed));
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine($"Loaded {_goals.Count} goals from {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Load failed: " + ex.Message);
        }
    }
}
