# Cybersecurity Awareness Chatbot - Final POE

## Overview

This is a Windows desktop application built with C# and Windows Forms (.NET 9). It presents a conversational chatbot that educates users on cybersecurity topics such as password safety, phishing, online privacy, safe browsing, and scam detection. The application includes a task manager, a quiz feature, an activity log, and basic natural language intent recognition.

## Project Structure

The solution is named PROGPOEP2 and contains a single project with the following source files:

- Program.cs - Application entry point. Plays a voice greeting, displays ASCII art in the console, and launches the main form.
- MainForm.cs / MainForm.Designer.cs - The main GUI window. Contains the chat display, user input field, and sidebar buttons for tasks, quiz, memory recall, topics, and help.
- ChatbotEngine.cs - Core logic that processes user input, routes it to the appropriate handler based on intent, manages conversation state, and produces responses.
- IntentRecognizer.cs - Performs keyword-based intent recognition. Maps various user phrasings to a fixed set of intents such as adding a task, viewing tasks, asking about cybersecurity topics, starting the quiz, and viewing the activity log.
- ResponseManager.cs - Stores and retrieves topic-specific responses and tips for cybersecurity subjects.
- MemoryManager.cs - Stores the user's name, interests, and sentiment across the conversation session.
- SentimentAnalyzer.cs - Detects basic sentiment from user input (worried, frustrated, or curious) and adjusts the chatbot's response tone accordingly.
- TaskItem.cs - Data model representing a single task with a title, description, optional reminder date, completion status, and creation timestamp.
- TasManager.cs - Manages a list of task items, supporting add, complete, delete, and reminder operations.
- TaskRepository.cs - Handles persistence of tasks using a MySQL database via the MySql.Data NuGet package.
- TaskForm.cs / AddTaskDialog.cs - GUI forms for visually managing tasks outside of the chat interface.
- QuizQuestion.cs - Data model for a quiz question, supporting both multiple-choice and true/false question types.
- QuizManager.cs - Contains a bank of 12 cybersecurity quiz questions. Shuffles and presents them, evaluates answers, and returns a final score with feedback.
- QuizForm.cs - GUI form that runs the quiz, displaying questions and answer options to the user.
- ActivityLogger.cs - Records timestamped log entries for significant chatbot actions during the session and can display the most recent entries on request.
- VoiceManager.cs - Plays a WAV audio greeting file on application startup.
- AsciiArt.cs - Displays a decorative logo in the console window on startup.

## Features

**Conversational chatbot**
The chatbot greets the user and asks for their name before proceeding. It remembers the user's name and their most recently discussed topic within the session. Follow-up questions like "tell me more" or "another tip" retrieve additional information on the last topic discussed.

**Intent recognition**
User input is matched against keyword patterns to determine intent. Supported intents include adding, viewing, completing, and deleting tasks; setting reminders; asking about passwords, phishing, privacy, browsing, or scams; starting the quiz; viewing the activity log; recalling session memory; and exiting.

**Sentiment detection**
If the user expresses worry, frustration, or curiosity, the chatbot acknowledges the sentiment and adjusts its response before providing relevant cybersecurity information.

**Task manager**
Users can manage cybersecurity-related to-do items through natural language commands in the chat or through a dedicated task GUI. Tasks support optional date-based reminders. The chatbot guides the user through a multi-turn conversation to set a reminder after a task is added. Tasks are persisted to a MySQL database.

**Cybersecurity quiz**
A quiz with 12 questions covering password safety, phishing, HTTPS, two-factor authentication, VPNs, social engineering, and more. Questions are shuffled each session. The quiz is accessible via a sidebar button or by typing quiz-related commands. Results are shown at the end with a score and feedback message.

**Activity log**
The chatbot maintains an in-session log of actions such as topics discussed, tasks added or completed, quiz events, and sentiment detections. The user can request the recent log through the chat or view the full log by asking for it.

## Requirements

- Windows operating system (the project targets net9.0-windows and uses Windows Forms)
- .NET 9 SDK
- A MySQL database instance for task persistence (connection details must be configured in TaskRepository.cs)
- The greeting.wav file must be present in the project output directory (it is included in the project and copied automatically on build)

## Dependencies

- MySql.Data version 9.7.0 - used for task storage
- System.Windows.Extensions version 9.0.0 - used for audio playback via SoundPlayer

## Building and Running

1. Open the solution file PROGPOEP2.sln in Visual Studio or run the following commands from the solution root:

```
dotnet restore
dotnet build
dotnet run --project PROGPOEP2/PROGPOEP2.csproj
```

2. On first launch the application plays a greeting sound and opens the main chat window.
3. The chatbot will ask for your name to begin the session.

## CI/CD

The repository includes a GitHub Actions workflow at .github/workflows/main.yml. On every push to main or master it builds and publishes the application in Release configuration, automatically increments the patch version tag, and creates a GitHub Release with a ZIP archive of the published output.

## Author

Thando Dlamini
