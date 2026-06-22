using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PROGPOEP2
{
    // Sits between ChatbotEngine/GUI and TaskRepository.
    // Handles business rules (like parsing "in 3 days" into a real date)
    // so neither the chat logic nor the DB layer needs to know about that.
    public class TaskManager
    {
        private readonly TaskRepository repository;

        public TaskManager()
        {
            repository = new TaskRepository();
        }

        public TaskItem AddTask(string title, string description)
        {
            int newId = repository.AddTask(title, description, null);
            return new TaskItem
            {
                TaskId = newId,
                Title = title,
                Description = description,
                ReminderDate = null,
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };
        }

        public bool SetReminder(int taskId, string reminderPhrase, out DateTime resolvedDate)
        {
            if (!TryParseReminderPhrase(reminderPhrase, out resolvedDate))
                return false;

            repository.SetReminder(taskId, resolvedDate);
            return true;
        }

        // Turns natural phrases like "in 3 days", "tomorrow", "next week"
        // or a literal date string into a concrete DateTime.
        public bool TryParseReminderPhrase(string phrase, out DateTime result)
        {
            phrase = phrase.ToLower().Trim();
            result = default;

            if (phrase.Contains("tomorrow"))
            {
                result = DateTime.Now.Date.AddDays(1);
                return true;
            }
            if (phrase.Contains("next week"))
            {
                result = DateTime.Now.Date.AddDays(7);
                return true;
            }
            if (phrase.Contains("today"))
            {
                result = DateTime.Now.Date;
                return true;
            }

            // Matches "in 3 days", "3 days", "2 weeks", etc.
            var match = Regex.Match(phrase, @"(\d+)\s*(day|week)s?");
            if (match.Success)
            {
                int amount = int.Parse(match.Groups[1].Value);
                string unit = match.Groups[2].Value;
                result = unit == "week"
                    ? DateTime.Now.Date.AddDays(amount * 7)
                    : DateTime.Now.Date.AddDays(amount);
                return true;
            }

            // Fallback: a literal date like "15 July" or "2026-07-15"
            if (DateTime.TryParse(phrase, out DateTime parsedDate))
            {
                result = parsedDate;
                return true;
            }

            return false;
        }

        public List<TaskItem> GetAllTasks() => repository.GetAllTasks();

        public bool CompleteTaskByTitle(string titleFragment, out TaskItem? completedTask)
        {
            completedTask = repository.GetAllTasks()
                .FirstOrDefault(t => t.Title.ToLower().Contains(titleFragment.ToLower()) && !t.IsCompleted);

            if (completedTask == null) return false;
            repository.MarkCompleted(completedTask.TaskId);
            return true;
        }

        public bool DeleteTaskByTitle(string titleFragment, out TaskItem? deletedTask)
        {
            deletedTask = repository.GetAllTasks()
                .FirstOrDefault(t => t.Title.ToLower().Contains(titleFragment.ToLower()));

            if (deletedTask == null) return false;
            repository.DeleteTask(deletedTask.TaskId);
            return true;
        }

        public string FormatTaskList()
        {
            var tasks = GetAllTasks();
            if (tasks.Count == 0)
                return "You don't have any tasks yet. Try \"Add task - Enable two-factor authentication\".";

            var sb = new StringBuilder();
            sb.AppendLine("📋 Here are your tasks:\n");
            foreach (var task in tasks)
            {
                sb.AppendLine(task.ToString());
                if (!string.IsNullOrEmpty(task.Description))
                    sb.AppendLine($"    {task.Description}");
            }
            return sb.ToString();
        }
    }
}