namespace UDPTapChat
{
    partial class Form1
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
            this.UI_CloseProgram = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.UI_LbxIP = new System.Windows.Forms.ListBox();
            this.UI_btnFilterName = new System.Windows.Forms.Button();
            this.UI_btnFilterIP = new System.Windows.Forms.Button();
            this.lbxDisplay = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UI_btnSend = new System.Windows.Forms.Button();
            this._txtNickname = new System.Windows.Forms.TextBox();
            this._txtIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._txtMessage = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UI_CloseProgram
            // 
            this.UI_CloseProgram.BackColor = System.Drawing.Color.DarkGray;
            this.UI_CloseProgram.FlatAppearance.BorderSize = 0;
            this.UI_CloseProgram.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UI_CloseProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_CloseProgram.Location = new System.Drawing.Point(641, 3);
            this.UI_CloseProgram.Name = "UI_CloseProgram";
            this.UI_CloseProgram.Size = new System.Drawing.Size(24, 25);
            this.UI_CloseProgram.TabIndex = 5;
            this.UI_CloseProgram.TabStop = false;
            this.UI_CloseProgram.Text = "X";
            this.UI_CloseProgram.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.UI_LbxIP);
            this.panel1.Controls.Add(this.UI_btnFilterName);
            this.panel1.Controls.Add(this.UI_btnFilterIP);
            this.panel1.Controls.Add(this.lbxDisplay);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.UI_btnSend);
            this.panel1.Controls.Add(this._txtNickname);
            this.panel1.Controls.Add(this._txtIP);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this._txtMessage);
            this.panel1.Location = new System.Drawing.Point(0, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(668, 411);
            this.panel1.TabIndex = 9;
            // 
            // UI_LbxIP
            // 
            this.UI_LbxIP.BackColor = System.Drawing.Color.DarkGray;
            this.UI_LbxIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UI_LbxIP.Font = new System.Drawing.Font("MV Boli", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_LbxIP.FormattingEnabled = true;
            this.UI_LbxIP.ItemHeight = 17;
            this.UI_LbxIP.Location = new System.Drawing.Point(527, 10);
            this.UI_LbxIP.Name = "UI_LbxIP";
            this.UI_LbxIP.Size = new System.Drawing.Size(138, 391);
            this.UI_LbxIP.TabIndex = 17;
            // 
            // UI_btnFilterName
            // 
            this.UI_btnFilterName.BackColor = System.Drawing.Color.Silver;
            this.UI_btnFilterName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UI_btnFilterName.Font = new System.Drawing.Font("MV Boli", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_btnFilterName.Location = new System.Drawing.Point(3, 61);
            this.UI_btnFilterName.Name = "UI_btnFilterName";
            this.UI_btnFilterName.Size = new System.Drawing.Size(255, 28);
            this.UI_btnFilterName.TabIndex = 16;
            this.UI_btnFilterName.Text = "Filter by Nickname";
            this.UI_btnFilterName.UseVisualStyleBackColor = false;
            // 
            // UI_btnFilterIP
            // 
            this.UI_btnFilterIP.BackColor = System.Drawing.Color.Silver;
            this.UI_btnFilterIP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UI_btnFilterIP.Font = new System.Drawing.Font("MV Boli", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_btnFilterIP.Location = new System.Drawing.Point(266, 61);
            this.UI_btnFilterIP.Name = "UI_btnFilterIP";
            this.UI_btnFilterIP.Size = new System.Drawing.Size(255, 28);
            this.UI_btnFilterIP.TabIndex = 15;
            this.UI_btnFilterIP.Text = "Filter by IP";
            this.UI_btnFilterIP.UseVisualStyleBackColor = false;
            // 
            // lbxDisplay
            // 
            this.lbxDisplay.BackColor = System.Drawing.Color.DarkGray;
            this.lbxDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbxDisplay.Font = new System.Drawing.Font("MV Boli", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxDisplay.ForeColor = System.Drawing.Color.Black;
            this.lbxDisplay.FormattingEnabled = true;
            this.lbxDisplay.ItemHeight = 17;
            this.lbxDisplay.Location = new System.Drawing.Point(3, 95);
            this.lbxDisplay.Name = "lbxDisplay";
            this.lbxDisplay.Size = new System.Drawing.Size(518, 306);
            this.lbxDisplay.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MV Boli", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Nickname :";
            // 
            // UI_btnSend
            // 
            this.UI_btnSend.BackColor = System.Drawing.Color.Silver;
            this.UI_btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UI_btnSend.Font = new System.Drawing.Font("MV Boli", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_btnSend.Location = new System.Drawing.Point(446, 9);
            this.UI_btnSend.Name = "UI_btnSend";
            this.UI_btnSend.Size = new System.Drawing.Size(75, 46);
            this.UI_btnSend.TabIndex = 3;
            this.UI_btnSend.Text = "Send";
            this.UI_btnSend.UseVisualStyleBackColor = false;
            // 
            // _txtNickname
            // 
            this._txtNickname.BackColor = System.Drawing.Color.Silver;
            this._txtNickname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtNickname.Location = new System.Drawing.Point(87, 9);
            this._txtNickname.Name = "_txtNickname";
            this._txtNickname.Size = new System.Drawing.Size(100, 20);
            this._txtNickname.TabIndex = 0;
            // 
            // _txtIP
            // 
            this._txtIP.BackColor = System.Drawing.Color.Silver;
            this._txtIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtIP.Location = new System.Drawing.Point(298, 9);
            this._txtIP.Name = "_txtIP";
            this._txtIP.Size = new System.Drawing.Size(139, 20);
            this._txtIP.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MV Boli", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(199, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "IPv4 Address :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MV Boli", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Message :";
            // 
            // _txtMessage
            // 
            this._txtMessage.BackColor = System.Drawing.Color.Silver;
            this._txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtMessage.Location = new System.Drawing.Point(87, 35);
            this._txtMessage.Name = "_txtMessage";
            this._txtMessage.Size = new System.Drawing.Size(350, 20);
            this._txtMessage.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(243, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 28);
            this.label4.TabIndex = 10;
            this.label4.Text = "UDP ChatClient";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(173)))));
            this.ClientSize = new System.Drawing.Size(668, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.UI_CloseProgram);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UI_CloseProgram;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lbxDisplay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _txtNickname;
        private System.Windows.Forms.TextBox _txtIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _txtMessage;
        private System.Windows.Forms.Button UI_btnSend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button UI_btnFilterIP;
        private System.Windows.Forms.Button UI_btnFilterName;
        private System.Windows.Forms.ListBox UI_LbxIP;
    }
}

