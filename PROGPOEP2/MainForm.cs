using CybersecurityChatbotGUI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PROGPOEP2
{
    public partial class MainForm : Form
    {
        private ChatbotEngine chatbot;

        public MainForm()
        {
            InitializeComponent();
            chatbot = new ChatbotEngine();
            SetupForm();
            DisplayWelcomeMessage();

            // Attach event handlers
            btnSend.Click += BtnSend_Click;
            btnClearChat.Click += BtnClearChat_Click;
            btnNewConversation.Click += BtnNewConversation_Click;
            btnMemory.Click += BtnMemory_Click;
            btnTopics.Click += BtnTopics_Click;
            btnHelp.Click += BtnHelp_Click;
            btnTasks.Click += BtnTasks_Click;
            btnQuiz.Click += BtnQuiz_Click;
            txtUserInput.KeyDown += TxtUserInput_KeyDown;
        }

        private void SetupForm()
        {
            this.Text = "Cybersecurity Awareness Bot";
            this.Size = new Size(900, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(20, 20, 35);
        }

        private void TxtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSend_Click(sender, e);
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtUserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                AppendMessage("Please enter a message.", "System", Color.Yellow);
                return;
            }

            // Display user message
            AppendMessage(userInput, "You", Color.LightBlue);

            // Update status
            lblStatus.Text = "🤖 Bot is thinking...";
            Application.DoEvents();

            // Get bot response
            string response = chatbot.GetResponse(userInput);

            // Display bot response
            AppendMessage(response, "🛡️ CyberBot", Color.LightGreen);

            // Clear input
            txtUserInput.Clear();

            // Update status
            lblStatus.Text = "🤖 Bot is ready";

            // Update user info display
            UpdateUserInfoDisplay();
        }

        private void AppendMessage(string message, string sender, Color color)
        {
            if (rtbChatDisplay.InvokeRequired)
            {
                rtbChatDisplay.Invoke(new Action(() => AppendMessage(message, sender, color)));
                return;
            }

            rtbChatDisplay.SelectionStart = rtbChatDisplay.TextLength;
            rtbChatDisplay.SelectionLength = 0;

            rtbChatDisplay.SelectionColor = color;
            rtbChatDisplay.SelectionFont = new Font("Segoe UI", 10, FontStyle.Bold);
            rtbChatDisplay.AppendText($"{sender}: ");

            rtbChatDisplay.SelectionColor = Color.White;
            rtbChatDisplay.SelectionFont = new Font("Segoe UI", 10, FontStyle.Regular);
            rtbChatDisplay.AppendText($"{message}\n\n");

            rtbChatDisplay.ScrollToCaret();
        }

        private void DisplayWelcomeMessage()
        {
            string welcome = "👋 Hello! I'm your Cybersecurity Awareness Bot.\n\n" +
                           "I can help you learn about:\n" +
                           "• 🔒 Password safety\n" +
                           "• 🎣 Phishing attacks\n" +
                           "• 🔐 Online privacy\n" +
                           "• 🌐 Safe browsing\n" +
                           "• ⚠️ Scam detection\n\n" +
                           "💬 Task commands: 'add task - [title]', 'show my tasks'\n" +
                           "📋 Or click the Tasks button to manage tasks visually.\n" +
                           "🎯 Click Quiz to test your cybersecurity knowledge!\n\n" +
                           "What's your name?";

            AppendMessage(welcome, "🛡️ CyberBot", Color.LightGreen);
        }

        private void BtnClearChat_Click(object sender, EventArgs e)
        {
            rtbChatDisplay.Clear();
            DisplayWelcomeMessage();
        }

        private void BtnNewConversation_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Start a new conversation? This will clear the chat and reset memory.",
                "New Conversation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                chatbot.ResetMemory();
                rtbChatDisplay.Clear();
                DisplayWelcomeMessage();
                lblStatus.Text = "🤖 Bot is ready - New conversation started";
                UpdateUserInfoDisplay();
            }
        }

        private void BtnMemory_Click(object sender, EventArgs e)
        {
            string memoryRecall = chatbot.RecallUserInfo();
            AppendMessage(memoryRecall, "🛡️ CyberBot", Color.LightGreen);
        }

        private void BtnTopics_Click(object sender, EventArgs e)
        {
            string topics = "📚 **Topics I can help with:**\n\n" +
                            "• 🔒 **Password Safety** - Strong passwords, password managers\n" +
                            "• 🎣 **Phishing** - Spot fake emails, avoid scams\n" +
                            "• 🔐 **Privacy** - Protect personal info, app permissions\n" +
                            "• ⚠️ **Scams** - Recognize common online scams\n" +
                            "• 🌐 **Safe Browsing** - HTTPS, ad-blockers, updates\n\n" +
                            "Just ask me about any of these topics!";

            AppendMessage(topics, "🛡️ CyberBot", Color.LightGreen);
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            string help = "❓ **How to use CyberBot:**\n\n" +
                          "💬 Type your questions naturally!\n\n" +
                          "**Try asking about:**\n" +
                          "  • 'How to create strong passwords?'\n" +
                          "  • 'What is phishing?'\n" +
                          "  • 'Privacy tips for social media'\n" +
                          "  • 'How to spot online scams?'\n\n" +
                          "**Task commands:**\n" +
                          "  • 'Add task - Enable two-factor authentication'\n" +
                          "  • 'Show my tasks'\n" +
                          "  • 'Complete task - [title]'\n" +
                          "  • 'Delete task - [title]'\n\n" +
                          "**Follow-up questions:**\n" +
                          "  • 'Tell me more'\n" +
                          "  • 'Another tip'\n\n" +
                          "🧠 I remember your name and interests!";

            AppendMessage(help, "🛡️ CyberBot", Color.LightGreen);
        }

        // ========== TASK ASSISTANT BUTTON (Part 3, Task 1) ========== //
        private void BtnTasks_Click(object sender, EventArgs e)
        {
            var tasksForm = new TasksForm(chatbot.GetTaskManager());
            tasksForm.Show();
        }

        // ========== QUIZ BUTTON (Part 3, Task 2) ========== //
        private void BtnQuiz_Click(object sender, EventArgs e)
        {
            var quizForm = new QuizForm(chatbot.GetActivityLogger());
            quizForm.Show();
        }

        private void UpdateUserInfoDisplay()
        {
            string userName = chatbot.GetUserName();
            string userInterest = chatbot.GetUserInterest();

            if (!string.IsNullOrEmpty(userName))
            {
                lblUserInfo.Text = $"👤 User: {userName}\n";
                if (!string.IsNullOrEmpty(userInterest))
                {
                    lblUserInfo.Text += $"📚 Interest: {userInterest}";
                }
            }
            else
            {
                lblUserInfo.Text = "👤 User info will appear here\n(Start chatting!)";
            }
        }
    }
}