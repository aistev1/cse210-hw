using System;
using System.Threading;

namespace MindfulnessProgram
{
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
}