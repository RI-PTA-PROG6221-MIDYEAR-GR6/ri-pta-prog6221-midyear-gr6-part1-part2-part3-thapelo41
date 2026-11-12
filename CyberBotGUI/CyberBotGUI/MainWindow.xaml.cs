using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

namespace CyberBotGUI
{
    public partial class MainWindow : Window
    {
        private CyberBot _bot = new CyberBot();
        private DatabaseHelper _dbHelper = new DatabaseHelper();

        public class TaskItem
        {
            public int Id { get; set; }
            public string DisplayText { get; set; } = "";
        }

        public MainWindow()
        {
            InitializeComponent();
            PlayVoiceGreeting();
            DisplayAsciiArt();
            AddMessage("Bot: Hello! I am your Cybersecurity Assistant. How can I help you today?");
            LoadDatabaseTasks();
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                if (File.Exists(audioPath))
                {
                    SoundPlayer player = new SoundPlayer(audioPath);
                    player.Play();
                }
            }
            catch { }
        }

        private void DisplayAsciiArt()
        {
            AsciiArtHeader.Text = @"
   ______      __              __ 
  / ____/_  __/ /_  ___  _____/ /_
 / /   / / / / __ \/ _ \/ ___/ __/
/ /___/ /_/ / /_/ /  __/ /  / /_  
\____/\__, /_.___/\___/_/   \__/  
     /____/                       
  Cybersecurity Awareness Bot";
        }

        private void SendButton_Click(object sender, RoutedEventArgs e) { ProcessUserInput(); }
        private void UserInput_KeyDown(object sender, KeyEventArgs e) { if (e.Key == Key.Enter) ProcessUserInput(); }

        private void ProcessUserInput()
        {
            string userText = UserInput.Text.Trim();
            if (string.IsNullOrEmpty(userText)) return;

            AddMessage("You: " + userText);
            UserInput.Clear();

            Func<string, string> responseDelegate = (input) => { bool s; return _bot.GetResponse(input, out s); };
            string botResponse = responseDelegate(userText);

            if (botResponse == "QUIZ_START")
            {
                AddMessage("Bot: Opening the Cybersecurity Quiz...");
                QuizWindow quiz = new QuizWindow();
                quiz.ShowDialog();
                return;
            }

            System.Threading.Tasks.Task.Delay(400).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() => {
                    AddMessage("Bot: " + botResponse);
                    if (botResponse.Contains("Task added") || botResponse.Contains("Got it!") || botResponse.Contains("Okay, I added"))
                        LoadDatabaseTasks();
                });
            });
        }

        private void AddMessage(string message)
        {
            ChatHistory.Items.Add(message);
            if (ChatHistory.Items.Count > 0) ChatHistory.ScrollIntoView(ChatHistory.Items[ChatHistory.Items.Count - 1]);
        }

        // --- TASK MANAGER METHODS ---
        private void LoadDatabaseTasks()
        {
            try
            {
                var rawTasks = _dbHelper.GetTasks();
                var formattedTasks = new List<TaskItem>();
                foreach (var t in rawTasks)
                {
                    int idStart = t.IndexOf("ID: ") + 4;
                    int idEnd = t.IndexOf(" |");
                    int id = int.Parse(t.Substring(idStart, idEnd - idStart).Trim());
                    string displayText = t.Substring(idEnd + 3).Trim();
                    formattedTasks.Add(new TaskItem { Id = id, DisplayText = displayText });
                }
                TaskListBox.ItemsSource = formattedTasks;
            }
            catch { AddMessage("Bot: Could not connect to database. Please check MySQL."); }
        }

        private void RefreshTasks_Click(object sender, RoutedEventArgs e) { LoadDatabaseTasks(); AddMessage("Bot: Task list refreshed."); }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem is TaskItem selectedTask)
            {
                _dbHelper.MarkTaskCompleted(selectedTask.Id);
                LoadDatabaseTasks();
            }
            else AddMessage("Bot: Select a task first.");
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem is TaskItem selectedTask)
            {
                _dbHelper.DeleteTask(selectedTask.Id);
                LoadDatabaseTasks();
            }
            else AddMessage("Bot: Select a task first.");
        }
    }
}