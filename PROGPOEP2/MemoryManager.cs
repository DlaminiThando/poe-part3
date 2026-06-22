using System;
using System.Collections.Generic;

namespace PROGPOEP2
{
    public class MemoryManager
    {
        private Dictionary<string, string> userMemory;
        private string userName;
        private string userInterest;

        public MemoryManager()
        {
            userMemory = new Dictionary<string, string>();
            userName = "";
            userInterest = "";
        }

        public void SetUserName(string name)
        {
            userName = name;
            StoreInfo("user_name", name);
        }

        public string GetUserName()
        {
            return userName;
        }

        public string GetUserInterest()
        {
            return userInterest;
        }

        public void StoreInfo(string key, string value)
        {
            if (!userMemory.ContainsKey(key))
            {
                userMemory.Add(key, value);
            }
            else
            {
                userMemory[key] = value;
            }

            // Track interest separately for quick access
            if (key == "interest")
            {
                userInterest = value;
            }
        }

        public string RecallUserInfo()
        {
            if (userMemory.Count == 0)
            {
                return "I don't remember anything yet! Tell me about your cybersecurity interests. 😊";
            }

            string recallMessage = "Here's what I remember about you:\n\n";

            if (userMemory.ContainsKey("user_name"))
            {
                recallMessage += $"• Your name: {userMemory["user_name"]}\n";
            }

            if (userMemory.ContainsKey("interest"))
            {
                recallMessage += $"• Your interest: {userMemory["interest"]}\n";
            }

            if (userMemory.ContainsKey("last_interest_date"))
            {
                recallMessage += $"• You asked about cybersecurity on: {userMemory["last_interest_date"]}\n";
            }

            if (userMemory.ContainsKey("sentiment"))
            {
                recallMessage += $"• Your last expressed feeling: {userMemory["sentiment"]}\n";
            }

            recallMessage += "\nIs there anything specific you'd like to learn more about?";

            return recallMessage;
        }

        public void ClearMemory()
        {
            userMemory.Clear();
            userName = "";
            userInterest = "";
        }

        public string GetPersonalizedMessage(string baseMessage)
        {
            if (!string.IsNullOrEmpty(userInterest))
            {
                return $"{baseMessage}\n\nSince you're interested in {userInterest}, here's a special tip: " +
                       $"{GetPersonalizedTip()}\n\nStay safe, {userName}!";
            }

            if (!string.IsNullOrEmpty(userName))
            {
                return $"{baseMessage}\n\nStay safe, {userName}!";
            }

            return baseMessage;
        }

        private string GetPersonalizedTip()
        {
            switch (userInterest?.ToLower())
            {
                case "password":
                    return "Create a passphrase instead of a password - it's easier to remember and harder to crack!";
                case "phishing":
                    return "Always check the sender's email address carefully - scammers often use addresses that look almost legitimate!";
                case "privacy":
                    return "Review your app permissions regularly - many apps request access they don't actually need!";
                case "scam":
                    return "Remember: If something seems too good to be true, it probably is! Trust your instincts.";
                default:
                    return "Stay vigilant and keep learning about online safety!";
            }
        }
    }
}