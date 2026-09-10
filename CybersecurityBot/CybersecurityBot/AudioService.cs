using System;
using System.Media;
using System.IO;
using System.Threading;

namespace CybersecurityBot
{
    public class AudioService
    {
        private readonly string _audioFilePath;

        public AudioService()
        {
            // Get the base directory where the executable is running
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectDirectory = Directory.GetParent(baseDirectory)?.Parent?.Parent?.Parent?.FullName ?? Directory.GetCurrentDirectory();

            // Try multiple possible locations
            string[] possiblePaths = {
                // Current output directory (bin/Debug/net9.0/)
                Path.Combine(baseDirectory, "greetings.wav"),
                
                // Project root directory
                Path.Combine(projectDirectory, "greetings.wav"),
                
                // Current working directory
                Path.Combine(Directory.GetCurrentDirectory(), "greetings.wav"),
                
                // Resources folder in output
                Path.Combine(baseDirectory, "Resources", "greetings.wav"),
                
                // Resources folder in project
                Path.Combine(projectDirectory, "Resources", "greetings.wav"),
                
                // One level up from output
                Path.Combine(Directory.GetParent(baseDirectory)?.FullName ?? "", "greetings.wav")
            };

            _audioFilePath = null;

            // Search for the file
            foreach (string path in possiblePaths)
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    _audioFilePath = path;
                    Console.WriteLine($"✅ Found audio file at: {_audioFilePath}");
                    break;
                }
            }

            // If still not found, use the expected path
            if (string.IsNullOrEmpty(_audioFilePath))
            {
                _audioFilePath = Path.Combine(baseDirectory, "greetings.wav");
                Console.WriteLine($"⚠️  Audio file not found. Expected at: {_audioFilePath}");
            }
        }

        public void PlayWelcomeGreeting()
        {
            try
            {
                if (File.Exists(_audioFilePath))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n🔊 Playing welcome message...");
                    Console.ResetColor();

                    using (SoundPlayer player = new SoundPlayer(_audioFilePath))
                    {
                        // Load the audio file
                        player.Load();
                        // Play synchronously (waits for completion)
                        player.PlaySync();
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✅ Welcome message played successfully!");
                    Console.ResetColor();
                    Thread.Sleep(500);
                    Console.Clear();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n⚠️  Audio file not found at: {_audioFilePath}");
                    Console.WriteLine("📝 Continuing in text mode...");
                    Console.ResetColor();
                    Thread.Sleep(1500);
                    Console.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ Error playing audio: {ex.Message}");
                Console.WriteLine("📝 Continuing in text mode...");
                Console.ResetColor();
                Thread.Sleep(1500);
                Console.Clear();
            }
        }

        public bool IsAudioFilePresent()
        {
            return File.Exists(_audioFilePath);
        }

        public string GetAudioFilePath()
        {
            return _audioFilePath;
        }

        // Play audio in background (doesn't block)
        public void PlayWelcomeGreetingAsync()
        {
            try
            {
                if (File.Exists(_audioFilePath))
                {
                    using (SoundPlayer player = new SoundPlayer(_audioFilePath))
                    {
                        player.Load();
                        player.Play(); // Non-blocking
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing audio: {ex.Message}");
            }
        }
    }
}
