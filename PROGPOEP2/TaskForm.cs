using System;
using System.Drawing;
using System.Windows.Forms;

namespace PROGPOEP2
{
    public class TasksForm : Form
    {
        private TaskManager taskManager;
        private DataGridView dgvTasks;
        private Button btnAddTask;
        private Button btnComplete;
        private Button btnDelete;
        private Button btnRefresh;
        private Label lblTitle;

        public TasksForm(TaskManager sharedTaskManager)
        {
            taskManager = sharedTaskManager;
            InitializeComponents();
            LoadTasks();
        }

        private void InitializeComponents()
        {
            // Form settings
            this.Text = "Cybersecurity Task Assistant";
            this.Size = new Size(800, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(18, 18, 30);
            this.MinimumSize = new Size(700, 450);

            // Title label
            lblTitle = new Label
            {
                Text = "📋 My Cybersecurity Tasks",
                ForeColor = Color.FromArgb(0, 200, 150),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };

            // DataGridView
            dgvTasks = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(750, 380),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackgroundColor = Color.FromArgb(28, 28, 45),
                ForeColor = Color.White,
                GridColor = Color.FromArgb(50, 50, 80),
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 10)
            };

            // Style the header
            dgvTasks.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 150, 120);
            dgvTasks.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTasks.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTasks.ColumnHeadersHeight = 35;

            // Style rows
            dgvTasks.DefaultCellStyle.BackColor = Color.FromArgb(28, 28, 45);
            dgvTasks.DefaultCellStyle.ForeColor = Color.White;
            dgvTasks.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 180, 140);
            dgvTasks.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvTasks.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(35, 35, 55);
            dgvTasks.RowTemplate.Height = 30;

            // Define columns
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                Width = 45,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTitle",
                HeaderText = "Title",
                FillWeight = 30
            });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDescription",
                HeaderText = "Description",
                FillWeight = 40
            });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colReminder",
                HeaderText = "Reminder",
                FillWeight = 20
            });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Status",
                FillWeight = 10
            });

            // Buttons panel
            Panel btnPanel = new Panel
            {
                Location = new Point(20, 450),
                Size = new Size(750, 45),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent
            };

            btnAddTask = CreateButton("➕ Add Task", Color.FromArgb(0, 150, 120), 0);
            btnComplete = CreateButton("✅ Mark Complete", Color.FromArgb(0, 120, 180), 160);
            btnDelete = CreateButton("🗑️ Delete", Color.FromArgb(180, 50, 50), 330);
            btnRefresh = CreateButton("🔄 Refresh", Color.FromArgb(80, 80, 120), 460);

            btnAddTask.Click += BtnAddTask_Click;
            btnComplete.Click += BtnComplete_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += (s, e) => LoadTasks();

            btnPanel.Controls.AddRange(new Control[] { btnAddTask, btnComplete, btnDelete, btnRefresh });

            this.Controls.AddRange(new Control[] { lblTitle, dgvTasks, btnPanel });
        }

        private Button CreateButton(string text, Color backColor, int x)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, 0),
                Size = new Size(150, 38),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
        }

        private void LoadTasks()
        {
            dgvTasks.Rows.Clear();
            var tasks = taskManager.GetAllTasks();

            foreach (var task in tasks)
            {
                string status = task.IsCompleted ? "✅ Done" : "⬜ Pending";
                string reminder = task.ReminderDate.HasValue
                    ? task.ReminderDate.Value.ToString("dd MMM yyyy")
                    : "None";

                dgvTasks.Rows.Add(
                    task.TaskId,
                    task.Title,
                    task.Description,
                    reminder,
                    status
                );
            }

            // Grey out completed rows
            foreach (DataGridViewRow row in dgvTasks.Rows)
            {
                if (row.Cells["colStatus"].Value?.ToString() == "✅ Done")
                {
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Strikeout);
                }
            }
        }

        private void BtnAddTask_Click(object? sender, EventArgs e)
        {
            using var addForm = new AddTaskDialog(taskManager);
            if (addForm.ShowDialog() == DialogResult.OK)
                LoadTasks();
        }

        private void BtnComplete_Click(object? sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to mark as complete.",
                    "No Task Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int taskId = Convert.ToInt32(dgvTasks.SelectedRows[0].Cells["colId"].Value);
            string title = dgvTasks.SelectedRows[0].Cells["colTitle"].Value?.ToString() ?? "";

            var confirm = MessageBox.Show($"Mark '{title}' as completed?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                taskManager.GetAllTasks(); // ensures repo is fresh
                // Mark directly via repository through TaskManager
                var tasks = taskManager.GetAllTasks();
                var match = tasks.Find(t => t.TaskId == taskId);
                if (match != null)
                {
                    taskManager.CompleteTaskByTitle(match.Title, out _);
                    LoadTasks();
                }
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to delete.",
                    "No Task Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string title = dgvTasks.SelectedRows[0].Cells["colTitle"].Value?.ToString() ?? "";

            var confirm = MessageBox.Show($"Delete '{title}'? This cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                taskManager.DeleteTaskByTitle(title, out _);
                LoadTasks();
            }
        }
    }
}