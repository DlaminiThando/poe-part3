using CybersecurityChatbotGUI;
using System;
using System.Linq;

namespace PROGPOEP2
{
    public class ChatbotEngine
    {
        private ResponseManager responseManager;
        private MemoryManager memory;
        private SentimentAnalyzer sentimentAnalyzer;
        private TaskManager taskManager;
        private IntentRecognizer intentRecognizer;
        private ActivityLogger activityLogger;
        private string lastTopic;
        private int followUpCount;
        private bool awaitingReminderForTask;
        private int pendingTaskId;

        public ChatbotEngine()
        {
            responseManager = new ResponseManager();
            memory = new MemoryManager();
            sentimentAnalyzer = new SentimentAnalyzer();
            taskManager = new TaskManager();
            intentRecognizer = new IntentRecognizer();
            activityLogger = new ActivityLogger();
            lastTopic = "";
            followUpCount = 0;
        }

        public string GetResponse(string userInput)
        {
            string lowerInput = userInput.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(lowerInput))
                return "I didn't catch that. Could you please type something? 🤔";

            // Name collection comes first
            if (string.IsNullOrEmpty(memory.GetUserName()))
                return HandleNameIntroduction(lowerInput);

            // Multi-turn reminder flow takes priority
            if (awaitingReminderForTask)
                return HandleReminderResponse(lowerInput);

            // ===== NLP INTENT ROUTING (Task 3) =====
            var intent = intentRecognizer.Recognize(lowerInput);

            switch (intent)
            {
                case IntentRecognizer.Intent.AddTask:
                    return HandleAddTaskCommand(userInput);

                case IntentRecognizer.Intent.ViewTasks:
                    activityLogger.Log("User viewed task list.");
                    return taskManager.FormatTaskList();

                case IntentRecognizer.Intent.CompleteTask:
                    return HandleCompleteTask(lowerInput);

                case IntentRecognizer.Intent.DeleteTask:
                    return HandleDeleteTask(lowerInput);

                case IntentRecognizer.Intent.SetReminder:
                    return HandleSetReminderIntent(userInput);

                case IntentRecognizer.Intent.StartQuiz:
                    activityLogger.Log("User requested quiz via chat.");
                    return "🎯 Click the Quiz button in the sidebar to start!";

                case IntentRecognizer.Intent.ShowActivityLog:
                    bool showAll = lowerInput.Contains("full") || lowerInput.Contains("all");
                    activityLogger.Log("User viewed activity log.");
                    return activityLogger.GetRecentLog(showAll);

                case IntentRecognizer.Intent.PasswordTopic:
                    lastTopic = "password";
                    StoreUserInterest(lowerInput);
                    activityLogger.Log("NLP: Responded to password security query.");
                    return responseManager.GetResponse("password", memory.GetUserName());

                case IntentRecognizer.Intent.PhishingTopic:
                    lastTopic = "phishing";
                    StoreUserInterest(lowerInput);
                    activityLogger.Log("NLP: Responded to phishing query.");
                    return responseManager.GetResponse("phishing", memory.GetUserName());

                case IntentRecognizer.Intent.PrivacyTopic:
                    lastTopic = "privacy";
                    StoreUserInterest(lowerInput);
                    activityLogger.Log("NLP: Responded to privacy query.");
                    return responseManager.GetResponse("privacy", memory.GetUserName());

                case IntentRecognizer.Intent.BrowsingTopic:
                    lastTopic = "browsing";
                    StoreUserInterest(lowerInput);
                    activityLogger.Log("NLP: Responded to safe browsing query.");
                    return responseManager.GetResponse("browsing", memory.GetUserName());

                case IntentRecognizer.Intent.ScamTopic:
                    lastTopic = "scam";
                    StoreUserInterest(lowerInput);
                    activityLogger.Log("NLP: Responded to scam/fraud query.");
                    return responseManager.GetResponse("scam", memory.GetUserName());

                case IntentRecognizer.Intent.FollowUp:
                    return HandleFollowUp();

                case IntentRecognizer.Intent.MemoryRecall:
                    activityLogger.Log("User recalled memory/profile info.");
                    return memory.RecallUserInfo();

                case IntentRecognizer.Intent.Exit:
                    activityLogger.Log("User ended the conversation.");
                    return GetGoodbyeMessage();
            }
            // ===== END NLP INTENT ROUTING =====

