using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace MindfulnessProgram
{
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
                if (data != null) _entries.AddRange(data);
            }
            catch { }
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
            catch { }
        }
    }
}