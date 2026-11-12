using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace CyberBotGUI
{
    public class DatabaseHelper
    {
        // ⚠️ UPDATE THIS STRING WITH YOUR MySQL PASSWORD
        private string connectionString = "server=localhost;user=root;password=root;database=cyberbot_db;";

        public void AddTask(string title, string description, DateTime? reminderDate)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Tasks (Title, Description, ReminderDate) VALUES (@title, @desc, @date)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@desc", description);
                    cmd.Parameters.AddWithValue("@date", reminderDate.HasValue ? reminderDate.Value : (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<string> GetTasks()
        {
            List<string> tasks = new List<string>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT TaskID, Title, ReminderDate, IsCompleted FROM Tasks";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string status = reader.GetBoolean("IsCompleted") ? "[✔] " : "[ ] ";
                        string date = reader.IsDBNull(reader.GetOrdinal("ReminderDate")) ? "No reminder" : reader.GetDateTime("ReminderDate").ToString("yyyy-MM-dd");
                        tasks.Add($"{status}ID: {reader.GetInt32("TaskID")} | {reader.GetString("Title")} (Reminder: {date})");
                    }
                }
            }
            return tasks;
        }

        public void MarkTaskCompleted(int taskId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Tasks SET IsCompleted = TRUE WHERE TaskID = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", taskId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTask(int taskId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Tasks WHERE TaskID = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", taskId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}