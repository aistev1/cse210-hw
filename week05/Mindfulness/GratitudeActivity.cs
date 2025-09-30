using System;
using System.Collections.Generic;

namespace MindfulnessProgram
{
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
}