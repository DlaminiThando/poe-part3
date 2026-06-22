using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PROGPOEP2
{
    // Stores a running log of significant chatbot actions with timestamps.
    // Uses a List<LogEntry> as the brief specifies list/dictionary storage.
    public class ActivityLogger
    {
        private readonly List<LogEntry> log;
        private const int MaxDisplayCount = 10;

        public ActivityLogger()
        {
            log = new List<LogEntry>();
        }

        public void Log(string action)
        {
            log.Add(new LogEntry
            {
                Timestamp = DateTime.Now,
                Description = action
            });
        }

        public string GetRecentLog(bool showAll = false)
        {
            if (log.Count == 0)
                return "📋 No activity recorded yet. Start chatting, adding tasks, or taking the quiz!";

            var entries = showAll ? log : log.TakeLast(MaxDisplayCount).ToList();

            var sb = new StringBuilder();
            sb.AppendLine("📋 Here's a summary of recent actions:\n");

            int number = 1;
            foreach (var entry in entries)
            {
                sb.AppendLine($"{number}. [{entry.Timestamp:dd MMM yyyy HH:mm}] {entry.Description}");
                number++;
            }

            if (!showAll && log.Count > MaxDisplayCount)
                sb.AppendLine($"\n...and {log.Count - MaxDisplayCount} earlier actions. Say \"show full log\" to see all.");

            return sb.ToString();
        }

        public int TotalActions => log.Count;

        private class LogEntry
        {
            public DateTime Timestamp { get; set; }
            public string Description { get; set; } = "";
        }
    }
}