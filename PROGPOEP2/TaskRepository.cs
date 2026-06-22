using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PROGPOEP2
{
    // Talks directly to MySQL. Nothing in here knows about the chatbot or the GUI -
    // it just does CRUD on the tasks table.
    public class TaskRepository
    {
        // TODO: replace YOUR_PASSWORD with your actual MySQL root password
        private const string ConnectionString =
            "Server=localhost;Database=cybersecuritychatbot;Uid=root;Pwd=@Emeris2026!;";

        public TaskRepository()
        {
            EnsureTableExists();
        }

        private MySqlConnection GetConnection() => new MySqlConnection(ConnectionString);

        private void EnsureTableExists()
        {
            const string sql = @"
                CREATE TABLE IF NOT EXISTS tasks (
                    task_id INT AUTO_INCREMENT PRIMARY KEY,
                    title VARCHAR(255) NOT NULL,
                    description TEXT,
                    reminder_date DATETIME NULL,
                    is_completed BOOLEAN NOT NULL DEFAULT FALSE,
                    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
                );";

            using var conn = GetConnection();
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        public int AddTask(string title, string description, DateTime? reminderDate)
        {
            const string sql = @"
                INSERT INTO tasks (title, description, reminder_date, is_completed)
                VALUES (@title, @description, @reminderDate, FALSE);
                SELECT LAST_INSERT_ID();";

            using var conn = GetConnection();
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@description", description ?? "");
            cmd.Parameters.AddWithValue("@reminderDate", (object?)reminderDate ?? DBNull.Value);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public List<TaskItem> GetAllTasks()
        {
            const string sql = @"
                SELECT task_id, title, description, reminder_date, is_completed, created_at
                FROM tasks ORDER BY created_at DESC;";

            var tasks = new List<TaskItem>();

            using var conn = GetConnection();
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tasks.Add(new TaskItem
                {
                    TaskId = reader.GetInt32("task_id"),
                    Title = reader.GetString("title"),
                    Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                    ReminderDate = reader.IsDBNull(reader.GetOrdinal("reminder_date")) ? null : reader.GetDateTime("reminder_date"),
                    IsCompleted = reader.GetBoolean("is_completed"),
                    CreatedAt = reader.GetDateTime("created_at")
                });
            }

            return tasks;
        }

        public void MarkCompleted(int taskId)
        {
            const string sql = "UPDATE tasks SET is_completed = TRUE WHERE task_id = @id;";
            using var conn = GetConnection();
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", taskId);
            cmd.ExecuteNonQuery();
        }

        public void DeleteTask(int taskId)
        {
            const string sql = "DELETE FROM tasks WHERE task_id = @id;";
            using var conn = GetConnection();
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", taskId);
            cmd.ExecuteNonQuery();
        }

        public void SetReminder(int taskId, DateTime reminderDate)
        {
            const string sql = "UPDATE tasks SET reminder_date = @reminderDate WHERE task_id = @id;";
            using var conn = GetConnection();
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@reminderDate", reminderDate);
            cmd.Parameters.AddWithValue("@id", taskId);
            cmd.ExecuteNonQuery();
        }
    }
}