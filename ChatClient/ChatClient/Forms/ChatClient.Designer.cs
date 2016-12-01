namespace ChatClient.Forms
{
    partial class ChatClient
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatClient));
            this.label1 = new System.Windows.Forms.Label();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.messageData = new System.Windows.Forms.TextBox();
            this.tabControlChat = new System.Windows.Forms.TabControl();
            this.tabPageListOfUsers = new System.Windows.Forms.TabPage();
            this.userList = new System.Windows.Forms.ListBox();
            this.tabPageFriends = new System.Windows.Forms.TabPage();
            this.nicknameDelete = new System.Windows.Forms.TextBox();
            this.bDeleteFriend = new System.Windows.Forms.Button();
            this.bUpdateFriends = new System.Windows.Forms.Button();
            this.friendList = new System.Windows.Forms.ListBox();
            this.nicknameData = new System.Windows.Forms.TextBox();
            this.enterChat = new System.Windows.Forms.Button();
            this.nameData = new System.Windows.Forms.Label();
            this.userMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bLogIng = new System.Windows.Forms.Button();
            this.bHelp = new System.Windows.Forms.Button();
            this.helpProviderChat = new System.Windows.Forms.HelpProvider();
            this.tabControlChat.SuspendLayout();
            this.tabPageListOfUsers.SuspendLayout();
            this.tabPageFriends.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ваш никнейм:";
            // 
            // chatBox
            // 
            this.chatBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatBox.Location = new System.Drawing.Point(240, 79);
            this.chatBox.Name = "chatBox";
            this.chatBox.ReadOnly = true;
            this.chatBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.chatBox.Size = new System.Drawing.Size(502, 325);
            this.chatBox.TabIndex = 1;
            this.chatBox.Text = "";
            // 
            // messageData
            // 
            this.messageData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageData.Location = new System.Drawing.Point(16, 410);
            this.messageData.Name = "messageData";
            this.messageData.Size = new System.Drawing.Size(726, 26);
            this.messageData.TabIndex = 2;
            this.messageData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.messageData_KeyUp);
            // 
            // tabControlChat
            // 
            this.tabControlChat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlChat.Controls.Add(this.tabPageListOfUsers);
            this.tabControlChat.Controls.Add(this.tabPageFriends);
            this.tabControlChat.Location = new System.Drawing.Point(16, 79);
            this.tabControlChat.Name = "tabControlChat";
            this.tabControlChat.SelectedIndex = 0;
            this.tabControlChat.Size = new System.Drawing.Size(218, 325);
            this.tabControlChat.TabIndex = 4;
            this.tabControlChat.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControlChat_Selecting);
            // 
            // tabPageListOfUsers
            // 
            this.tabPageListOfUsers.Controls.Add(this.userList);
            this.tabPageListOfUsers.Location = new System.Drawing.Point(4, 29);
            this.tabPageListOfUsers.Name = "tabPageListOfUsers";
            this.tabPageListOfUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageListOfUsers.Size = new System.Drawing.Size(210, 292);
            this.tabPageListOfUsers.TabIndex = 0;
            this.tabPageListOfUsers.Text = "Общий";
            this.tabPageListOfUsers.UseVisualStyleBackColor = true;
            // 
            // userList
            // 
            this.userList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userList.FormattingEnabled = true;
            this.userList.ItemHeight = 20;
            this.userList.Location = new System.Drawing.Point(3, 3);
            this.userList.Name = "userList";
            this.userList.Size = new System.Drawing.Size(204, 286);
            this.userList.TabIndex = 0;
            // 
            // tabPageFriends
            // 
            this.tabPageFriends.Controls.Add(this.nicknameDelete);
            this.tabPageFriends.Controls.Add(this.bDeleteFriend);
            this.tabPageFriends.Controls.Add(this.bUpdateFriends);
            this.tabPageFriends.Controls.Add(this.friendList);
            this.tabPageFriends.Location = new System.Drawing.Point(4, 29);
            this.tabPageFriends.Name = "tabPageFriends";
            this.tabPageFriends.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFriends.Size = new System.Drawing.Size(210, 292);
            this.tabPageFriends.TabIndex = 1;
            this.tabPageFriends.Text = "Друзья(0)";
            this.tabPageFriends.UseVisualStyleBackColor = true;
            // 
            // nicknameDelete
            // 
            this.nicknameDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nicknameDelete.Location = new System.Drawing.Point(50, 13);
            this.nicknameDelete.Name = "nicknameDelete";
            this.nicknameDelete.Size = new System.Drawing.Size(110, 26);
            this.nicknameDelete.TabIndex = 3;
            // 
            // bDeleteFriend
            // 
            this.bDeleteFriend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bDeleteFriend.BackgroundImage")));
            this.bDeleteFriend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bDeleteFriend.Location = new System.Drawing.Point(166, 6);
            this.bDeleteFriend.Name = "bDeleteFriend";
            this.bDeleteFriend.Size = new System.Drawing.Size(38, 33);
            this.bDeleteFriend.TabIndex = 2;
            this.bDeleteFriend.UseVisualStyleBackColor = true;
            this.bDeleteFriend.Click += new System.EventHandler(this.bDeleteFriend_Click);
            // 
            // bUpdateFriends
            // 
            this.bUpdateFriends.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bUpdateFriends.BackgroundImage")));
            this.bUpdateFriends.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bUpdateFriends.Location = new System.Drawing.Point(6, 6);
            this.bUpdateFriends.Name = "bUpdateFriends";
            this.bUpdateFriends.Size = new System.Drawing.Size(38, 33);
            this.bUpdateFriends.TabIndex = 1;
            this.bUpdateFriends.UseVisualStyleBackColor = true;
            this.bUpdateFriends.Click += new System.EventHandler(this.bUpdateFriends_Click);
            // 
            // friendList
            // 
            this.friendList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.friendList.FormattingEnabled = true;
            this.friendList.HorizontalScrollbar = true;
            this.friendList.ItemHeight = 20;
            this.friendList.Location = new System.Drawing.Point(3, 45);
            this.friendList.Name = "friendList";
            this.friendList.Size = new System.Drawing.Size(204, 244);
            this.friendList.TabIndex = 0;
            // 
            // nicknameData
            // 
            this.nicknameData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nicknameData.Enabled = false;
            this.nicknameData.Location = new System.Drawing.Point(144, 9);
            this.nicknameData.Name = "nicknameData";
            this.nicknameData.Size = new System.Drawing.Size(374, 26);
            this.nicknameData.TabIndex = 5;
            // 
            // enterChat
            // 
            this.enterChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enterChat.Enabled = false;
            this.enterChat.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.enterChat.Location = new System.Drawing.Point(524, 9);
            this.enterChat.Name = "enterChat";
            this.enterChat.Size = new System.Drawing.Size(218, 26);
            this.enterChat.TabIndex = 6;
            this.enterChat.Text = "Подключится";
            this.enterChat.UseVisualStyleBackColor = true;
            this.enterChat.Click += new System.EventHandler(this.enterChat_Click);
            // 
            // nameData
            // 
            this.nameData.AutoSize = true;
            this.nameData.Location = new System.Drawing.Point(12, 41);
            this.nameData.Name = "nameData";
            this.nameData.Size = new System.Drawing.Size(126, 20);
            this.nameData.TabIndex = 7;
            this.nameData.Text = "Ваш никнейм:";
            // 
            // userMenu
            // 
            this.userMenu.Name = "userMenu";
            this.userMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // bLogIng
            // 
            this.bLogIng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bLogIng.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bLogIng.Location = new System.Drawing.Point(524, 41);
            this.bLogIng.Name = "bLogIng";
            this.bLogIng.Size = new System.Drawing.Size(179, 26);
            this.bLogIng.TabIndex = 9;
            this.bLogIng.Text = "Авторизация";
            this.bLogIng.UseVisualStyleBackColor = true;
            this.bLogIng.Click += new System.EventHandler(this.bLogIng_Click);
            // 
            // bHelp
            // 
            this.bHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bHelp.BackgroundImage")));
            this.bHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bHelp.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bHelp.Location = new System.Drawing.Point(709, 41);
            this.bHelp.Name = "bHelp";
            this.bHelp.Size = new System.Drawing.Size(33, 26);
            this.bHelp.TabIndex = 10;
            this.bHelp.UseVisualStyleBackColor = true;
            this.bHelp.Click += new System.EventHandler(this.bHelp_Click);
            // 
            // ChatClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(762, 453);
            this.Controls.Add(this.bHelp);
            this.Controls.Add(this.bLogIng);
            this.Controls.Add(this.nameData);
            this.Controls.Add(this.enterChat);
            this.Controls.Add(this.nicknameData);
            this.Controls.Add(this.tabControlChat);
            this.Controls.Add(this.messageData);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimumSize = new System.Drawing.Size(540, 491);
            this.Name = "ChatClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Чат, общая комната";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatClient_FormClosing);
            this.Load += new System.EventHandler(this.ChatClient_Load);
            this.tabControlChat.ResumeLayout(false);
            this.tabPageListOfUsers.ResumeLayout(false);
            this.tabPageFriends.ResumeLayout(false);
            this.tabPageFriends.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.TextBox messageData;
        private System.Windows.Forms.TabControl tabControlChat;
        private System.Windows.Forms.TabPage tabPageListOfUsers;
        private System.Windows.Forms.TabPage tabPageFriends;
        private System.Windows.Forms.ListBox userList;
        private System.Windows.Forms.ListBox friendList;
        private System.Windows.Forms.Button enterChat;
        public System.Windows.Forms.TextBox nicknameData;
        private System.Windows.Forms.Label nameData;
        private System.Windows.Forms.ContextMenuStrip userMenu;
        private System.Windows.Forms.Button bUpdateFriends;
        private System.Windows.Forms.Button bDeleteFriend;
        private System.Windows.Forms.TextBox nicknameDelete;
        private System.Windows.Forms.Button bLogIng;
        private System.Windows.Forms.Button bHelp;
        private System.Windows.Forms.HelpProvider helpProviderChat;
    }
}