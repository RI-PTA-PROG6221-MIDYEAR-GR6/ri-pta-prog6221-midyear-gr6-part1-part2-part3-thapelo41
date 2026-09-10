using System;
using System.Collections.Generic;

namespace CybersecurityBot
{
    public class ResponseManager
    {
        private readonly Dictionary<string, string> _responses;

        public ResponseManager()
        {
            _responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "how are you", "I'm doing great! Thanks for asking. I'm always here and ready to help you stay safe online! 😊" },
                { "purpose", "My purpose is to educate and protect you from online threats. I provide tips and information about cybersecurity to help you browse safely!" },
                { "help", "You can ask me about password safety, phishing, safe browsing, or general cybersecurity tips. Type 'help' to see all available commands." },
                { "password", "Password Safety Tips:\n• Use a mix of uppercase, lowercase, numbers, and special characters\n• Make passwords at least 12 characters long\n• Don't reuse passwords across different sites\n• Use a password manager like Bitwarden or LastPass\n• Enable 2FA (Two-Factor Authentication) whenever possible" },
                { "phishing", "Phishing Protection Tips:\n• Never click on suspicious links in emails or messages\n• Verify the sender's email address carefully\n• Check for spelling and grammar errors in messages\n• Hover over links to see the actual URL\n• When in doubt, contact the organization directly through official channels" },
                { "browsing", "Safe Browsing Tips:\n• Keep your browser and extensions updated\n• Use HTTPS websites (look for the padlock icon)\n• Avoid clicking on pop-up ads\n• Use a reputable ad blocker\n• Clear your cookies and cache regularly\n• Use incognito mode for sensitive browsing" },
                { "tips", "General Cybersecurity Tips:\n• Keep all software and operating systems updated\n• Use antivirus software and run regular scans\n• Backup your important data regularly\n• Be cautious of what you share on social media\n• Use strong, unique passwords for each account\n• Enable 2FA wherever possible\n• Be aware of social engineering tactics" }
            };
        }

        public string GetResponse(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return null;
            }

            string input = userInput.Trim().ToLower();

            // Check for exact matches
            foreach (var key in _responses.Keys)
            {
                if (input.Contains(key) || key.Contains(input))
                {
                    return _responses[key];
                }
            }

            return null;
        }

        public string GetDefaultResponse()
        {
            string[] defaultResponses = {
                "I didn't quite understand that. Could you rephrase? 🤔",
                "I'm not sure I understand. Can you ask in a different way?",
                "That's outside my knowledge base. Try asking about password safety, phishing, or safe browsing!",
                "Hmm, I'm not programmed to answer that. Feel free to ask me about cybersecurity topics!"
            };

            Random random = new Random();
            return defaultResponses[random.Next(defaultResponses.Length)];
        }

        public bool IsExitCommand(string input)
        {
            string[] exitCommands = { "quit", "exit", "bye", "goodbye" };
            return Array.Exists(exitCommands, cmd => cmd.Equals(input?.Trim().ToLower()));
        }

        public bool IsHelpCommand(string input)
        {
            return input?.Trim().ToLower() == "help";
        }

        public bool IsGreeting(string input)
        {
            string[] greetings = { "hello", "hi", "hey", "greetings", "good morning", "good afternoon" };
            return Array.Exists(greetings, greet => input?.Trim().ToLower().Contains(greet) == true);
        }

        public string GetGreetingResponse(string userName)
        {
            string[] greetings = {
                $"Hello again, {userName}! How can I help you stay safe today? 😊",
                $"Hi {userName}! Ready to learn about cybersecurity?",
                $"Hey {userName}! Great to see you again!"
            };

            Random random = new Random();
            return greetings[random.Next(greetings.Length)];
        }
    }
}
