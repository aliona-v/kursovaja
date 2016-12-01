namespace ChatClient.Forms
{
    partial class AddEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEdit));
            this.bCancel = new System.Windows.Forms.Button();
            this.tB2 = new System.Windows.Forms.TextBox();
            this.lb2 = new System.Windows.Forms.Label();
            this.tB1 = new System.Windows.Forms.TextBox();
            this.lb1 = new System.Windows.Forms.Label();
            this.bOkay = new System.Windows.Forms.Button();
            this.comboB2 = new System.Windows.Forms.ComboBox();
            this.comboB1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCancel.ForeColor = System.Drawing.Color.Black;
            this.bCancel.Location = new System.Drawing.Point(16, 162);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(434, 30);
            this.bCancel.TabIndex = 14;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // tB2
            // 
            this.tB2.BackColor = System.Drawing.Color.SkyBlue;
            this.tB2.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tB2.ForeColor = System.Drawing.Color.White;
            this.tB2.Location = new System.Drawing.Point(16, 90);
            this.tB2.Name = "tB2";
            this.tB2.Size = new System.Drawing.Size(434, 30);
            this.tB2.TabIndex = 12;
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb2.ForeColor = System.Drawing.Color.Black;
            this.lb2.Location = new System.Drawing.Point(12, 66);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(67, 21);
            this.lb2.TabIndex = 16;
            this.lb2.Text = "label2";
            // 
            // tB1
            // 
            this.tB1.BackColor = System.Drawing.Color.SkyBlue;
            this.tB1.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tB1.ForeColor = System.Drawing.Color.White;
            this.tB1.Location = new System.Drawing.Point(16, 33);
            this.tB1.Name = "tB1";
            this.tB1.Size = new System.Drawing.Size(434, 30);
            this.tB1.TabIndex = 11;
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb1.ForeColor = System.Drawing.Color.Black;
            this.lb1.Location = new System.Drawing.Point(12, 9);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(67, 21);
            this.lb1.TabIndex = 15;
            this.lb1.Text = "label1";
            // 
            // bOkay
            // 
            this.bOkay.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOkay.ForeColor = System.Drawing.Color.Black;
            this.bOkay.Location = new System.Drawing.Point(16, 126);
            this.bOkay.Name = "bOkay";
            this.bOkay.Size = new System.Drawing.Size(434, 30);
            this.bOkay.TabIndex = 13;
            this.bOkay.Text = "Подтвердить";
            this.bOkay.UseVisualStyleBackColor = true;
            this.bOkay.Click += new System.EventHandler(this.bOkay_Click);
            // 
            // comboB2
            // 
            this.comboB2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboB2.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboB2.FormattingEnabled = true;
            this.comboB2.Location = new System.Drawing.Point(16, 91);
            this.comboB2.Name = "comboB2";
            this.comboB2.Size = new System.Drawing.Size(434, 29);
            this.comboB2.TabIndex = 17;
            this.comboB2.Visible = false;
            // 
            // comboB1
            // 
            this.comboB1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboB1.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboB1.FormattingEnabled = true;
            this.comboB1.Location = new System.Drawing.Point(16, 33);
            this.comboB1.Name = "comboB1";
            this.comboB1.Size = new System.Drawing.Size(434, 29);
            this.comboB1.TabIndex = 18;
            this.comboB1.Visible = false;
            // 
            // AddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(462, 215);
            this.Controls.Add(this.comboB1);
            this.Controls.Add(this.comboB2);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.tB2);
            this.Controls.Add(this.lb2);
            this.Controls.Add(this.tB1);
            this.Controls.Add(this.lb1);
            this.Controls.Add(this.bOkay);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddEdit";
            this.Load += new System.EventHandler(this.AddEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bCancel;
        public System.Windows.Forms.TextBox tB2;
        private System.Windows.Forms.Label lb2;
        public System.Windows.Forms.TextBox tB1;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Button bOkay;
        public System.Windows.Forms.ComboBox comboB2;
        public System.Windows.Forms.ComboBox comboB1;
    }
}