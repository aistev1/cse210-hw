using System;
using System.Collections.Generic;

/*

  Creativity
   - Leveling system: The user accumulates XP (points) and levels up every 1000 XP. This demonstrates additional gamification beyond the requirements.
   - Badges: Simple textual badges ("Apprentice", "Adept", "Master") are awarded at level milestones to encourage continued engagement.
   - Save format: Goals, score, and level are saved in a text file (goals.txt)
   - Serialization: Each Goal type implements Serialize() for consistent saving/loading, showing use of polymorphism in persistence.
   - Readable UI: Console menu provides create/list/record/save/load options per spec.
   - Extensibility: New goal types can be added by deriving from Goal and implementing RecordEvent() and Serialize().
   - These creative features are implemented in GoalManager.cs and noted here to document how the project exceeds core requirements.
*/


class Program
{
    static void Main(string[] args)
    {
        var manager = new GoalManager();
        manager.LoadFromFile("goals.txt"); // try to load previous work (non-fatal)
        bool quit = false;
        while (!quit)
        {
            Console.WriteLine("\nEternal Quest — Main Menu");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Show Score and Badges");
            Console.WriteLine("5. Save");
            Console.WriteLine("6. Load");
            Console.WriteLine("7. Quit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    manager.CreateGoalFromInput();
                    break;
                case "2":
                    manager.ListGoals();
                    break;
                case "3":
                    manager.RecordEventInteractive();
                    break;
                case "4":
                    manager.ShowScoreAndBadges();
                    break;
                case "5":
                    manager.SaveToFile("goals.txt");
                    break;
                case "6":
                    manager.LoadFromFile("goals.txt");
                    break;
                case "7":
                    quit = true;
                    manager.SaveToFile("goals.txt");
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
        Console.WriteLine("Goodbye — keep progressing on your Eternal Quest!");
    }
}
