using System;
using System.Threading;

namespace CybersecurityBot
{
    public class ChatbotEngine
    {
        private readonly AudioService _audioService;
        private readonly DisplayService _displayService;
        private readonly ResponseManager _responseManager;
        private string _userName;

        public ChatbotEngine()
        {
            _audioService = new AudioService();
            _displayService = new DisplayService();
            _responseManager = new ResponseManager();
        }

        public void Start()
        {
            // Play voice greeting - THIS PLAYS greetings.wav
            _audioService.PlayWelcomeGreeting();

            // Display ASCII logo
            _displayService.ShowAsciiLogo();
            Console.WriteLine();

            // Get user name
            GetUserName();

            // Show welcome message
            _displayService.ShowWelcomeMessage(_userName);

            // Start main conversation loop
            RunConversationLoop();
        }

        private void GetUserName()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("What's your name? ");
            Console.ResetColor();

            string input = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("I didn't catch that. Please enter your name:");
                Console.ResetColor();
                input = Console.ReadLine();
            }

            _userName = input.Trim();

            // Clear the input line and show a personalized greeting
            Console.Clear();
            _displayService.ShowAsciiLogo();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Welcome, {_userName}! Let's get started! 🚀");
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        private void RunConversationLoop()
        {
            bool running = true;

            while (running)
            {
                _displayService.ShowDivider();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{_userName} > ");
                Console.ResetColor();

                string userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("I didn't quite understand that. Could you rephrase?");
                    Console.ResetColor();
                    continue;
                }

                // Check for exit commands
                if (_responseManager.IsExitCommand(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nGoodbye, {_userName}! Stay safe online! 🛡️");
                    Console.ResetColor();
                    running = false;
                    continue;
                }

                // Check for help command
                if (_responseManager.IsHelpCommand(userInput))
                {
                    _displayService.ShowHelpMenu();
                    continue;
                }

                // Check if it's a greeting
                if (_responseManager.IsGreeting(userInput))
                {
                    _displayService.ShowBotResponse(_responseManager.GetGreetingResponse(_userName));
                    continue;
                }

                // Get response
                string response = _responseManager.GetResponse(userInput);

                if (response != null)
                {
                    _displayService.ShowBotResponse(response);
                }
                else
                {
                    _displayService.ShowBotResponse(_responseManager.GetDefaultResponse());
                }

                // Small delay for natural conversation flow
                Thread.Sleep(500);
            }
        }
    }
}