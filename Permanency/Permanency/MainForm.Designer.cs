namespace Permanency
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MsgInputBox = new System.Windows.Forms.TextBox();
            this.SendMessageBtn = new System.Windows.Forms.Button();
            this.StatusLbl = new System.Windows.Forms.Label();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.OutputTextBox = new System.Windows.Forms.RichTextBox();
            this.OnlineUsersLabel = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.DisconnectBtn = new System.Windows.Forms.Button();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.EmailTextBox = new System.Windows.Forms.TextBox();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.MainGroupBox = new System.Windows.Forms.GroupBox();
            this.UserCountLabel = new System.Windows.Forms.Label();
            this.MainGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MsgInputBox
            // 
            resources.ApplyResources(this.MsgInputBox, "MsgInputBox");
            this.MsgInputBox.Name = "MsgInputBox";
            // 
            // SendMessageBtn
            // 
            resources.ApplyResources(this.SendMessageBtn, "SendMessageBtn");
            this.SendMessageBtn.Name = "SendMessageBtn";
            this.SendMessageBtn.UseVisualStyleBackColor = true;
            this.SendMessageBtn.Click += new System.EventHandler(this.SendMessageBtn_Click);
            // 
            // StatusLbl
            // 
            resources.ApplyResources(this.StatusLbl, "StatusLbl");
            this.StatusLbl.Name = "StatusLbl";
            // 
            // ConnectBtn
            // 
            resources.ApplyResources(this.ConnectBtn, "ConnectBtn");
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.AcceptsTab = true;
            resources.ApplyResources(this.OutputTextBox, "OutputTextBox");
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            this.OutputTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.OutputTextBox_LinkClicked);
            // 
            // OnlineUsersLabel
            // 
            resources.ApplyResources(this.OnlineUsersLabel, "OnlineUsersLabel");
            this.OnlineUsersLabel.Name = "OnlineUsersLabel";
            // 
            // IPTextBox
            // 
            resources.ApplyResources(this.IPTextBox, "IPTextBox");
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Enter += new System.EventHandler(this.IPTextBox_Enter);
            this.IPTextBox.Leave += new System.EventHandler(this.IPTextBox_Leave);
            // 
            // DisconnectBtn
            // 
            resources.ApplyResources(this.DisconnectBtn, "DisconnectBtn");
            this.DisconnectBtn.Name = "DisconnectBtn";
            this.DisconnectBtn.UseVisualStyleBackColor = true;
            this.DisconnectBtn.Click += new System.EventHandler(this.DisconnectBtn_Click);
            // 
            // PortTextBox
            // 
            resources.ApplyResources(this.PortTextBox, "PortTextBox");
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Enter += new System.EventHandler(this.PortTextBox_Enter);
            this.PortTextBox.Leave += new System.EventHandler(this.PortTextBox_Leave);
            // 
            // LoginButton
            // 
            resources.ApplyResources(this.LoginButton, "LoginButton");
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // RegisterButton
            // 
            resources.ApplyResources(this.RegisterButton, "RegisterButton");
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // UsernameTextBox
            // 
            resources.ApplyResources(this.UsernameTextBox, "UsernameTextBox");
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Enter += new System.EventHandler(this.UsernameTextBox_Enter);
            this.UsernameTextBox.Leave += new System.EventHandler(this.UsernameTextBox_Leave);
            // 
            // PasswordTextBox
            // 
            resources.ApplyResources(this.PasswordTextBox, "PasswordTextBox");
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Enter += new System.EventHandler(this.PasswordTextBox_Enter);
            this.PasswordTextBox.Leave += new System.EventHandler(this.PasswordTextBox_Leave);
            // 
            // EmailTextBox
            // 
            resources.ApplyResources(this.EmailTextBox, "EmailTextBox");
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.Enter += new System.EventHandler(this.EmailTextBox_Enter);
            this.EmailTextBox.Leave += new System.EventHandler(this.EmailTextBox_Leave);
            // 
            // ErrorLabel
            // 
            resources.ApplyResources(this.ErrorLabel, "ErrorLabel");
            this.ErrorLabel.Name = "ErrorLabel";
            // 
            // MainGroupBox
            // 
            resources.ApplyResources(this.MainGroupBox, "MainGroupBox");
            this.MainGroupBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MainGroupBox.Controls.Add(this.UserCountLabel);
            this.MainGroupBox.Controls.Add(this.ErrorLabel);
            this.MainGroupBox.Controls.Add(this.EmailTextBox);
            this.MainGroupBox.Controls.Add(this.PasswordTextBox);
            this.MainGroupBox.Controls.Add(this.UsernameTextBox);
            this.MainGroupBox.Controls.Add(this.RegisterButton);
            this.MainGroupBox.Controls.Add(this.LoginButton);
            this.MainGroupBox.Controls.Add(this.PortTextBox);
            this.MainGroupBox.Controls.Add(this.DisconnectBtn);
            this.MainGroupBox.Controls.Add(this.IPTextBox);
            this.MainGroupBox.Controls.Add(this.OnlineUsersLabel);
            this.MainGroupBox.Controls.Add(this.OutputTextBox);
            this.MainGroupBox.Controls.Add(this.ConnectBtn);
            this.MainGroupBox.Controls.Add(this.StatusLbl);
            this.MainGroupBox.Controls.Add(this.SendMessageBtn);
            this.MainGroupBox.Controls.Add(this.MsgInputBox);
            this.MainGroupBox.Name = "MainGroupBox";
            this.MainGroupBox.TabStop = false;
            // 
            // UserCountLabel
            // 
            resources.ApplyResources(this.UserCountLabel, "UserCountLabel");
            this.UserCountLabel.Name = "UserCountLabel";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.MainGroupBox);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MainGroupBox.ResumeLayout(false);
            this.MainGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MsgInputBox;
        private System.Windows.Forms.Button SendMessageBtn;
        private System.Windows.Forms.Label StatusLbl;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.RichTextBox OutputTextBox;
        private System.Windows.Forms.Label OnlineUsersLabel;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.Button DisconnectBtn;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.TextBox EmailTextBox;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.GroupBox MainGroupBox;
        private System.Windows.Forms.Label UserCountLabel;
    }
}

