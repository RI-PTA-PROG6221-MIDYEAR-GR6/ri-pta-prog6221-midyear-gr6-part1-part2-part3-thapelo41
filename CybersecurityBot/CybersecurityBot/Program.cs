using System;

namespace CybersecurityBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot 🔐";
            Console.SetWindowSize(90, 40);

            try
            {
                // Check if audio file exists
                AudioService audioService = new AudioService();
                if (audioService.IsAudioFilePresent())
                {
                    Console.WriteLine($"✅ Audio file found: {audioService.GetAudioFilePath()}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"⚠️  Audio file not found!");
                    Console.WriteLine($"Expected: {audioService.GetAudioFilePath()}");
                    Console.WriteLine("Continuing in text mode...");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }

                ChatbotEngine engine = new ChatbotEngine();
                engine.Start();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}