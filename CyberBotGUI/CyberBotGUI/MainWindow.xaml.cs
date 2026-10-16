using System;
using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Input;

namespace CyberBotGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Handles UI events and delegates communication with the CyberBot class.
    /// </summary>
    public partial class MainWindow : Window
    {
        private CyberBot _bot = new CyberBot();

        public MainWindow()
        {
            InitializeComponent();

            // Requirement: Translate Part 1 Voice Implementation
            PlayVoiceGreeting();

            // Requirement: Translate Part 1 ASCII Art
            DisplayAsciiArt();

            // Initial Bot Message
            AddMessage("Bot: Hello! I am your Cybersecurity Assistant. How can I help you today?");
        }

        /// <summary>
        /// Plays the voice greeting from Part 1.
        /// </summary>
        private void PlayVoiceGreeting()
        {
            try
            {
                // Ensure 'greeting.wav' is in the project folder and set to 'Copy if newer'
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                if (File.Exists(audioPath))
                {
                    SoundPlayer player = new SoundPlayer(audioPath);
                    player.Play();
                    StatusText.Text = "🔊 Voice Greeting Played";
                }
                else
                {
                    StatusText.Text = "⚠️ Voice file (greeting.wav) not found.";
                }
            }
            catch (Exception ex)
            {
                // Requirement: Error Handling (don't crash if audio fails)
                StatusText.Text = "⚠️ Error playing audio: " + ex.Message;
            }
        }

        /// <summary>
        /// Displays the ASCII art from Part 1 in the GUI header.
        /// </summary>
        private void DisplayAsciiArt()
        {
            // PASTE YOUR PART 1 ASCII ART HERE
            string ascii = @"
   ______      __              __ 
  / ____/_  __/ /_  ___  _____/ /_
 / /   / / / / __ \/ _ \/ ___/ __/
/ /___/ /_/ / /_/ /  __/ /  / /_  
\____/\__, /_.___/\___/_/   \__/  
     /____/                       
  Cybersecurity Awareness Bot
";
            AsciiArtHeader.Text = ascii;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessUserInput();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessUserInput();
            }
        }

        /// <summary>
        /// Processes the user's input, invokes the bot, and updates the UI.
        /// </summary>
        private void ProcessUserInput()
        {
            string userText = UserInput.Text.Trim();
            if (string.IsNullOrEmpty(userText)) return;

            AddMessage("You: " + userText);
            UserInput.Clear();

            // Requirement: Use delegates to solve a programming problem.
            // We define a delegate (Func) that takes a string and returns a string.
            // This decouples the UI from the bot logic.
            Func<string, string> responseDelegate = (input) =>
            {
                bool isSentiment;
                return _bot.GetResponse(input, out isSentiment);
            };

            string botResponse = responseDelegate(userText);

            // Simulate typing delay for a natural feel (Conversation Flow rubric)
            System.Threading.Tasks.Task.Delay(400).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() => AddMessage("Bot: " + botResponse));
            });
        }

        /// <summary>
        /// Adds a message to the chat history and auto-scrolls to the bottom.
        /// </summary>
        private void AddMessage(string message)
        {
            ChatHistory.Items.Add(message);
            if (ChatHistory.Items.Count > 0)
            {
                ChatHistory.ScrollIntoView(ChatHistory.Items[ChatHistory.Items.Count - 1]);
            }
        }
    }
}