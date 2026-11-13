using System;
using System.Threading;

namespace CybersecurityBot
{
    public class DisplayService
    {
        private readonly Random _random = new Random();

        public void ShowAsciiLogo()
        {
            // Green for the shield/emblem part
            Console.ForegroundColor = ConsoleColor.Green;
            string logoTop = @"
   ██████╗██╗   ██╗██████╗ ███████╗██████╗ 
  ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗
  ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝
  ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗
  ╚██████╗   ██║   ██████╔╝███████╗██║  ██║
   ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝";
            Console.WriteLine(logoTop);
            Console.ResetColor();

            // White for the subtitle lines
            Console.ForegroundColor = ConsoleColor.White;
            string logoBottom = @"
        🔐 CYBERSECURITY AWARENESS BOT 🔐
           Your Digital Safety Companion
";
            Console.WriteLine(logoBottom);
            Console.ResetColor();
        }

        public void TypeWriterEffect(string text, int delay = 30)
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        public void ShowWelcomeMessage(string userName)
        {
            Console.Clear();
            ShowAsciiLogo();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            TypeWriterEffect($"Hello, {userName}! 👋 Welcome to the Cybersecurity Awareness Bot!", 25);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.ResetColor();

            Console.WriteLine();
            TypeWriterEffect("I'm here to help you stay safe online. Feel free to ask me about:", 20);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  • Password Safety 🔑?");
            Console.WriteLine("  • Phishing Attacks 🎣?");
            Console.WriteLine("  • Safe Browsing 🌐?");
            Console.WriteLine("  • General Cybersecurity Tips 🛡️?");
            Console.ResetColor();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Type 'help' to see all available commands, or 'quit' to exit.");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void ShowDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("───────────────────────────────────────────────────────────────");
            Console.ResetColor();
        }

        public void ShowSectionHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n▶ {title}");
            Console.ResetColor();
            ShowDivider();
        }

        public void ShowBotResponse(string response)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[BOT] ");
            Console.ResetColor();
            TypeWriterEffect(response, 20);
        }

        public void ShowUserInput(string input)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[YOU] ");
            Console.ResetColor();
            Console.WriteLine(input);
        }

        public void ShowHelpMenu()
        {
            ShowSectionHeader("AVAILABLE COMMANDS");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  • help           - Show this help menu?");
            Console.WriteLine("  • quit/exit      - Exit the chatbot?");
            Console.WriteLine("  • password       - Get password safety tips?");
            Console.WriteLine("  • phishing       - Learn about phishing attacks?");
            Console.WriteLine("  • browsing       - Safe browsing tips?");
            Console.WriteLine("  • purpose        - What's my purpose?");
            Console.WriteLine("  • how are you    - Ask how I'm doing?");
            Console.WriteLine("  • tips           - General security tips?");
            Console.ResetColor();
        }

        public void SimulateTyping(int duration = 500)
        {
            Console.Write("...");
            Thread.Sleep(duration);
            Console.Write("\b\b\b   \b\b\b");
        }
    }
}