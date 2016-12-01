namespace ChatClient.Forms
{
    partial class LogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogIn));
            this.label3 = new System.Windows.Forms.Label();
            this.tbUserPass = new System.Windows.Forms.TextBox();
            this.bCloseProgram = new System.Windows.Forms.Button();
            this.bOkay = new System.Windows.Forms.Button();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 21);
            this.label3.TabIndex = 13;
            this.label3.Text = "Введите логин:";
            // 
            // tbUserPass
            // 
            this.tbUserPass.BackColor = System.Drawing.Color.SkyBlue;
            this.tbUserPass.ForeColor = System.Drawing.Color.White;
            this.tbUserPass.Location = new System.Drawing.Point(16, 86);
            this.tbUserPass.Name = "tbUserPass";
            this.tbUserPass.Size = new System.Drawing.Size(278, 26);
            this.tbUserPass.TabIndex = 10;
            // 
            // bCloseProgram
            // 
            this.bCloseProgram.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCloseProgram.Location = new System.Drawing.Point(16, 156);
            this.bCloseProgram.Name = "bCloseProgram";
            this.bCloseProgram.Size = new System.Drawing.Size(278, 32);
            this.bCloseProgram.TabIndex = 12;
            this.bCloseProgram.Text = "Выход";
            this.bCloseProgram.UseVisualStyleBackColor = true;
            this.bCloseProgram.Click += new System.EventHandler(this.bCloseProgram_Click);
            // 
            // bOkay
            // 
            this.bOkay.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOkay.Location = new System.Drawing.Point(16, 118);
            this.bOkay.Name = "bOkay";
            this.bOkay.Size = new System.Drawing.Size(278, 32);
            this.bOkay.TabIndex = 11;
            this.bOkay.Text = "Вход";
            this.bOkay.UseVisualStyleBackColor = true;
            this.bOkay.Click += new System.EventHandler(this.bOkay_Click);
            // 
            // tbUserName
            // 
            this.tbUserName.BackColor = System.Drawing.Color.SkyBlue;
            this.tbUserName.ForeColor = System.Drawing.Color.White;
            this.tbUserName.Location = new System.Drawing.Point(16, 33);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(278, 26);
            this.tbUserName.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "Введите пароль:";
            // 
            // LogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(312, 206);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbUserPass);
            this.Controls.Add(this.bCloseProgram);
            this.Controls.Add(this.bOkay);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "LogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUserPass;
        private System.Windows.Forms.Button bCloseProgram;
        private System.Windows.Forms.Button bOkay;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label2;
    }
}