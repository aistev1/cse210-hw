using System;

namespace MindfulnessProgram
{
    public class ActivityLogEntry
    {
        public string ActivityName { get; set; } = "";
        public DateTime Timestamp { get; set; }
        public int DurationSeconds { get; set; }
        public int ItemsCount { get; set; }
    }
}