            // Sentiment detection runs on anything not caught by intent routing
            string sentiment = sentimentAnalyzer.DetectSentiment(lowerInput);
            if (sentiment != "neutral")
            {
                activityLogger.Log($"Sentiment detected: {sentiment}. Responded with support.");
                return HandleSentimentResponse(sentiment, lowerInput);
            }

            // Final fallback to ResponseManager
            string response = responseManager.GetResponse(lowerInput, memory.GetUserName());
            lastTopic = GetTopicFromInput(lowerInput);
            StoreUserInterest(lowerInput);

            return response;
        }

        private string HandleSetReminderIntent(string input)
        {
            string payload = intentRecognizer.ExtractPayload(input, new[]
            {
                "remind me to", "remind me about", "set a reminder to",
                "set a reminder for", "set reminder to", "notify me to",
                "can you remind me to", "can you remind me about"
            });

            if (string.IsNullOrWhiteSpace(payload))
                return "What would you like me to remind you about? Try: \"Remind me to update my password tomorrow.\"";

            var task = taskManager.AddTask(payload, $"{payload} to ensure your data is protected.");
            pendingTaskId = task.TaskId;
            awaitingReminderForTask = true;

            activityLogger.Log($"NLP: Task created via reminder phrase - '{payload}'.");
            return $"Task added: '{payload}'. When would you like to be reminded? (e.g. 'tomorrow', 'in 3 days')";
        }

        private string HandleNameIntroduction(string input)
        {
            string name = input.Trim();
            memory.SetUserName(name);
            memory.StoreInfo("name", name);
            activityLogger.Log($"New user introduced: {name}.");

            return $"Nice to meet you, {name}! 🎉\n\n" +
                   "I remember that! Now, what would you like to learn about cybersecurity today?\n" +
                   "Here are some topics you can ask me about:\n" +
                   "• Password safety\n• Phishing scams\n• Online privacy\n• Safe browsing";
        }

        private string HandleSentimentResponse(string sentiment, string input)
        {
            switch (sentiment)
            {
                case "worried":
                    memory.StoreInfo("sentiment", "worried");
                    return "It's completely understandable to feel worried about online security. 😟\n" +
                           "Let me share some reassuring tips that can help you stay safe.\n\n" +
                           responseManager.GetResponse("worried security", memory.GetUserName());

                case "frustrated":
                    memory.StoreInfo("sentiment", "frustrated");
                    return "I understand your frustration! Cybersecurity can feel overwhelming. 😤\n" +
                           "Let's take it step by step. Here's something simple to help:\n\n" +
                           responseManager.GetResponse("frustrated security", memory.GetUserName());

                case "curious":
                    memory.StoreInfo("sentiment", "curious");
                    return "It's great that you're curious about cybersecurity! 🌟\n" +
                           "Here's an interesting fact for you:\n\n" +
                           responseManager.GetResponse("curious security", memory.GetUserName());

                default:
                    return responseManager.GetResponse(input, memory.GetUserName());
            }
        }

        private string HandleFollowUp()
        {
            followUpCount++;
            activityLogger.Log($"Follow-up request on topic: {lastTopic}.");

            if (string.IsNullOrEmpty(lastTopic))
                return responseManager.GetRandomTip();

            if (followUpCount == 1)
            {
                return $"Sure! Here's another tip about {lastTopic}:\n\n" +
                       $"{responseManager.GetResponse(lastTopic, memory.GetUserName())}\n\n" +
                       "Would you like to hear another tip or ask about a different topic?";
            }
            else
            {
                return $"Absolutely! Let me share more about {lastTopic}:\n\n" +
                       $"{responseManager.GetAlternateResponse(lastTopic)}\n\n" +
                       "You can also ask me about passwords, phishing, privacy, or safe browsing!";
            }
        }

        private string GetTopicFromInput(string input)
        {
            string[] topics = { "password", "phishing", "privacy", "scam", "browsing", "security" };
            foreach (string topic in topics)
            {
                if (input.Contains(topic))
                    return topic;
            }
            return "cybersecurity";
        }

