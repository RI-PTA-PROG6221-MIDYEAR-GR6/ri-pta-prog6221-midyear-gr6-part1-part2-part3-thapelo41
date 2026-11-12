using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CyberBotGUI
{
    public partial class QuizWindow : Window
    {
        public class QuizQuestion
        {
            public string Question { get; set; }
            public string OptionA { get; set; }
            public string OptionB { get; set; }
            public string OptionC { get; set; }
            public string CorrectAnswer { get; set; }
            public string Explanation { get; set; }
        }

        private List<QuizQuestion> _questions;
        private int _currentQuestionIndex = 0;
        private int _score = 0;

        public QuizWindow()
        {
            InitializeComponent();
            LoadQuestions();
            ShowQuestion();
        }

        private void LoadQuestions()
        {
            _questions = new List<QuizQuestion>
            {
                new QuizQuestion { Question = "What should you do if you receive an email asking for your password?", OptionA = "Reply with your password", OptionB = "Delete the email", OptionC = "Report the email as phishing", CorrectAnswer = "C", Explanation = "Reporting phishing emails helps prevent scams." },
                new QuizQuestion { Question = "True or False: It is safe to use the same password for all your accounts.", OptionA = "True", OptionB = "False", OptionC = "N/A", CorrectAnswer = "B", Explanation = "If one account is breached, hackers will try that password on other sites." },
                new QuizQuestion { Question = "What does 2FA stand for?", OptionA = "Two-Factor Authentication", OptionB = "Two-File Access", OptionC = "Total Firewall Activation", CorrectAnswer = "A", Explanation = "2FA adds an extra layer of security." },
                new QuizQuestion { Question = "Which of the following is a sign of a phishing email?", OptionA = "It comes from a known contact", OptionB = "It creates a sense of extreme urgency", OptionC = "It has no spelling mistakes", CorrectAnswer = "B", Explanation = "Scammers use urgency to make you act without thinking." },
                new QuizQuestion { Question = "True or False: Public Wi-Fi is always secure.", OptionA = "True", OptionB = "False", OptionC = "N/A", CorrectAnswer = "B", Explanation = "Public Wi-Fi is often unsecured. Use a VPN." },
                new QuizQuestion { Question = "What is malware?", OptionA = "Software designed to harm devices", OptionB = "A type of firewall", OptionC = "A secure email protocol", CorrectAnswer = "A", Explanation = "Malware is malicious software." },
                new QuizQuestion { Question = "What is social engineering?", OptionA = "Building social media apps", OptionB = "Manipulating people into revealing info", OptionC = "Engineering secure networks", CorrectAnswer = "B", Explanation = "It exploits human psychology." },
                new QuizQuestion { Question = "True or False: You should click links in emails to verify if they are real.", OptionA = "True", OptionB = "False", OptionC = "N/A", CorrectAnswer = "B", Explanation = "Hover over links, don't click them." },
                new QuizQuestion { Question = "What does a firewall do?", OptionA = "Speeds up internet", OptionB = "Monitors and controls network traffic", OptionC = "Deletes viruses", CorrectAnswer = "B", Explanation = "Firewalls block unauthorized access." },
                new QuizQuestion { Question = "Which password is the strongest?", OptionA = "password123", OptionB = "MyDogName", OptionC = "G7#kL9!pQ2$m", CorrectAnswer = "C", Explanation = "Long passwords with symbols are strongest." },
                new QuizQuestion { Question = "True or False: Antivirus guarantees 100% protection.", OptionA = "True", OptionB = "False", OptionC = "N/A", CorrectAnswer = "B", Explanation = "No software is 100% foolproof." },
                new QuizQuestion { Question = "What is ransomware?", OptionA = "Software that locks files until you pay", OptionB = "A secure browser", OptionC = "A hardware firewall", CorrectAnswer = "A", Explanation = "Ransomware demands payment to unlock data." }
            };
        }

        private void ShowQuestion()
        {
            if (_currentQuestionIndex >= _questions.Count)
            {
                MessageBox.Show($"Quiz Complete!\nScore: {_score}/{_questions.Count}\n" + (_score >= 8 ? "Great job! You're a pro." : "Keep learning!"), "Quiz Results");
                this.Close();
                return;
            }

            var q = _questions[_currentQuestionIndex];
            QuestionText.Text = $"Q{_currentQuestionIndex + 1}: {q.Question}";
            OptionAButton.Content = $"A) {q.OptionA}";
            OptionBButton.Content = $"B) {q.OptionB}";
            OptionCButton.Content = $"C) {q.OptionC}";
            OptionCButton.Visibility = q.OptionC == "N/A" ? Visibility.Collapsed : Visibility.Visible;
            FeedbackText.Text = "";
            NextButton.IsEnabled = false;
        }

        private void AnswerClicked(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            string selected = btn.Name.Replace("Option", "").Replace("Button", "");
            var q = _questions[_currentQuestionIndex];

            OptionAButton.IsEnabled = false;
            OptionBButton.IsEnabled = false;
            OptionCButton.IsEnabled = false;

            if (selected == q.CorrectAnswer)
            {
                _score++;
                FeedbackText.Text = $"Correct! {q.Explanation}";
                FeedbackText.Foreground = Brushes.LightGreen;
            }
            else
            {
                FeedbackText.Text = $"Wrong. Answer was {q.CorrectAnswer}. {q.Explanation}";
                FeedbackText.Foreground = Brushes.IndianRed;
            }
            NextButton.IsEnabled = true;
        }

        private void NextClicked(object sender, RoutedEventArgs e)
        {
            _currentQuestionIndex++;
            OptionAButton.IsEnabled = true;
            OptionBButton.IsEnabled = true;
            OptionCButton.IsEnabled = true;
            ShowQuestion();
        }
    }
}