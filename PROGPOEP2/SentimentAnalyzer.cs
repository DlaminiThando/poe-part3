using System;
using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    public class SentimentAnalyzer
    {
        private List<string> worriedKeywords;
        private List<string> frustratedKeywords;
        private List<string> curiousKeywords;
        private List<string> happyKeywords;

        public SentimentAnalyzer()
        {
            InitializeKeywordLists();
        }

        private void InitializeKeywordLists()
        {
            worriedKeywords = new List<string>
            {
                "worried", "scared", "afraid", "nervous", "anxious", "concerned",
                "unsafe", "vulnerable", "panic", "fear", "stress", "overwhelmed"
            };

            frustratedKeywords = new List<string>
            {
                "frustrated", "annoyed", "angry", "mad", "upset", "frustrating",
                "difficult", "hard", "tired", "confused", "lost", "complicated"
            };

            curiousKeywords = new List<string>
            {
                "curious", "interested", "wonder", "want to know", "tell me",
                "explain", "learn", "understand", "how does", "why", "what is"
            };

            happyKeywords = new List<string>
            {
                "happy", "great", "good", "awesome", "excellent", "thanks",
                "thank you", "appreciate", "love", "enjoy"
            };
        }

        public string DetectSentiment(string input)
        {
            string lowerInput = input.ToLower();

            // Check for worried sentiment
            foreach (string keyword in worriedKeywords)
            {
                if (lowerInput.Contains(keyword))
                    return "worried";
            }

            // Check for frustrated sentiment
            foreach (string keyword in frustratedKeywords)
            {
                if (lowerInput.Contains(keyword))
                    return "frustrated";
            }

            // Check for curious sentiment
            foreach (string keyword in curiousKeywords)
            {
                if (lowerInput.Contains(keyword))
                    return "curious";
            }

            // Check for happy sentiment
            foreach (string keyword in happyKeywords)
            {
                if (lowerInput.Contains(keyword))
                    return "happy";
            }

            return "neutral";
        }

        public string GetSentimentEmoji(string sentiment)
        {
            switch (sentiment)
            {
                case "worried": return "😟";
                case "frustrated": return "😤";
                case "curious": return "🧐";
                case "happy": return "😊";
                default: return "🤖";
            }
        }
    }
}