        private void StoreUserInterest(string input)
        {
            string[] topics = { "password", "phishing", "privacy", "scam", "browsing" };
            foreach (string topic in topics)
            {
                if (input.Contains(topic))
                {
                    memory.StoreInfo("interest", topic);
                    memory.StoreInfo("last_interest_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                    break;
                }
            }
        }

        private string GetGoodbyeMessage()
        {
            string name = memory.GetUserName();
            string interest = memory.GetUserInterest();

            if (!string.IsNullOrEmpty(interest))
            {
                return $"Goodbye, {name}! 👋\n\n" +
                       $"Remember to stay safe online! Keep working on your interest in {interest}.\n" +
                       "Feel free to come back anytime for more cybersecurity tips! 🔒";
            }

            return $"Goodbye, {name}! 👋 Stay safe online and remember to always use strong passwords! 🔒";
        }

        public void ResetMemory()
        {
            memory.ClearMemory();
            lastTopic = "";
            followUpCount = 0;
        }

        public string RecallUserInfo() => memory.RecallUserInfo();
        public string GetUserName() => memory.GetUserName();
        public string GetUserInterest() => memory.GetUserInterest();
        public TaskManager GetTaskManager() => taskManager;

        // Exposes logger so QuizForm can log quiz events
        public ActivityLogger GetActivityLogger() => activityLogger;

        // ========== TASK ASSISTANT (Part 3, Task 1) ========== //

        private string HandleAddTaskCommand(string input)
        {
            string title = ExtractTaskTitle(input);
            if (string.IsNullOrWhiteSpace(title))
                return "Sure! What would you like to call this task? E.g. \"Add task - Enable two-factor authentication\".";

            var task = taskManager.AddTask(title, $"{title} to ensure your data is protected.");
            pendingTaskId = task.TaskId;
            awaitingReminderForTask = true;
            activityLogger.Log($"Task added: '{title}'.");

            return $"Task added with the description \"{task.Description}\" Would you like a reminder?";
        }

        private string HandleReminderResponse(string input)
        {
            awaitingReminderForTask = false;

            if (input.Contains("no") && !input.Contains("now"))
                return "No problem, no reminder set. You can add one later by saying \"set a reminder for [task]\".";

            if (taskManager.SetReminder(pendingTaskId, input, out DateTime resolved))
            {
                activityLogger.Log($"Reminder set for task ID {pendingTaskId} on {resolved:dd MMM yyyy}.");
                return $"Got it! I'll remind you on {resolved:dd MMM yyyy}.";
            }

            return "I didn't catch a valid timeframe. Try something like \"in 3 days\" or \"tomorrow\".";
        }

        private string HandleCompleteTask(string input)
        {
            string fragment = StripCommandWords(input, new[] { "mark", "complete", "completed", "done", "finish", "finished", "task", "as" });
            if (string.IsNullOrWhiteSpace(fragment))
                return "Which task would you like to mark as complete? Try \"complete task - enable 2FA\".";

            if (taskManager.CompleteTaskByTitle(fragment, out var task))
            {
                activityLogger.Log($"Task marked complete: '{task!.Title}'.");
                return $"✅ Marked '{task!.Title}' as completed. Nice work staying on top of your cybersecurity!";
            }

            return $"I couldn't find a task matching \"{fragment}\". Try \"show my tasks\" to see exact titles.";
        }

        private string HandleDeleteTask(string input)
        {
            string fragment = StripCommandWords(input, new[] { "delete", "remove", "task" });
            if (string.IsNullOrWhiteSpace(fragment))
                return "Which task would you like to delete? Try \"delete task - enable 2FA\".";

            if (taskManager.DeleteTaskByTitle(fragment, out var task))
            {
                activityLogger.Log($"Task deleted: '{task!.Title}'.");
                return $"🗑️ Deleted task '{task!.Title}'.";
            }

            return $"I couldn't find a task matching \"{fragment}\".";
        }

        private string ExtractTaskTitle(string input)
        {
            string[] triggers = {
                "add a task to", "add task to", "add a task -", "add task -",
                "add a task:", "add task:", "create a task to", "create task to",
                "new task to", "add a task", "add task", "create task",
                "can you remind me to", "remind me to"
            };

            string lower = input.ToLower();
            foreach (var trigger in triggers)
            {
                int index = lower.IndexOf(trigger);
                if (index >= 0)
                {
                    string remainder = input.Substring(index + trigger.Length).Trim(' ', '-', ':');
                    if (!string.IsNullOrWhiteSpace(remainder))
                        return char.ToUpper(remainder[0]) + remainder.Substring(1);
                }
            }
            return "";
        }

        private string StripCommandWords(string input, string[] wordsToRemove)
        {
            var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Where(w => !wordsToRemove.Contains(w.Trim('-', ':')))
                .ToArray();
            return string.Join(' ', words).Trim(' ', '-', ':');
        }
    }
}