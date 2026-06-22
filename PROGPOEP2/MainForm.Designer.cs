namespace PROGPOEP2
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.rtbChatDisplay = new System.Windows.Forms.RichTextBox();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.txtUserInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClearChat = new System.Windows.Forms.Button();
            this.btnNewConversation = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.btnMemory = new System.Windows.Forms.Button();
            this.btnTopics = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnTasks = new System.Windows.Forms.Button();
            this.btnQuiz = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.pictureBoxLogo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(900, 80);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(60, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(840, 80);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "🛡️ CYBERSECURITY AWARENESS BOT";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(60, 80);
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // rtbChatDisplay
            // 
            this.rtbChatDisplay.BackColor = System.Drawing.Color.FromArgb(30, 30, 45);
            this.rtbChatDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbChatDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbChatDisplay.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.rtbChatDisplay.ForeColor = System.Drawing.Color.White;
            this.rtbChatDisplay.Location = new System.Drawing.Point(200, 80);
            this.rtbChatDisplay.Name = "rtbChatDisplay";
            this.rtbChatDisplay.ReadOnly = true;
            this.rtbChatDisplay.Size = new System.Drawing.Size(700, 440);
            this.rtbChatDisplay.TabIndex = 2;
            this.rtbChatDisplay.Text = "";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(35, 35, 50);
            this.panelFooter.Controls.Add(this.txtUserInput);
            this.panelFooter.Controls.Add(this.btnSend);
            this.panelFooter.Controls.Add(this.btnClearChat);
            this.panelFooter.Controls.Add(this.btnNewConversation);
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(200, 520);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(700, 100);
            this.panelFooter.TabIndex = 3;
            // 
            // txtUserInput
            // 
            this.txtUserInput.BackColor = System.Drawing.Color.FromArgb(50, 50, 70);
            this.txtUserInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserInput.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.txtUserInput.ForeColor = System.Drawing.Color.White;
            this.txtUserInput.Location = new System.Drawing.Point(10, 15);
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.Size = new System.Drawing.Size(450, 27);
            this.txtUserInput.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(470, 14);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 30);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = false;
            // 
            // btnClearChat
            // 
            this.btnClearChat.BackColor = System.Drawing.Color.FromArgb(200, 50, 50);
            this.btnClearChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearChat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClearChat.ForeColor = System.Drawing.Color.White;
            this.btnClearChat.Location = new System.Drawing.Point(580, 14);
            this.btnClearChat.Name = "btnClearChat";
            this.btnClearChat.Size = new System.Drawing.Size(110, 30);
            this.btnClearChat.TabIndex = 2;
            this.btnClearChat.Text = "CLEAR";
            this.btnClearChat.UseVisualStyleBackColor = false;
            // 
            // btnNewConversation
            // 
            this.btnNewConversation.BackColor = System.Drawing.Color.FromArgb(75, 75, 95);
            this.btnNewConversation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewConversation.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNewConversation.ForeColor = System.Drawing.Color.White;
            this.btnNewConversation.Location = new System.Drawing.Point(580, 55);
            this.btnNewConversation.Name = "btnNewConversation";
            this.btnNewConversation.Size = new System.Drawing.Size(110, 30);
            this.btnNewConversation.TabIndex = 3;
            this.btnNewConversation.Text = "NEW";
            this.btnNewConversation.UseVisualStyleBackColor = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.Color.LightGray;
            this.lblStatus.Location = new System.Drawing.Point(10, 60);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(97, 15);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "🤖 Bot is ready";
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(25, 25, 40);
            this.panelSidebar.Controls.Add(this.lblUserInfo);
            this.panelSidebar.Controls.Add(this.btnMemory);
            this.panelSidebar.Controls.Add(this.btnTopics);
            this.panelSidebar.Controls.Add(this.btnHelp);
            this.panelSidebar.Controls.Add(this.btnTasks);
            this.panelSidebar.Controls.Add(this.btnQuiz);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 80);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(200, 540);
            this.panelSidebar.TabIndex = 4;
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblUserInfo.ForeColor = System.Drawing.Color.LightGray;
            this.lblUserInfo.Location = new System.Drawing.Point(10, 350);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(180, 80);
            this.lblUserInfo.TabIndex = 5;
            this.lblUserInfo.Text = "👤 User info will appear here";
            // 
            // btnMemory
            // 
            this.btnMemory.BackColor = System.Drawing.Color.FromArgb(45, 45, 60);
            this.btnMemory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMemory.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.btnMemory.ForeColor = System.Drawing.Color.White;
            this.btnMemory.Location = new System.Drawing.Point(10, 20);
            this.btnMemory.Name = "btnMemory";
            this.btnMemory.Size = new System.Drawing.Size(180, 40);
            this.btnMemory.TabIndex = 0;
            this.btnMemory.Text = "🧠 Recall Memory";
            this.btnMemory.UseVisualStyleBackColor = false;
            // 
            // btnTopics
            // 
            this.btnTopics.BackColor = System.Drawing.Color.FromArgb(45, 45, 60);
            this.btnTopics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTopics.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.btnTopics.ForeColor = System.Drawing.Color.White;
            this.btnTopics.Location = new System.Drawing.Point(10, 75);
            this.btnTopics.Name = "btnTopics";
            this.btnTopics.Size = new System.Drawing.Size(180, 40);
            this.btnTopics.TabIndex = 1;
            this.btnTopics.Text = "📚 Topics";
            this.btnTopics.UseVisualStyleBackColor = false;
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.FromArgb(45, 45, 60);
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.btnHelp.ForeColor = System.Drawing.Color.White;
            this.btnHelp.Location = new System.Drawing.Point(10, 130);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(180, 40);
            this.btnHelp.TabIndex = 2;
            this.btnHelp.Text = "❓ Help";
            this.btnHelp.UseVisualStyleBackColor = false;
            // 
            // btnTasks
            // 
            this.btnTasks.BackColor = System.Drawing.Color.FromArgb(0, 130, 100);
            this.btnTasks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTasks.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.btnTasks.ForeColor = System.Drawing.Color.White;
            this.btnTasks.Location = new System.Drawing.Point(10, 185);
            this.btnTasks.Name = "btnTasks";
            this.btnTasks.Size = new System.Drawing.Size(180, 40);
            this.btnTasks.TabIndex = 3;
            this.btnTasks.Text = "📋 Tasks";
            this.btnTasks.UseVisualStyleBackColor = false;
            // 
            // btnQuiz
            // 
            this.btnQuiz.BackColor = System.Drawing.Color.FromArgb(100, 60, 180);
            this.btnQuiz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuiz.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.btnQuiz.ForeColor = System.Drawing.Color.White;
            this.btnQuiz.Location = new System.Drawing.Point(10, 240);
            this.btnQuiz.Name = "btnQuiz";
            this.btnQuiz.Size = new System.Drawing.Size(180, 40);
            this.btnQuiz.TabIndex = 4;
            this.btnQuiz.Text = "🎯 Quiz";
            this.btnQuiz.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(20, 20, 35);
            this.ClientSize = new System.Drawing.Size(900, 620);
            this.Controls.Add(this.rtbChatDisplay);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cybersecurity Awareness Bot";
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.RichTextBox rtbChatDisplay;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.TextBox txtUserInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClearChat;
        private System.Windows.Forms.Button btnNewConversation;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Button btnMemory;
        private System.Windows.Forms.Button btnTopics;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnTasks;
        private System.Windows.Forms.Button btnQuiz;
    }
}