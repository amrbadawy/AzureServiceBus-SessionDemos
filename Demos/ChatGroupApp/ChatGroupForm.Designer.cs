namespace ChatGroupApp
{
    partial class ChatGroupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatGroupForm));
            this.pnlAuthArea = new System.Windows.Forms.Panel();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lstChat = new System.Windows.Forms.ListBox();
            this.btnSignInOut = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.btnLoadUsers = new System.Windows.Forms.Button();
            this.btnBlockUsers = new System.Windows.Forms.Button();
            this.btnUnblockAll = new System.Windows.Forms.Button();
            this.chkEnableEncryption = new System.Windows.Forms.CheckBox();
            this.txtCipherKey = new System.Windows.Forms.TextBox();
            this.btnAddEncryptKey = new System.Windows.Forms.Button();
            this.btnRemoveEncryptKey = new System.Windows.Forms.Button();
            this.btnDisableSending = new System.Windows.Forms.Button();
            this.pnlAuthArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAuthArea
            // 
            this.pnlAuthArea.Controls.Add(this.btnSendMessage);
            this.pnlAuthArea.Controls.Add(this.txtMessage);
            this.pnlAuthArea.Controls.Add(this.lstChat);
            this.pnlAuthArea.Enabled = false;
            this.pnlAuthArea.Location = new System.Drawing.Point(9, 47);
            this.pnlAuthArea.Name = "pnlAuthArea";
            this.pnlAuthArea.Size = new System.Drawing.Size(350, 328);
            this.pnlAuthArea.TabIndex = 5;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(262, 298);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(85, 23);
            this.btnSendMessage.TabIndex = 7;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.AcceptsReturn = true;
            this.txtMessage.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(3, 298);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(253, 23);
            this.txtMessage.TabIndex = 6;
            // 
            // lstChat
            // 
            this.lstChat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstChat.BackColor = System.Drawing.Color.AliceBlue;
            this.lstChat.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstChat.FormattingEnabled = true;
            this.lstChat.ItemHeight = 15;
            this.lstChat.Location = new System.Drawing.Point(0, 0);
            this.lstChat.Name = "lstChat";
            this.lstChat.ScrollAlwaysVisible = true;
            this.lstChat.Size = new System.Drawing.Size(347, 289);
            this.lstChat.TabIndex = 5;
            // 
            // btnSignInOut
            // 
            this.btnSignInOut.Location = new System.Drawing.Point(240, 12);
            this.btnSignInOut.Name = "btnSignInOut";
            this.btnSignInOut.Size = new System.Drawing.Size(90, 23);
            this.btnSignInOut.TabIndex = 7;
            this.btnSignInOut.Text = "Sign in";
            this.btnSignInOut.UseVisualStyleBackColor = true;
            this.btnSignInOut.Click += new System.EventHandler(this.btnSignInOut_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.AcceptsReturn = true;
            this.txtUsername.Location = new System.Drawing.Point(11, 12);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(208, 23);
            this.txtUsername.TabIndex = 6;
            // 
            // lstUsers
            // 
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.ItemHeight = 15;
            this.lstUsers.Location = new System.Drawing.Point(368, 47);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstUsers.Size = new System.Drawing.Size(94, 109);
            this.lstUsers.TabIndex = 8;
            // 
            // btnLoadUsers
            // 
            this.btnLoadUsers.Enabled = false;
            this.btnLoadUsers.Location = new System.Drawing.Point(368, 171);
            this.btnLoadUsers.Name = "btnLoadUsers";
            this.btnLoadUsers.Size = new System.Drawing.Size(94, 23);
            this.btnLoadUsers.TabIndex = 9;
            this.btnLoadUsers.Text = "Get Users";
            this.btnLoadUsers.UseVisualStyleBackColor = true;
            this.btnLoadUsers.Click += new System.EventHandler(this.btnLoadUsers_Click);
            // 
            // btnBlockUsers
            // 
            this.btnBlockUsers.Enabled = false;
            this.btnBlockUsers.Location = new System.Drawing.Point(368, 200);
            this.btnBlockUsers.Name = "btnBlockUsers";
            this.btnBlockUsers.Size = new System.Drawing.Size(94, 23);
            this.btnBlockUsers.TabIndex = 9;
            this.btnBlockUsers.Text = "Block Users";
            this.btnBlockUsers.UseVisualStyleBackColor = true;
            this.btnBlockUsers.Click += new System.EventHandler(this.btnBlockUsers_Click);
            // 
            // btnUnblockAll
            // 
            this.btnUnblockAll.Enabled = false;
            this.btnUnblockAll.Location = new System.Drawing.Point(368, 229);
            this.btnUnblockAll.Name = "btnUnblockAll";
            this.btnUnblockAll.Size = new System.Drawing.Size(94, 23);
            this.btnUnblockAll.TabIndex = 10;
            this.btnUnblockAll.Text = "Unblock All";
            this.btnUnblockAll.UseVisualStyleBackColor = true;
            this.btnUnblockAll.Click += new System.EventHandler(this.btnUnblockAll_Click);
            // 
            // chkEnableEncryption
            // 
            this.chkEnableEncryption.AutoSize = true;
            this.chkEnableEncryption.Location = new System.Drawing.Point(373, 327);
            this.chkEnableEncryption.Name = "chkEnableEncryption";
            this.chkEnableEncryption.Size = new System.Drawing.Size(75, 19);
            this.chkEnableEncryption.TabIndex = 11;
            this.chkEnableEncryption.Text = "Encrypt";
            this.chkEnableEncryption.UseVisualStyleBackColor = true;
            this.chkEnableEncryption.CheckedChanged += new System.EventHandler(this.chkEnableEncryption_CheckedChanged);
            // 
            // txtCipherKey
            // 
            this.txtCipherKey.Location = new System.Drawing.Point(373, 345);
            this.txtCipherKey.Name = "txtCipherKey";
            this.txtCipherKey.Size = new System.Drawing.Size(85, 23);
            this.txtCipherKey.TabIndex = 12;
            this.txtCipherKey.Text = "987654";
            this.txtCipherKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCipherKey.Visible = false;
            this.txtCipherKey.TextChanged += new System.EventHandler(this.chkEnableEncryption_CheckedChanged);
            // 
            // btnAddEncryptKey
            // 
            this.btnAddEncryptKey.Enabled = false;
            this.btnAddEncryptKey.Location = new System.Drawing.Point(368, 263);
            this.btnAddEncryptKey.Name = "btnAddEncryptKey";
            this.btnAddEncryptKey.Size = new System.Drawing.Size(94, 23);
            this.btnAddEncryptKey.TabIndex = 14;
            this.btnAddEncryptKey.Text = "Add EKey";
            this.btnAddEncryptKey.UseVisualStyleBackColor = true;
            this.btnAddEncryptKey.Click += new System.EventHandler(this.BtnAddEncryptKey_Click);
            // 
            // btnRemoveEncryptKey
            // 
            this.btnRemoveEncryptKey.Enabled = false;
            this.btnRemoveEncryptKey.Location = new System.Drawing.Point(368, 292);
            this.btnRemoveEncryptKey.Name = "btnRemoveEncryptKey";
            this.btnRemoveEncryptKey.Size = new System.Drawing.Size(94, 23);
            this.btnRemoveEncryptKey.TabIndex = 15;
            this.btnRemoveEncryptKey.Text = "Remove EKey";
            this.btnRemoveEncryptKey.UseVisualStyleBackColor = true;
            this.btnRemoveEncryptKey.Click += new System.EventHandler(this.BtnRemoveEncryptKey_Click);
            // 
            // btnDisableSending
            // 
            this.btnDisableSending.Location = new System.Drawing.Point(368, 12);
            this.btnDisableSending.Name = "btnDisableSending";
            this.btnDisableSending.Size = new System.Drawing.Size(90, 23);
            this.btnDisableSending.TabIndex = 16;
            this.btnDisableSending.Text = "Disable Sending";
            this.btnDisableSending.UseVisualStyleBackColor = true;
            this.btnDisableSending.Click += new System.EventHandler(this.btnDisableSending_Click);
            // 
            // ChatGroupForm
            // 
            this.AcceptButton = this.btnSignInOut;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 383);
            this.Controls.Add(this.btnDisableSending);
            this.Controls.Add(this.btnRemoveEncryptKey);
            this.Controls.Add(this.btnAddEncryptKey);
            this.Controls.Add(this.txtCipherKey);
            this.Controls.Add(this.chkEnableEncryption);
            this.Controls.Add(this.btnUnblockAll);
            this.Controls.Add(this.btnBlockUsers);
            this.Controls.Add(this.btnLoadUsers);
            this.Controls.Add(this.lstUsers);
            this.Controls.Add(this.btnSignInOut);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.pnlAuthArea);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ChatGroupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat Group";
            this.pnlAuthArea.ResumeLayout(false);
            this.pnlAuthArea.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlAuthArea;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ListBox lstChat;
        private System.Windows.Forms.Button btnSignInOut;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.ListBox lstUsers;
        private System.Windows.Forms.Button btnLoadUsers;
        private System.Windows.Forms.Button btnBlockUsers;
        private System.Windows.Forms.Button btnUnblockAll;
        private System.Windows.Forms.CheckBox chkEnableEncryption;
        private System.Windows.Forms.TextBox txtCipherKey;
        private System.Windows.Forms.Button btnAddEncryptKey;
        private System.Windows.Forms.Button btnRemoveEncryptKey;
        private System.Windows.Forms.Button btnDisableSending;
    }
}