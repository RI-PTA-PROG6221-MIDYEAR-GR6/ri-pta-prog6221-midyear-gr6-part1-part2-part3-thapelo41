# 🔐 Cybersecurity Awareness Chatbot (PROG2A - Part 3)

A fully integrated Graphical User Interface (GUI) application built in C# using WPF. This final part expands the chatbot into a complete cybersecurity assistant with a Task Manager, a Mini-Game Quiz, NLP simulation, and Activity Logging.

---

 ✨ Features

- 🖥️ WPF GUI: Modern dark-themed interface with a chat area and a live Task Manager panel.
- 🗄️ MySQL Database: Saves, updates, and deletes cybersecurity tasks with date reminders.
- 🎮 Cybersecurity Quiz: An interactive 12-question mini-game with immediate feedback and scoring.
- 🧠 NLP Simulation: Recognizes intent (e.g., "remind me to...") without needing exact commands.
- 📝 Activity Log: Tracks actions (tasks added, quiz started) and displays the last 5 actions.
- 😟 Sentiment Detection: Detects mood (worried, frustrated) and adjusts tone.
- 🔑 Keyword Recognition: Randomised responses for passwords, scams, privacy, etc.
- YouTube Video: [https://youtu.be/3BkJ2vMzGVo]



⚙️ Setup Instructions (Read Carefully)

Because this app uses a MySQL database, you MUST set up the database before running the app.

 1. Database Setup
1. Open MySQL Workbench and connect to your local instance.
2. Open a new SQL tab and run the following code:
   ```sql
   CREATE DATABASE IF NOT EXISTS cyberbot_db;
   USE cyberbot_db;
   CREATE TABLE IF NOT EXISTS Tasks (
       TaskID INT AUTO_INCREMENT PRIMARY KEY,
       Title VARCHAR(255) NOT NULL,
       Description TEXT,
       ReminderDate DATE,
       IsCompleted BOOLEAN DEFAULT FALSE
   );
