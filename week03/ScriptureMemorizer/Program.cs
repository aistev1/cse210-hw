using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // EXCEEDS REQUIREMENTS:
        // - Includes a library of scriptures (random selection each run)
        // - User can choose difficulty level (how many words to hide per round)

        List<Scripture> scriptureLibrary = new List<Scripture>
        {
            new Scripture(new Reference("John", 3, 16),
                "For God so loved the world that he gave his one and only Son, " +
                "that whoever believes in him shall not perish but have eternal life."),
            
            new Scripture(new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all your heart and lean not on your own understanding; " +
                "in all your ways submit to him, and he will make your paths straight."),

            new Scripture(new Reference("Psalm", 23, 1, 2),
                "The Lord is my shepherd, I lack nothing. He makes me lie down in green pastures, " +
                "he leads me beside quiet waters.")
        };

        Random rand = new Random();
        Scripture scripture = scriptureLibrary[rand.Next(scriptureLibrary.Count)];

        Console.WriteLine("Choose difficulty (words hidden per round): easy(1), medium(3), hard(5)");
        int wordsToHide = 3;
        string choice = Console.ReadLine()?.ToLower();

        if (choice == "easy") wordsToHide = 1;
        else if (choice == "hard") wordsToHide = 5;

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input?.ToLower() == "quit" || scripture.IsCompletelyHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                break;
            }

            scripture.HideRandomWords(wordsToHide);
        }
    }
}
