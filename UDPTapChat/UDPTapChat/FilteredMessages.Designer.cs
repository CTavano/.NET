namespace UDPTapChat
{
    partial class FilteredMessages
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
            this.lbxFiltered = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.UI_CloseForm = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbxFiltered
            // 
            this.lbxFiltered.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxFiltered.BackColor = System.Drawing.Color.DarkGray;
            this.lbxFiltered.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbxFiltered.Font = new System.Drawing.Font("MV Boli", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxFiltered.FormattingEnabled = true;
            this.lbxFiltered.ItemHeight = 17;
            this.lbxFiltered.Location = new System.Drawing.Point(3, 13);
            this.lbxFiltered.Name = "lbxFiltered";
            this.lbxFiltered.Size = new System.Drawing.Size(500, 221);
            this.lbxFiltered.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.lbxFiltered);
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(506, 248);
            this.panel1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(59, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(388, 28);
            this.label4.TabIndex = 11;
            this.label4.Text = "UDP ChatClient - Message History";
            // 
            // UI_CloseForm
            // 
            this.UI_CloseForm.BackColor = System.Drawing.Color.DarkGray;
            this.UI_CloseForm.FlatAppearance.BorderSize = 0;
            this.UI_CloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UI_CloseForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_CloseForm.Location = new System.Drawing.Point(470, 4);
            this.UI_CloseForm.Name = "UI_CloseForm";
            this.UI_CloseForm.Size = new System.Drawing.Size(24, 25);
            this.UI_CloseForm.TabIndex = 12;
            this.UI_CloseForm.Text = "X";
            this.UI_CloseForm.UseVisualStyleBackColor = false;
            // 
            // FilteredMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(173)))));
            this.ClientSize = new System.Drawing.Size(506, 292);
            this.Controls.Add(this.UI_CloseForm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FilteredMessages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FilteredMessages";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxFiltered;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button UI_CloseForm;
    }
}