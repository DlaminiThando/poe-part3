using System.Collections.Generic;

namespace PROGPOEP2
{
    // Simulates NLP by mapping many different phrasings to a single intent.
    // Uses string.Contains() keyword matching with synonym groups —
    // this is the "basic NLP technique" the brief refers to.
    public class IntentRecognizer
    {
        public enum Intent
        {
            AddTask,
            ViewTasks,
            CompleteTask,
            DeleteTask,
            SetReminder,
            StartQuiz,
            ShowActivityLog,
            PasswordTopic,
            PhishingTopic,
            PrivacyTopic,
            BrowsingTopic,
            ScamTopic,
            FollowUp,
            MemoryRecall,
            Exit,
            Unknown
        }

        // Each intent maps to a list of keyword groups.
        // ALL keywords in a group must be present for that group to match.
        private readonly Dictionary<Intent, List<string[]>> intentPatterns;

        public IntentRecognizer()
        {
            intentPatterns = new Dictionary<Intent, List<string[]>>
            {
                [Intent.AddTask] = new List<string[]>
                {
                    new[] { "add", "task" },
                    new[] { "create", "task" },
                    new[] { "new", "task" },
                    new[] { "add", "reminder" },
                    new[] { "remind", "me", "to" },
                    new[] { "can you remind" },
                    new[] { "set", "task" },
                    new[] { "make", "task" },
                    new[] { "schedule", "task" },
                },

                [Intent.ViewTasks] = new List<string[]>
                {
                    new[] { "show", "task" },
                    new[] { "view", "task" },
                    new[] { "list", "task" },
                    new[] { "my tasks" },
                    new[] { "see", "task" },
                    new[] { "what", "task" },
                    new[] { "display", "task" },
                    new[] { "show", "my", "task" },
                },

                [Intent.CompleteTask] = new List<string[]>
                {
                    new[] { "complete", "task" },
                    new[] { "mark", "done" },
                    new[] { "finished", "task" },
                    new[] { "done", "task" },
                    new[] { "mark", "complete" },
                    new[] { "task", "complete" },
                },

                [Intent.DeleteTask] = new List<string[]>
                {
                    new[] { "delete", "task" },
                    new[] { "remove", "task" },
                    new[] { "get rid", "task" },
                    new[] { "erase", "task" },
                    new[] { "cancel", "task" },
                },

                [Intent.SetReminder] = new List<string[]>
                {
                    new[] { "set", "reminder" },
                    new[] { "add", "reminder" },
                    new[] { "remind me" },
                    new[] { "set an alert" },
                    new[] { "notify me" },
                    new[] { "reminder for" },
                },

                [Intent.StartQuiz] = new List<string[]>
                {
                    new[] { "start", "quiz" },
                    new[] { "play", "quiz" },
                    new[] { "take", "quiz" },
                    new[] { "quiz me" },
                    new[] { "test my knowledge" },
                    new[] { "test me" },
                    new[] { "cyber", "quiz" },
                    new[] { "begin", "quiz" },
                    new[] { "open", "quiz" },
                    new[] { "launch", "quiz" },
                    new[] { "i want", "quiz" },
                },

                [Intent.ShowActivityLog] = new List<string[]>
                {
                    new[] { "show", "log" },
                    new[] { "activity", "log" },
                    new[] { "what have you done" },
                    new[] { "show", "history" },
                    new[] { "view", "log" },
                    new[] { "recent", "action" },
                    new[] { "what did you do" },
                    new[] { "show", "activity" },
                },

                [Intent.PasswordTopic] = new List<string[]>
                {
                    new[] { "password" },
                    new[] { "passphrase" },
                    new[] { "login", "security" },
                    new[] { "strong", "password" },
                    new[] { "password", "manager" },
                    new[] { "2fa" },
                    new[] { "two factor" },
                    new[] { "two-factor" },
                    new[] { "authentication" },
                },

                [Intent.PhishingTopic] = new List<string[]>
                {
                    new[] { "phishing" },
                    new[] { "phish" },
                    new[] { "fake", "email" },
                    new[] { "suspicious", "email" },
                    new[] { "scam", "email" },
                    new[] { "email", "attack" },
                    new[] { "spam", "email" },
                    new[] { "fraudulent", "email" },
                },

                [Intent.PrivacyTopic] = new List<string[]>
                {
                    new[] { "privacy" },
                    new[] { "private" },
                    new[] { "personal data" },
                    new[] { "data protection" },
                    new[] { "protect", "information" },
                    new[] { "social media", "privacy" },
                    new[] { "app permission" },
                    new[] { "location", "tracking" },
                },

                [Intent.BrowsingTopic] = new List<string[]>
                {
                    new[] { "browsing" },
                    new[] { "safe browsing" },
                    new[] { "https" },
                    new[] { "vpn" },
                    new[] { "browser" },
                    new[] { "website", "safe" },
                    new[] { "internet", "safe" },
                    new[] { "ad blocker" },
                },

                [Intent.ScamTopic] = new List<string[]>
                {
                    new[] { "scam" },
                    new[] { "fraud" },
                    new[] { "social engineering" },
                    new[] { "trick" },
                    new[] { "fake", "website" },
                    new[] { "online", "scam" },
                    new[] { "identity theft" },
                },

                [Intent.FollowUp] = new List<string[]>
                {
                    new[] { "tell me more" },
                    new[] { "more info" },
                    new[] { "explain more" },
                    new[] { "another tip" },
                    new[] { "what else" },
                    new[] { "continue" },
                    new[] { "elaborate" },
                    new[] { "give me more" },
                    new[] { "more detail" },
                },

                [Intent.MemoryRecall] = new List<string[]>
                {
                    new[] { "what did i say" },
                    new[] { "what do you know about me" },
                    new[] { "recall" },
                    new[] { "remember me" },
                    new[] { "my info" },
                    new[] { "what do you remember" },
                },

                [Intent.Exit] = new List<string[]>
                {
                    new[] { "bye" },
                    new[] { "goodbye" },
                    new[] { "exit" },
                    new[] { "quit" },
                    new[] { "see you" },
                    new[] { "farewell" },
                    new[] { "close" },
                },
            };
        }

        public Intent Recognize(string input)
        {
            string lower = input.ToLower().Trim();

            foreach (var kvp in intentPatterns)
            {
                foreach (var keywordGroup in kvp.Value)
                {
                    bool allMatch = true;
                    foreach (var keyword in keywordGroup)
                    {
                        if (!lower.Contains(keyword))
                        {
                            allMatch = false;
                            break;
                        }
                    }
                    if (allMatch)
                        return kvp.Key;
                }
            }

            return Intent.Unknown;
        }

        // Extracts whatever comes after a known trigger phrase
        // e.g. "can you remind me to update my password" → "update my password"
        public string ExtractPayload(string input, string[] triggerPhrases)
        {
            string lower = input.ToLower();
            foreach (var trigger in triggerPhrases)
            {
                int index = lower.IndexOf(trigger);
                if (index >= 0)
                {
                    string remainder = input.Substring(index + trigger.Length).Trim(' ', '-', ':', '?', '!');
                    if (!string.IsNullOrWhiteSpace(remainder))
                        return char.ToUpper(remainder[0]) + remainder.Substring(1);
                }
            }
            return "";
        }
    }
}