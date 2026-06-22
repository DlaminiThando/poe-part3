using System;
using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    public class ResponseManager
    {
        private Dictionary<string, List<string>> responses;
        private Random random;
        private List<string> generalTips;

        public ResponseManager()
        {
            random = new Random();
            InitializeResponses();
            InitializeGeneralTips();
        }

        private void InitializeResponses()
        {
            responses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                // Password-related responses
                ["password"] = new List<string>
                {
                    "🔒 **Strong Password Tips:**\n- Use at least 12 characters\n- Mix uppercase, lowercase, numbers, and symbols\n- Avoid personal information like birthdays\n- Never reuse passwords across multiple sites",
                    "🔐 **Password Manager:** Consider using a password manager like Bitwarden or LastPass. They generate and store strong passwords securely!",
                    "⚠️ **Password Red Flags:** Never share your password via email or text. Legitimate companies won't ask for your password!"
                },

                // Phishing responses
                ["phishing"] = new List<string>
                {
                    "🎣 **Phishing Warning Signs:**\n- Urgent language ('Your account will be closed!')\n- Spelling and grammar mistakes\n- Suspicious email addresses\n- Requests for personal information",
                    "📧 **Email Safety:** Always hover over links before clicking to see the real URL. When in doubt, go directly to the website rather than clicking email links!",
                    "🛡️ **Phishing Protection:** Enable 2-factor authentication (2FA) on all important accounts. This adds an extra layer of security even if your password is stolen!"
                },

                // Privacy responses
                ["privacy"] = new List<string>
                {
                    "🔏 **Privacy Protection:**\n- Review app permissions regularly\n- Use privacy-focused browsers like Brave or Firefox\n- Consider using a VPN on public Wi-Fi\n- Regularly clear cookies and browsing data",
                    "📱 **Social Media Privacy:** Adjust your privacy settings to limit who can see your posts. Avoid sharing your location or travel plans in real-time!"
                },

                // Scam responses
                ["scam"] = new List<string>
                {
                    "⚠️ **Common Online Scams:**\n- Fake tech support calls\n- Lottery or prize scams\n- Romance scams on dating apps\n- Fake job offers asking for payment",
                    "💸 **Financial Scams:** Never send money to someone you haven't met in person. Legitimate organizations won't ask for payment via gift cards or cryptocurrency!"
                },

                // Safe browsing responses
                ["browsing"] = new List<string>
                {
                    "🌐 **Safe Browsing Checklist:**\n✓ Look for HTTPS (padlock icon)\n\r✓ Keep browser updated\n\r✓ Use ad-blockers\n\r✓ Avoid suspicious downloads",
                    "🛡️ **Browser Extensions** for security: uBlock Origin, HTTPS Everywhere, and Privacy Badger can help protect you while browsing!"
                },

                // Help responses
                ["help"] = new List<string>
                {
                    "🤖 **I can help you with these topics:**\n• Password safety\n• Phishing scams\n• Online privacy\n• Scam detection\n• Safe browsing\n\nJust ask me about any of these!",
                    "💡 **Try asking me:**\n- 'What are strong passwords?'\n- 'How to spot phishing?'\n- 'Privacy tips'\n- 'Safe browsing habits'"
                },

                // Sentiment-specific responses
                ["worried security"] = new List<string>
                {
                    "Don't worry! 😊 Here's a simple step to feel more secure: Enable two-factor authentication on your email and banking accounts. It's easy and very effective!",
                    "Feeling worried is normal! Start with small changes: Use a password manager and never click suspicious links. You've got this! 💪"
                },

                ["frustrated security"] = new List<string>
                {
                    "Take a deep breath! 😌 Let me give you one simple tip: Start with updating your passwords for your most important accounts. Progress, not perfection!",
                    "I hear you! Cybersecurity can be overwhelming. Focus on one thing today: set up a password manager. It makes everything easier! 🚀"
                },

                ["curious security"] = new List<string>
                {
                    "Great curiosity! 🧠 Did you know? 81% of data breaches are caused by weak or stolen passwords. That's why strong passwords matter!",
                    "Here's something interesting: Phishing attacks increased by 61% in recent years. Stay curious and keep learning to stay safe! 📚"
                }
            };
        }

        private void InitializeGeneralTips()
        {
            generalTips = new List<string>
            {
                "💡 **Quick Tip:** Always verify the sender's email address before clicking links or downloading attachments!",
                "🔐 **Pro Tip:** Use a passphrase instead of a password. Example: 'Purple-Elephant-Dances-2024!' is both strong and memorable!",
                "🛡️ **Security Reminder:** Keep your software and operating system updated. Updates often include important security patches!",
                "📱 **Mobile Safety:** Lock your phone with a strong PIN or biometric authentication. Don't leave it unattended in public places!",
                "🌐 **Wi-Fi Warning:** Avoid accessing sensitive accounts (banking, email) on public Wi-Fi without using a VPN!"
            };
        }

        public string GetResponse(string input, string userName)
        {
            // Check each topic
            foreach (var topic in responses.Keys)
            {
                if (input.Contains(topic.ToLower()))
                {
                    List<string> possibleResponses = responses[topic];
                    string response = possibleResponses[random.Next(possibleResponses.Count)];

                    // Add personalization if user name is known
                    if (!string.IsNullOrEmpty(userName) && !response.Contains(userName))
                    {
                        response = $"{response}\n\nStay safe, {userName}! 🛡️";
                    }

                    return response;
                }
            }

            // Default response for unknown input
            return GetDefaultResponse();
        }

        public string GetAlternateResponse(string topic)
        {
            if (responses.ContainsKey(topic) && responses[topic].Count > 1)
            {
                // Get a different response (not the first one)
                int index = random.Next(1, responses[topic].Count);
                return responses[topic][index];
            }
            return GetRandomTip();
        }

        public string GetRandomTip()
        {
            return generalTips[random.Next(generalTips.Count)];
        }

        private string GetDefaultResponse()
        {
            string[] defaultResponses = {
                "I'm not sure I understand. 🤔 Could you rephrase or ask me about passwords, phishing, privacy, or safe browsing?",
                "Hmm, I didn't quite catch that. 😕 Try asking me about cybersecurity topics like 'password safety' or 'phishing tips'!",
                "I'm still learning! 📚 Could you ask me about specific cybersecurity topics like passwords, scams, or privacy?",
                "Let me help! 💡 Try asking:\n• 'How to create strong passwords?'\n• 'What is phishing?'\n• 'Privacy tips'"
            };
            return defaultResponses[random.Next(defaultResponses.Length)];
        }
    }
}