/* EXCEEDED REQUIREMENTS 
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
using System.Linq;

namespace MindfulnessProgram
{
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
                    case "1": activity = new BreathingActivity(); break;
                    case "2": activity = new ReflectionActivity(); break;
                    case "3": activity = new ListingActivity(); break;
                    case "4": activity = new GratitudeActivity(); break;
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