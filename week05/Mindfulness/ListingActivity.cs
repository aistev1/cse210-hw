using System;
using System.Collections.Generic;
using System.Linq;

namespace MindfulnessProgram
{
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
}