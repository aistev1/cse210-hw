
// - Added both current DATE and TIME to each journal entry.
// - Allowed the user to add their OWN custom prompt, in addition to random ones.
// - Saved entries using a "|" separator so the file is structured and reloadable.
// - Organized code with separate classes (Entry, Journal, PromptGenerator).


using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGen = new PromptGenerator();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write (with random prompt)");
            Console.WriteLine("2. Write (with my own prompt)");
            Console.WriteLine("3. Display");
            Console.WriteLine("4. Load");
            Console.WriteLine("5. Save");
            Console.WriteLine("6. Quit");
            Console.Write("What would you like to do? ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = promptGen.GetRandomPrompt();
                    Console.WriteLine(prompt);
                    string response = Console.ReadLine();
                    string date1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    Entry randomEntry = new Entry(date1, prompt, response);
                    journal.AddEntry(randomEntry);
                    break;

                case "2":
                    Console.Write("Enter your custom prompt: ");
                    string customPrompt = Console.ReadLine();
                    Console.WriteLine(customPrompt);
                    string customResponse = Console.ReadLine();
                    string date2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    Entry customEntry = new Entry(date2, customPrompt, customResponse);
                    journal.AddEntry(customEntry);
                    break;

                case "3":
                    journal.DisplayAll();
                    break;

                case "4":
                    Console.Write("Enter filename to load: ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    break;

                case "5":
                    Console.Write("Enter filename to save: ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    break;

                case "6":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
            Console.WriteLine();
        }
    }
}
