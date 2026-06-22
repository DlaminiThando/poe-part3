using System.Collections.Generic;

namespace PROGPOEP2
{
    public class QuizManager
    {
        private List<QuizQuestion> questions;
        private int currentIndex;
        private int score;
        private bool quizActive;

        public bool IsQuizActive => quizActive;
        public int Score => score;
        public int TotalQuestions => questions.Count;
        public int CurrentIndex => currentIndex;

        public QuizManager()
        {
            questions = BuildQuestions();
            currentIndex = 0;
            score = 0;
            quizActive = false;
        }

        private List<QuizQuestion> BuildQuestions()
        {
            return new List<QuizQuestion>
            {
                // Multiple choice
                new QuizQuestion(
                    "What should you do if you receive an email asking for your password?",
                    new[] { "A) Reply with your password", "B) Delete the email",
                            "C) Report the email as phishing", "D) Ignore it" },
                    "C",
                    "Reporting phishing emails helps prevent scams and protects others."
                ),
                new QuizQuestion(
                    "Which of the following is the strongest password?",
                    new[] { "A) password123", "B) MyDog2010",
                            "C) Tr0ub4dor&3!", "D) qwerty" },
                    "C",
                    "Strong passwords use a mix of uppercase, lowercase, numbers, and symbols."
                ),
                new QuizQuestion(
                    "What does HTTPS indicate about a website?",
                    new[] { "A) It loads faster", "B) The connection is encrypted and more secure",
                            "C) The site is government-owned", "D) The site has no ads" },
                    "B",
                    "HTTPS means data between your browser and the site is encrypted using SSL/TLS."
                ),
                new QuizQuestion(
                    "What is two-factor authentication (2FA)?",
                    new[] { "A) Using two different passwords", "B) Logging in from two devices",
                            "C) A second verification step beyond your password", "D) Encrypting your files twice" },
                    "C",
                    "2FA adds a second layer of security, like a code sent to your phone."
                ),
                new QuizQuestion(
                    "Which of these is a sign of a phishing website?",
                    new[] { "A) It uses HTTPS", "B) The URL looks slightly misspelled",
                            "C) It has a privacy policy", "D) It loads quickly" },
                    "B",
                    "Phishing sites often use URLs like 'paypa1.com' to trick users."
                ),
                new QuizQuestion(
                    "What is social engineering in cybersecurity?",
                    new[] { "A) Building social media platforms", "B) Hacking using code exploits",
                            "C) Manipulating people into revealing confidential info", "D) Network engineering" },
                    "C",
                    "Social engineering exploits human psychology rather than technical vulnerabilities."
                ),
                new QuizQuestion(
                    "How often should you update your passwords?",
                    new[] { "A) Never", "B) Every 10 years",
                            "C) Regularly, especially after a breach", "D) Only when you forget them" },
                    "C",
                    "Regular password updates reduce risk, especially if a breach is suspected."
                ),
                new QuizQuestion(
                    "What is a VPN used for?",
                    new[] { "A) Speeding up your internet", "B) Blocking ads",
                            "C) Encrypting your internet connection for privacy", "D) Storing passwords" },
                    "C",
                    "A VPN encrypts your traffic and masks your IP address for privacy."
                ),

                // True / False
                new QuizQuestion(
                    "Using public Wi-Fi without a VPN is completely safe for online banking.",
                    false,
                    "Public Wi-Fi is unencrypted — attackers can intercept your data easily."
                ),
                new QuizQuestion(
                    "Antivirus software alone is enough to protect you from all cyber threats.",
                    false,
                    "Antivirus is one layer — you also need strong passwords, updates, and safe habits."
                ),
                new QuizQuestion(
                    "It is safe to reuse the same password across multiple websites.",
                    false,
                    "If one site is breached, attackers try the same password everywhere else."
                ),
                new QuizQuestion(
                    "Keeping your software and OS updated helps protect against known vulnerabilities.",
                    true,
                    "Updates patch security holes that attackers actively exploit."
                ),
            };
        }

        public void StartQuiz()
        {
            currentIndex = 0;
            score = 0;
            quizActive = true;

            // Shuffle so questions appear in a different order each time
            var rng = new System.Random();
            for (int i = questions.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (questions[i], questions[j]) = (questions[j], questions[i]);
            }
        }

        public QuizQuestion? GetCurrentQuestion()
        {
            if (!quizActive || currentIndex >= questions.Count)
                return null;
            return questions[currentIndex];
        }

        // Returns true if answer was correct, sets explanation as out param
        public bool SubmitAnswer(string answer, out string explanation)
        {
            var q = questions[currentIndex];
            explanation = q.Explanation;
            bool correct = answer.Trim().ToUpper().StartsWith(q.CorrectAnswer.ToUpper());

            if (correct) score++;
            currentIndex++;

            if (currentIndex >= questions.Count)
                quizActive = false;

            return correct;
        }

        public bool HasMoreQuestions => currentIndex < questions.Count;

        public string GetFinalFeedback()
        {
            double percent = (double)score / questions.Count * 100;

            if (percent >= 80)
                return $"🏆 Great job! You scored {score}/{questions.Count}. You're a cybersecurity pro!";
            else if (percent >= 50)
                return $"👍 Not bad! You scored {score}/{questions.Count}. Keep learning to stay safe online!";
            else
                return $"📚 You scored {score}/{questions.Count}. Keep learning — cybersecurity knowledge keeps you safe!";
        }
    }
}