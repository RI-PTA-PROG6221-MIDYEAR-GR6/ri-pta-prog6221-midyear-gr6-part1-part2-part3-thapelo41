using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberBotGUI
{
    /// <summary>
    /// Core logic class for the Cybersecurity Chatbot.
    /// Handles keyword recognition, random responses, memory, and sentiment detection.
    /// </summary>
    public class CyberBot
    {
        // Requirement: Use generic collections (Dictionary and List) for optimisation
        private Dictionary<string, List<string>> _keywordResponses;
        private Random _random = new Random();

        // Requirement: Memory and Recall variables
        private string _userName = "";
        private string _favouriteTopic = "";
        private string _lastTopic = "";

        // Requirement: Sentiment detection keywords
        private List<string> _worriedKeywords = new List<string> { "worried", "scared", "anxious", "nervous", "afraid" };
        private List<string> _frustratedKeywords = new List<string> { "frustrated", "confused", "stuck", "annoyed", "overwhelmed" };

        public CyberBot()
        {
            InitializeResponses();
        }

        /// <summary>
        /// Populates the dictionary with cybersecurity topics and their varied responses.
        /// </summary>
        private void InitializeResponses()
        {
            _keywordResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", new List<string> {
                    "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords.",
                    "Consider using a password manager to keep track of complex passwords securely.",
                    "Enable Two-Factor Authentication (2FA) wherever possible for an extra layer of security."
                }},
                { "scam", new List<string> {
                    "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                    "If an offer sounds too good to be true, it probably is a scam. Always verify the source.",
                    "Never send money or gift cards to someone you haven't met in person."
                }},
                { "privacy", new List<string> {
                    "Review your social media privacy settings regularly to control who sees your data.",
                    "Use a VPN when on public Wi-Fi to protect your privacy and data from interceptors.",
                    "Be mindful of the permissions you grant to mobile apps."
                }},
                { "phishing", new List<string> {
                    "Check the sender's email address carefully. Phishing emails often have slight misspellings in the domain name.",
                    "Never click on links in unsolicited emails. Hover over them first to see the actual URL.",
                    "Legitimate companies will never ask for your password via email."
                }},
                { "malware", new List<string> {
                    "Keep your antivirus software updated to protect against the latest malware threats.",
                    "Avoid downloading software from untrusted websites or clicking on pop-up ads."
                }}
            };
        }

        /// <summary>
        /// Main entry point for processing user input and generating a response.
        /// </summary>
        public string GetResponse(string input, out bool isSentiment)
        {
            isSentiment = false;
            string lowerInput = input.ToLower().Trim();

            // 1. Sentiment Detection (Higher priority to adjust tone immediately)
            if (_worriedKeywords.Any(k => lowerInput.Contains(k)))
            {
                isSentiment = true;
                string topic = string.IsNullOrEmpty(_lastTopic) ? "scam" : _lastTopic;
                return $"It's completely understandable to feel that way. Scammers can be very convincing. Let me share a tip to help you stay safe: {GetRandomResponse(topic)}";
            }
            if (_frustratedKeywords.Any(k => lowerInput.Contains(k)))
            {
                isSentiment = true;
                string topic = string.IsNullOrEmpty(_lastTopic) ? "password" : _lastTopic;
                return $"I understand this can be frustrating. Cybersecurity is complex. Let's break it down simply: {GetRandomResponse(topic)}";
            }

            // 2. Memory and Recall (Storing user details)
            if (lowerInput.Contains("my name is"))
            {
                _userName = input.Substring(lowerInput.IndexOf("my name is") + 10).Trim();
                return $"Nice to meet you, {_userName}! How can I help you stay safe online today?";
            }
            if (lowerInput.Contains("interested in"))
            {
                int index = lowerInput.IndexOf("interested in") + 13;
                _favouriteTopic = input.Substring(index).Trim().TrimEnd('.');
                return $"Great! I'll remember that you're interested in {_favouriteTopic}. It's a crucial part of staying safe online.";
            }

            // 3. Conversation Flow (Handling follow-ups)
            if (lowerInput.Contains("another tip") || lowerInput.Contains("tell me more") || lowerInput.Contains("more"))
            {
                if (!string.IsNullOrEmpty(_lastTopic) && _keywordResponses.ContainsKey(_lastTopic))
                {
                    return $"Here is another tip on {_lastTopic}: {GetRandomResponse(_lastTopic)}";
                }
                return "Sure! What topic would you like another tip on? You can ask about passwords, scams, privacy, or phishing.";
            }

            // 4. Keyword Recognition
            foreach (var keyword in _keywordResponses.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    _lastTopic = keyword; // Remember this for follow-up questions
                    string response = GetRandomResponse(keyword);

                    // Personalise based on memory
                    if (!string.IsNullOrEmpty(_userName))
                    {
                        response = $"{_userName}, " + response;
                    }
                    if (!string.IsNullOrEmpty(_favouriteTopic) && _favouriteTopic.Contains(keyword))
                    {
                        response += $" As someone interested in {_favouriteTopic}, you might want to review your security settings.";
                    }
                    return response;
                }
            }

            // 5. Error Handling / Default Response
            return "I'm not sure I understand. Can you try rephrasing? You can ask me about passwords, scams, privacy, phishing, or malware.";
        }

        /// <summary>
        /// Selects a random response from the list of responses for a given topic.
        /// </summary>
        private string GetRandomResponse(string topic)
        {
            if (_keywordResponses.ContainsKey(topic))
            {
                var responses = _keywordResponses[topic];
                return responses[_random.Next(responses.Count)];
            }
            return "I don't have information on that topic yet.";
        }
    }
}