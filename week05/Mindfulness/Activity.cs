using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessProgram
{
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
                if (int.TryParse(input, out int seconds) && seconds > 0) return seconds;
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
            Console.Write("\r \r");
        }

        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"\r{i}... ");
                Thread.Sleep(1000);
            }
            Console.Write("\r \r");
        }

        protected string GetRandomPrompt(List<string> prompts, List<string> usedPrompts)
        {
            if (usedPrompts.Count >= prompts.Count) usedPrompts.Clear();
            var available = prompts.Where(p => !usedPrompts.Contains(p)).ToList();
            string selected = available[_rng.Next(available.Count)];
            usedPrompts.Add(selected);
            return selected;
        }
    }
}