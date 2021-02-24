
namespace Molar_Mass_Calculator
{
    partial class Main
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
            this.UI_tBox_Molar_Mass_Output = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UI_tBox_Formula = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UI_btn_sort_by_Atomic = new System.Windows.Forms.Button();
            this.UI_btn_char_Symbols = new System.Windows.Forms.Button();
            this.UI_btn_sort_by_Name = new System.Windows.Forms.Button();
            this.UI_gv_Periodic_Table = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.UI_gv_Periodic_Table)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_tBox_Molar_Mass_Output
            // 
            this.UI_tBox_Molar_Mass_Output.BackColor = System.Drawing.SystemColors.Control;
            this.UI_tBox_Molar_Mass_Output.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.UI_tBox_Molar_Mass_Output.Location = new System.Drawing.Point(526, 413);
            this.UI_tBox_Molar_Mass_Output.Name = "UI_tBox_Molar_Mass_Output";
            this.UI_tBox_Molar_Mass_Output.Size = new System.Drawing.Size(154, 20);
            this.UI_tBox_Molar_Mass_Output.TabIndex = 15;
            this.UI_tBox_Molar_Mass_Output.Text = "0";
            this.UI_tBox_Molar_Mass_Output.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(420, 416);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Approx. Molar Mass";
            // 
            // UI_tBox_Formula
            // 
            this.UI_tBox_Formula.Location = new System.Drawing.Point(134, 413);
            this.UI_tBox_Formula.Name = "UI_tBox_Formula";
            this.UI_tBox_Formula.Size = new System.Drawing.Size(269, 20);
            this.UI_tBox_Formula.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 416);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Chemical Formula:";
            // 
            // UI_btn_sort_by_Atomic
            // 
            this.UI_btn_sort_by_Atomic.Location = new System.Drawing.Point(500, 83);
            this.UI_btn_sort_by_Atomic.Name = "UI_btn_sort_by_Atomic";
            this.UI_btn_sort_by_Atomic.Size = new System.Drawing.Size(180, 23);
            this.UI_btn_sort_by_Atomic.TabIndex = 11;
            this.UI_btn_sort_by_Atomic.Text = "Sort By Atomic #";
            this.UI_btn_sort_by_Atomic.UseVisualStyleBackColor = true;
            // 
            // UI_btn_char_Symbols
            // 
            this.UI_btn_char_Symbols.Location = new System.Drawing.Point(500, 54);
            this.UI_btn_char_Symbols.Name = "UI_btn_char_Symbols";
            this.UI_btn_char_Symbols.Size = new System.Drawing.Size(180, 23);
            this.UI_btn_char_Symbols.TabIndex = 10;
            this.UI_btn_char_Symbols.Text = "Single Character Symbols";
            this.UI_btn_char_Symbols.UseVisualStyleBackColor = true;
            // 
            // UI_btn_sort_by_Name
            // 
            this.UI_btn_sort_by_Name.Location = new System.Drawing.Point(500, 25);
            this.UI_btn_sort_by_Name.Name = "UI_btn_sort_by_Name";
            this.UI_btn_sort_by_Name.Size = new System.Drawing.Size(180, 23);
            this.UI_btn_sort_by_Name.TabIndex = 9;
            this.UI_btn_sort_by_Name.Text = "Sort By Name";
            this.UI_btn_sort_by_Name.UseVisualStyleBackColor = true;
            // 
            // UI_gv_Periodic_Table
            // 
            this.UI_gv_Periodic_Table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UI_gv_Periodic_Table.Location = new System.Drawing.Point(24, 25);
            this.UI_gv_Periodic_Table.Name = "UI_gv_Periodic_Table";
            this.UI_gv_Periodic_Table.Size = new System.Drawing.Size(460, 367);
            this.UI_gv_Periodic_Table.TabIndex = 8;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 450);
            this.Controls.Add(this.UI_tBox_Molar_Mass_Output);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UI_tBox_Formula);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UI_btn_sort_by_Atomic);
            this.Controls.Add(this.UI_btn_char_Symbols);
            this.Controls.Add(this.UI_btn_sort_by_Name);
            this.Controls.Add(this.UI_gv_Periodic_Table);
            this.Name = "Main";
            this.Text = "Molar Mass Calculator";
            ((System.ComponentModel.ISupportInitialize)(this.UI_gv_Periodic_Table)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UI_tBox_Molar_Mass_Output;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UI_tBox_Formula;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button UI_btn_sort_by_Atomic;
        private System.Windows.Forms.Button UI_btn_char_Symbols;
        private System.Windows.Forms.Button UI_btn_sort_by_Name;
        private System.Windows.Forms.DataGridView UI_gv_Periodic_Table;
    }
}

