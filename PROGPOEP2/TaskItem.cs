using System;

namespace PROGPOEP2
{
    public class TaskItem
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? ReminderDate { get; set; }   // nullable - reminder is optional
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            string status = IsCompleted ? "✅" : "⬜";
            string reminder = ReminderDate.HasValue
                ? $" (Reminder: {ReminderDate.Value:dd MMM yyyy})"
                : "";
            return $"{status} {Title}{reminder}";
        }
    }
}