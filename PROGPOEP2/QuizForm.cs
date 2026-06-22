using System;
using System.Drawing;
using System.Windows.Forms;

namespace PROGPOEP2
{
    public class QuizForm : Form
    {
        private QuizManager quizManager;
        private Label lblProgress;
        private Label lblQuestion;
        private Panel pnlOptions;
        private Label lblFeedback;
        private Button btnNext;
        private Button btnStartOver;
        private Label lblScore;
        private EventHandler? nextQuestionHandler;  // tracks the current Next handler so we can safely remove it

        public QuizForm()
        {
            quizManager = new QuizManager();
            InitializeComponents();
            ShowStartScreen();
        }

        private void InitializeComponents()
        {
            this.Text = "Cybersecurity Quiz";
            this.Size = new Size(650, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(18, 18, 30);
            this.MinimumSize = new Size(580, 480);

            // Progress label
            lblProgress = new Label
            {
                Location = new Point(20, 15),
                Size = new Size(600, 25),
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 10),
                Text = "Press Start to begin!"
            };

            // Score label
            lblScore = new Label
            {
                Location = new Point(20, 40),
                Size = new Size(600, 25),
                ForeColor = Color.FromArgb(0, 200, 150),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Text = ""
            };

            // Question label
            lblQuestion = new Label
            {
                Location = new Point(20, 75),
                Size = new Size(600, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Text = "",
                AutoSize = false
            };

            // Options panel (buttons get added dynamically)
            pnlOptions = new Panel
            {
                Location = new Point(20, 165),
                Size = new Size(600, 200),
                BackColor = Color.Transparent
            };

            // Feedback label
            lblFeedback = new Label
            {
                Location = new Point(20, 375),
                Size = new Size(600, 55),
                ForeColor = Color.LightGreen,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                Text = ""
            };

            // Next button
            btnNext = new Button
            {
                Text = "▶ Start Quiz",
                Location = new Point(20, 440),
                Size = new Size(150, 38),
                BackColor = Color.FromArgb(0, 150, 120),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatAppearance = { BorderSize = 0 }
            };
            btnNext.Click += BtnNext_Click;

            // Start over button
            btnStartOver = new Button
            {
                Text = "🔄 Start Over",
                Location = new Point(185, 440),
                Size = new Size(150, 38),
                BackColor = Color.FromArgb(70, 70, 110),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatAppearance = { BorderSize = 0 },
                Visible = false
            };
            btnStartOver.Click += (s, e) =>
            {
                quizManager.StartQuiz();
                btnStartOver.Visible = false;
                ShowCurrentQuestion();
            };

            this.Controls.AddRange(new Control[]
            {
                lblProgress, lblScore, lblQuestion,
                pnlOptions, lblFeedback, btnNext, btnStartOver
            });
        }
        private ActivityLogger activityLogger;

        public QuizForm(ActivityLogger logger)
        {
            quizManager = new QuizManager();
            activityLogger = logger;
            InitializeComponents();
            ShowStartScreen();
        }

        private void ShowStartScreen()
        {
            lblQuestion.Text = "🛡️ Welcome to the Cybersecurity Quiz!\n\nTest your knowledge with 12 questions covering phishing, passwords, safe browsing, and more.";
            lblProgress.Text = "Ready when you are!";
            lblFeedback.Text = "";
            lblScore.Text = "";
            pnlOptions.Controls.Clear();
            btnNext.Text = "▶ Start Quiz";
            btnNext.Enabled = true;
        }

        private void BtnNext_Click(object? sender, EventArgs e)
        {
            if (!quizManager.IsQuizActive)
            {
                quizManager.StartQuiz();
                activityLogger.Log("Quiz started.");
                btnNext.Text = "▶ Start Quiz";
                ShowCurrentQuestion();
            }
        }

        private void ShowCurrentQuestion()
        {
            var q = quizManager.GetCurrentQuestion();
            if (q == null) return;

            lblProgress.Text = $"Question {quizManager.CurrentIndex + 1} of {quizManager.TotalQuestions}";
            lblScore.Text = $"Score: {quizManager.Score}";
            lblQuestion.Text = q.Question;
            lblFeedback.Text = "";
            btnNext.Visible = false;
            btnStartOver.Visible = false;

            pnlOptions.Controls.Clear();

            int y = 0;
            foreach (var option in q.Options)
            {
                var btn = new Button
                {
                    Text = option,
                    Location = new Point(0, y),
                    Size = new Size(590, 40),
                    BackColor = Color.FromArgb(40, 40, 65),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Tag = option,
                    FlatAppearance = { BorderColor = Color.FromArgb(70, 70, 100), BorderSize = 1 }
                };
                btn.Click += OptionButton_Click;
                pnlOptions.Controls.Add(btn);
                y += 47;
            }
        }

        private void OptionButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button clicked) return;

            // Disable all option buttons so user can't change answer
            foreach (Control c in pnlOptions.Controls)
                c.Enabled = false;

            string answer = clicked.Tag?.ToString() ?? "";
            bool correct = quizManager.SubmitAnswer(answer, out string explanation);

            // Highlight correct/wrong
            clicked.BackColor = correct
                ? Color.FromArgb(0, 140, 80)
                : Color.FromArgb(160, 40, 40);

            lblScore.Text = $"Score: {quizManager.Score}";
            lblFeedback.ForeColor = correct ? Color.LightGreen : Color.Salmon;
            lblFeedback.Text = correct
                ? $"✅ Correct! {explanation}"
                : $"❌ Incorrect. {explanation}";

            if (quizManager.HasMoreQuestions)
            {
                btnNext.Text = "Next Question ▶";
                btnNext.Visible = true;

                // Remove previous handler if any, then attach a fresh one
                if (nextQuestionHandler != null)
                    btnNext.Click -= nextQuestionHandler;

                nextQuestionHandler = (s, ev) =>
                {
                    btnNext.Visible = false;
                    if (nextQuestionHandler != null)
                        btnNext.Click -= nextQuestionHandler;
                    nextQuestionHandler = null;
                    ShowCurrentQuestion();
                };

                btnNext.Click += nextQuestionHandler;
            }
            else
            {
                ShowFinalScore();
            }
        }

        private void ShowFinalScore()
        {
            activityLogger.Log($"Quiz completed. Score: {quizManager.Score}/{quizManager.TotalQuestions}.");
            lblProgress.Text = "Quiz Complete!";
            lblQuestion.Text = quizManager.GetFinalFeedback();
            pnlOptions.Controls.Clear();
            lblFeedback.Text = $"You answered {quizManager.Score} out of {quizManager.TotalQuestions} questions correctly.";
            lblFeedback.ForeColor = Color.LightGreen;
            btnNext.Visible = false;
            btnStartOver.Visible = true;
        }
    }
}