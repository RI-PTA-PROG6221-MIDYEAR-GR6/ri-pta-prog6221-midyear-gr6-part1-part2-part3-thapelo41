# 🔐 Cybersecurity Awareness Chatbot (PROG2A - Part 2)

A Graphical User Interface (GUI) application built in C# using Windows Presentation Foundation (WPF) that educates users about cybersecurity topics through an interactive, conversational chatbot.

---

## 📖 Table of Contents

1. [Project Overview](#-project-overview)
2. [Features](#-features)
3. [Technologies Used](#️-technologies-used)
4. [Setup Instructions](#-setup-instructions)
5. [Usage Guide](#-usage-guide)
6. [Project Structure](#-project-structure)
7. [Code Highlights](#-code-highlights)
8. [Video Presentation](#-video-presentation)
9. [Author](#-author)

---

## 🎯 Project Overview

This project is **Part 2** of the Cybersecurity Awareness Chatbot assignment. It expands the original command-line application (Part 1) into a fully functional WPF GUI application. The chatbot is designed to help users learn about cybersecurity best practices, including password safety, phishing, scams, privacy, and malware.

The chatbot simulates a natural conversation using:

- **Keyword recognition** for cybersecurity topics
- **Randomised responses** to avoid repetition
- **Memory** to recall user details (name, favourite topic)
- **Sentiment detection** to adapt its tone based on user mood
- **Follow-up handling** for smooth conversation flow

---

## ✨ Features

| Feature | Description |
|--------|-------------|
| 🖥️ **WPF GUI** | Modern, dark-themed, user-friendly interface with chat history, input box, and ASCII art header. |
| 🔊 **Voice Greeting** | Plays a `.wav` audio file when the application launches. |
| 🔑 **Keyword Recognition** | Detects at least 5 cybersecurity keywords (password, scam, privacy, phishing, malware). |
| 🎲 **Random Responses** | Uses `List<string>` to provide varied responses for each topic. |
| 💬 **Conversation Flow** | Handles follow-ups like "another tip" or "tell me more" without restarting. |
| 🧠 **Memory & Recall** | Remembers the user's name and favourite cybersecurity topic for personalised responses. |
| 😟 **Sentiment Detection** | Detects "worried", "frustrated", "curious", etc., and adapts responses empathetically. |
| 🛡️ **Error Handling** | Provides default responses for unknown inputs and handles audio file errors gracefully. |
| 📦 **OOP Structure** | Clean separation between GUI (`MainWindow`) and logic (`CyberBot`). Uses `Dictionary`, `List`, and `Func` delegates. |

---

## 🛠️ Technologies Used

- **Language:** C#
- **Framework:** .NET 8 with **WPF (Windows Presentation Foundation)**
- **IDE:** Microsoft Visual Studio 2022
- **Version Control:** Git & GitHub
- **Audio:** `System.Media.SoundPlayer`

---

## ⚙️ Setup Instructions

Follow these steps to run the project on your machine:

### Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/) (or newer)
- **.NET Desktop Development** workload installed
- A working audio output device (for the voice greeting)

### Steps

1. **Clone the repository**

   ```bash
   git clone https://github.com/thapelo41/ri-pta-prog6221-midyear-gr6-part1-part2-part3-thapelo41.git