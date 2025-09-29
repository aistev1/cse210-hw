/*
EXCEEDED REQUIREMENTS
 Added an extra activity: GratitudeActivity (beyond the required three)
 Session-aware prompt rotation: prompts/questions are not reused until all have been used
 Activity logging: every completed activity is logged to a JSON file (mindfulness_log.json)
 Load/Save log: program reads existing log and updates it automatically
 Statistics display: menu shows how many times each activity has been completed
 Enhanced animations: 
    Breathing uses expanding/contracting visualization
    Smooth spinner animations
    Countdown timers with visual feedback

*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace MindfulnessProgram
{
    public class ActivityLogEntry
    {
        public string ActivityName { get; set; } = "";
        public DateTime Timestamp { get; set; }
        public int DurationSeconds { get; set; }
        public int ItemsCount { get; set; }
    }

    public class ActivityLogger
    {
        private readonly string _path;
        private readonly List<ActivityLogEntry> _entries = new List<ActivityLogEntry>();

        public ActivityLogger(string path = "mindfulness_log.json")
        {
            _path = path;
            Load();
        }

        private void Load()
        {
            try
            {
                if (!File.Exists(_path)) return;
                string json = File.ReadAllText(_path);
                List<ActivityLogEntry> data = JsonSerializer.Deserialize<List<ActivityLogEntry>>(json);
                if (data != null)
                    _entries.AddRange(data);
            }
            catch
            {
            }
        }

        public List<ActivityLogEntry> GetEntries() => _entries.ToList();

        public void Add(ActivityLogEntry entry)
        {
            _entries.Add(entry);
            Save();
        }

        private void Save()
        {
            try
            {
                string json = JsonSerializer.Serialize(_entries, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_path, json);
            }
            catch
            {
            }
        }
    }

    public abstract class Activity
    {
        private readonly string _name;
        private readonly string _description;
        protected Random _rng = new Random();

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public void Start(ActivityLogger logger)
        {
            ShowStartingMessage();
            int durationSeconds = PromptForDuration();
            PrepareToBegin();

            DateTime startTime = DateTime.Now;
            Run(durationSeconds);
            DateTime endTime = DateTime.Now;
            int actualDuration = (int)(endTime - startTime).TotalSeconds;

            ShowEndingMessage(actualDuration);
            
            logger.Add(new ActivityLogEntry
            {
                ActivityName = _name,
                Timestamp = DateTime.Now,
                DurationSeconds = actualDuration,
                ItemsCount = GetItemsCount()
            });
        }

        protected virtual int GetItemsCount() => 0;

        protected abstract void Run(int durationSeconds);

        private void ShowStartingMessage()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to the {_name}");
            Console.WriteLine("=" + new string('=', _name.Length));
            Console.WriteLine(_description);
            Console.WriteLine();
        }

        private int PromptForDuration()
        {
            while (true)
            {
                Console.Write("How long, in seconds, would you like for your session? ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int seconds) && seconds > 0)
                    return seconds;
                Console.WriteLine("Please enter a positive number.");
            }
        }

        private void PrepareToBegin()
        {
            Console.WriteLine("\nGet ready...");
            ShowSpinner(3);
        }

        private void ShowEndingMessage(int duration)
        {
            Console.WriteLine("\nWell done!");
            ShowSpinner(2);
            Console.WriteLine($"You have completed another {duration} seconds of the {_name}.");
            ShowSpinner(3);
        }

        protected void ShowSpinner(int seconds)
        {
            string[] spinner = { "|", "/", "-", "\\" };
            DateTime endTime = DateTime.Now.AddSeconds(seconds);
            int counter = 0;

            while (DateTime.Now < endTime)
            {
                Console.Write($"\r{spinner[counter % 4]} ");
                Thread.Sleep(250);
                counter++;
            }
            Console.Write("\r  \r");
        }

        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"\r{i}... ");
                Thread.Sleep(1000);
            }
            Console.Write("\r    \r");
        }

        protected string GetRandomPrompt(List<string> prompts, List<string> usedPrompts)
        {
            if (usedPrompts.Count >= prompts.Count)
                usedPrompts.Clear();

            var available = prompts.Where(p => !usedPrompts.Contains(p)).ToList();
            string selected = available[_rng.Next(available.Count)];
            usedPrompts.Add(selected);
            return selected;
        }
    }

    public class BreathingActivity : Activity
    {
        public BreathingActivity() : base(
            "Breathing Activity",
            "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.")
        { }

        protected override void Run(int durationSeconds)
        {
            Console.WriteLine("Starting breathing exercise...\n");
            int elapsed = 0;

            while (elapsed < durationSeconds)
            {
                Console.Write("Breathe in... ");
                ShowEnhancedBreathAnimation(4, true);
                elapsed += 4;
                if (elapsed >= durationSeconds) break;

                Console.Write("Breathe out... ");
                ShowEnhancedBreathAnimation(4, false);
                elapsed += 4;
                Console.WriteLine();
            }
        }

        private void ShowEnhancedBreathAnimation(int seconds, bool breatheIn)
        {
            if (breatheIn)
            {
                for (int i = 1; i <= seconds; i++)
                {
                    int width = (int)(20 * (i / (double)seconds));
                    string bar = new string('█', width).PadRight(20);
                    Console.Write($"\rBreathe in... [{bar}] ");
                    Thread.Sleep(1000);
                }
            }
            else
            {
                for (int i = seconds; i >= 1; i--)
                {
                    int width = (int)(20 * (i / (double)seconds));
                    string bar = new string('█', width).PadRight(20);
                    Console.Write($"\rBreathe out... [{bar}] ");
                    Thread.Sleep(1000);
                }
            }
            Console.WriteLine();
        }
    }

    public class ReflectionActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        private List<string> _usedPrompts = new List<string>();
        private List<string> _usedQuestions = new List<string>();

        public ReflectionActivity() : base(
            "Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        { }

        protected override void Run(int durationSeconds)
        {
            string prompt = GetRandomPrompt(_prompts, _usedPrompts);
            Console.WriteLine($"Consider the following prompt:\n");
            Console.WriteLine($"--- {prompt} ---\n");
            Console.WriteLine("When you have something in mind, press enter to continue.");
            Console.ReadLine();

            Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
            Console.Write("You may begin in: ");
            ShowCountdown(5);
            Console.WriteLine();

            DateTime endTime = DateTime.Now.AddSeconds(durationSeconds);
            while (DateTime.Now < endTime)
            {
                string question = GetRandomPrompt(_questions, _usedQuestions);
                Console.Write($"> {question} ");
                ShowSpinner(8);
                Console.WriteLine();
            }
        }
    }

    public class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private List<string> _usedPrompts = new List<string>();
        private List<string> _items = new List<string>();

        public ListingActivity() : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        { }

        protected override void Run(int durationSeconds)
        {
            string prompt = GetRandomPrompt(_prompts, _usedPrompts);
            Console.WriteLine("List as many responses as you can to the following prompt:");
            Console.WriteLine($"--- {prompt} ---");
            Console.Write("You may begin in: ");
            ShowCountdown(5);
            Console.WriteLine();

            DateTime endTime = DateTime.Now.AddSeconds(durationSeconds);
            int itemCount = 0;

            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                string item = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    _items.Add(item);
                    itemCount++;
                }
            }

            Console.WriteLine($"\nYou listed {itemCount} items!");
        }

        protected override int GetItemsCount() => _items.Count;
    }

    public class GratitudeActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "What are three things you're grateful for today?",
            "Who has helped you recently that you appreciate?",
            "What simple pleasures brought you joy this week?",
            "What opportunities are you thankful for right now?"
        };

        private List<string> _usedPrompts = new List<string>();

        public GratitudeActivity() : base(
            "Gratitude Activity",
            "This activity will help you cultivate gratitude by focusing on positive aspects of your life. Research shows gratitude practice improves mental wellbeing.")
        { }

        protected override void Run(int durationSeconds)
        {
            string prompt = GetRandomPrompt(_prompts, _usedPrompts);
            Console.WriteLine($"Reflect on: {prompt}\n");
            Console.WriteLine("Take a moment to feel gratitude for each item...");

            DateTime endTime = DateTime.Now.AddSeconds(durationSeconds);
            int gratitudeItems = 0;

            while (DateTime.Now < endTime && gratitudeItems < 10)
            {
                Console.Write($"Item {gratitudeItems + 1}: ");
                string response = Console.ReadLine();
                
                if (!string.IsNullOrWhiteSpace(response))
                {
                    gratitudeItems++;
                    Console.Write("Reflecting with gratitude ");
                    ShowSpinner(4);
                    Console.WriteLine();
                }
            }

            Console.WriteLine($"\nThank you for acknowledging {gratitudeItems} things you're grateful for!");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ActivityLogger logger = new ActivityLogger();
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program Menu");
                Console.WriteLine("========================");
                
                ShowActivityStats(logger);

                Console.WriteLine("\nChoose an activity:");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Gratitude Activity (Extra)");
                Console.WriteLine("5. View Activity Log");
                Console.WriteLine("6. Quit");
                Console.Write("\nSelect a choice from the menu: ");

                string choice = Console.ReadLine();
                Activity activity = null;

                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;
                    case "2":
                        activity = new ReflectionActivity();
                        break;
                    case "3":
                        activity = new ListingActivity();
                        break;
                    case "4":
                        activity = new GratitudeActivity();
                        break;
                    case "5":
                        ShowDetailedLog(logger);
                        Console.WriteLine("\nPress Enter to continue...");
                        Console.ReadLine();
                        continue;
                    case "6":
                        Console.WriteLine("Thank you for practicing mindfulness!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        continue;
                }

                activity.Start(logger);
                Console.WriteLine("\nPress Enter to return to menu...");
                Console.ReadLine();
            }
        }

        static void ShowActivityStats(ActivityLogger logger)
        {
            var entries = logger.GetEntries();
            if (entries.Any())
            {
                Console.WriteLine($"\nSession History: {entries.Count} total activities");
                var stats = entries.GroupBy(e => e.ActivityName)
                                 .Select(g => new { Name = g.Key, Count = g.Count() })
                                 .OrderByDescending(s => s.Count);

                foreach (var stat in stats)
                {
                    Console.WriteLine($"  {stat.Name}: {stat.Count} times");
                }
            }
        }

        static void ShowDetailedLog(ActivityLogger logger)
        {
            Console.WriteLine("\n=== Activity Log ===");
            var entries = logger.GetEntries().OrderByDescending(e => e.Timestamp);
            
            if (!entries.Any())
            {
                Console.WriteLine("No activities logged yet.");
                return;
            }

            foreach (var entry in entries.Take(15))
            {
                Console.WriteLine($"{entry.Timestamp:MM/dd HH:mm} - {entry.ActivityName} ({entry.DurationSeconds}s)");
                if (entry.ItemsCount > 0)
                    Console.WriteLine($"  Items listed: {entry.ItemsCount}");
            }
        }
    }
}