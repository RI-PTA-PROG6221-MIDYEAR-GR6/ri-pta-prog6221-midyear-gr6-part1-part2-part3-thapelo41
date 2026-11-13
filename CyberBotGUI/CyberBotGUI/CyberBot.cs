using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberBotGUI
{
    public class CyberBot
    {
        private Dictionary<string, List<string>> _keywordResponses = new();
        private Random _random = new Random();
        private string _userName = "";
        private string _favouriteTopic = "";
        private string _lastTopic = "";
        private List<string> _worriedKeywords = new List<string> { "worried", "scared", "anxious", "nervous", "afraid" };
        private List<string> _frustratedKeywords = new List<string> { "frustrated", "confused", "stuck", "annoyed", "overwhelmed" };

        // --- PART 3 ADDITIONS ---
        private DatabaseHelper _dbHelper = new DatabaseHelper();
        private List<string> _activityLog = new List<string>();
        private bool _isWaitingForTaskReminder = false;
        private string _pendingTaskTitle = "";

        public CyberBot()
        {
            InitializeResponses();
        }

        private void InitializeResponses()
        {
            _keywordResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", new List<string> { "Make sure to use strong, unique passwords.", "Consider using a password manager.", "Enable Two-Factor Authentication (2FA)." }},
                { "scam", new List<string> { "Be cautious of emails asking for personal information.", "If an offer sounds too good to be true, it probably is a scam." }},
                { "privacy", new List<string> { "Review your social media privacy settings regularly.", "Use a VPN when on public Wi-Fi." }},
                { "phishing", new List<string> { "Check the sender's email address carefully.", "Never click on links in unsolicited emails." }},
                { "malware", new List<string> { "Keep your antivirus software updated.", "Avoid downloading software from untrusted websites." }}
            };
        }

        public string GetResponse(string input, out bool isSentiment)
        {
            isSentiment = false;
            string lowerInput = input.ToLower().Trim();

            // --- PART 3: NLP & TASK ASSISTANT ---
            if (_isWaitingForTaskReminder)
            {
                _isWaitingForTaskReminder = false;
                if (lowerInput.Contains("yes") || lowerInput.Contains("sure") || lowerInput.Contains("ok"))
                {
                    DateTime reminderDate = DateTime.Now.AddDays(3);
                    _dbHelper.AddTask(_pendingTaskTitle, "User generated task", reminderDate);
                    LogActivity($"Task added: '{_pendingTaskTitle}' with reminder.");
                    return $"Got it! I'll remind you in 3 days for: '{_pendingTaskTitle}'.";
                }
                else
                {
                    _dbHelper.AddTask(_pendingTaskTitle, "User generated task", null);
                    LogActivity($"Task added: '{_pendingTaskTitle}' (No reminder)");
                    return $"Okay, I added '{_pendingTaskTitle}' without a reminder.";
                }
            }

            if (lowerInput.Contains("add task") || lowerInput.Contains("remind me to"))
            {
                string taskTitle = input.Replace("add task", "").Replace("remind me to", "").Trim().TrimEnd('.');
                if (string.IsNullOrEmpty(taskTitle)) return "What task would you like to add?";

                _pendingTaskTitle = taskTitle;
                _isWaitingForTaskReminder = true;
                LogActivity($"Action triggered: Add task '{taskTitle}'");
                return $"Task added with the description \"{taskTitle}\". Would you like a reminder? (Yes/No)";
            }

            if (lowerInput.Contains("show activity log") || lowerInput.Contains("what have you done"))
            {
                if (_activityLog.Count == 0) return "I haven't done anything yet!";
                string logResponse = "Here's a summary of recent actions:\n";
                var recentLogs = _activityLog.Skip(Math.Max(0, _activityLog.Count - 5)).ToList();
                for (int i = 0; i < recentLogs.Count; i++) logResponse += $"{i + 1}. {recentLogs[i]}\n";
                return logResponse;
            }

            if (lowerInput.Contains("quiz") || lowerInput.Contains("game"))
            {
                LogActivity("Quiz started");
                return "QUIZ_START";
            }

            // --- PART 2 LOGIC ---
            if (_worriedKeywords.Any(k => lowerInput.Contains(k)))
            {
                isSentiment = true;
                string topic = string.IsNullOrEmpty(_lastTopic) ? "scam" : _lastTopic;
                return $"It's completely understandable to feel that way. Let me share a tip: {GetRandomResponse(topic)}";
            }
            if (_frustratedKeywords.Any(k => lowerInput.Contains(k)))
            {
                isSentiment = true;
                string topic = string.IsNullOrEmpty(_lastTopic) ? "password" : _lastTopic;
                return $"I understand this can be frustrating. Let's break it down simply: {GetRandomResponse(topic)}";
            }
            if (lowerInput.Contains("my name is"))
            {
                _userName = input.Substring(lowerInput.IndexOf("my name is") + 10).Trim();
                return $"Nice to meet you, {_userName}!";
            }
            if (lowerInput.Contains("interested in"))
            {
                int index = lowerInput.IndexOf("interested in") + 13;
                _favouriteTopic = input.Substring(index).Trim().TrimEnd('.');
                return $"Great! I'll remember that you're interested in {_favouriteTopic}.";
            }
            if (lowerInput.Contains("another tip") || lowerInput.Contains("tell me more") || lowerInput.Contains("more"))
            {
                if (!string.IsNullOrEmpty(_lastTopic) && _keywordResponses.ContainsKey(_lastTopic))
                    return $"Here is another tip on {_lastTopic}: {GetRandomResponse(_lastTopic)}";
                return "Sure! What topic would you like another tip on?";
            }
            foreach (var keyword in _keywordResponses.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    _lastTopic = keyword;
                    string response = GetRandomResponse(keyword);
                    if (!string.IsNullOrEmpty(_userName)) response = $"{_userName}, " + response;
                    return response;
                }
            }
            return "I'm not sure I understand. Can you try rephrasing?";
        }

        private string GetRandomResponse(string topic)
        {
            if (_keywordResponses.ContainsKey(topic))
            {
                var responses = _keywordResponses[topic];
                return responses[_random.Next(responses.Count)];
            }
            return "I don't have information on that topic yet.";
        }

        private void LogActivity(string action)
        {
            _activityLog.Add($"{DateTime.Now:HH:mm} - {action}");
        }
    }
}