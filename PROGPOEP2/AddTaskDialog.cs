using System;
using System.Drawing;
using System.Windows.Forms;

namespace PROGPOEP2
{
    public class AddTaskDialog : Form
    {
        private TaskManager taskManager;
        private TextBox txtTitle;
        private TextBox txtDescription;
        private TextBox txtReminder;
        private Button btnSave;
        private Button btnCancel;

        public AddTaskDialog(TaskManager sharedTaskManager)
        {
            taskManager = sharedTaskManager;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Add New Task";
            this.Size = new Size(450, 320);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(18, 18, 30);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label lblTitle = CreateLabel("Task Title:", 20);
            txtTitle = CreateTextBox(50);

            Label lblDesc = CreateLabel("Description (optional):", 100);
            txtDescription = CreateTextBox(130);

            Label lblReminder = CreateLabel("Reminder (e.g. 'in 3 days', 'tomorrow', or leave blank):", 180);
            txtReminder = CreateTextBox(210);

            btnSave = new Button
            {
                Text = "Save Task",
                Location = new Point(220, 260),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(0, 150, 120),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                DialogResult = DialogResult.None,
                FlatAppearance = { BorderSize = 0 }
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(330, 260),
                Size = new Size(90, 35),
                BackColor = Color.FromArgb(80, 80, 120),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                DialogResult = DialogResult.Cancel,
                FlatAppearance = { BorderSize = 0 }
            };

            btnSave.Click += BtnSave_Click;

            this.Controls.AddRange(new Control[]
            {
                lblTitle, txtTitle,
                lblDesc, txtDescription,
                lblReminder, txtReminder,
                btnSave, btnCancel
            });
        }

        private Label CreateLabel(string text, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(20, y),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 200, 150),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
        }

        private TextBox CreateTextBox(int y)
        {
            return new TextBox
            {
                Location = new Point(20, y),
                Size = new Size(400, 28),
                BackColor = Color.FromArgb(35, 35, 55),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10)
            };
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a task title.",
                    "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string title = txtTitle.Text.Trim();
            string description = string.IsNullOrWhiteSpace(txtDescription.Text)
                ? $"{title} to ensure your data is protected."
                : txtDescription.Text.Trim();

            var task = taskManager.AddTask(title, description);

            // Handle optional reminder
            if (!string.IsNullOrWhiteSpace(txtReminder.Text))
            {
                if (taskManager.SetReminder(task.TaskId, txtReminder.Text, out DateTime resolved))
                    MessageBox.Show($"Task added! Reminder set for {resolved:dd MMM yyyy}.",
                        "Task Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Task added, but the reminder date wasn't recognised. Try 'in 3 days' or 'tomorrow'.",
                        "Task Saved", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Task added successfully!", "Task Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}