namespace MAC_L_W_5_2_Step2
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
            this.UpD_n = new System.Windows.Forms.NumericUpDown();
            this.UpD_i = new System.Windows.Forms.NumericUpDown();
            this.Start = new System.Windows.Forms.Button();
            this.tBx_Rezult = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.UpD_n)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpD_i)).BeginInit();
            this.SuspendLayout();
            // 
            // UpD_n
            // 
            this.UpD_n.Location = new System.Drawing.Point(12, 32);
            this.UpD_n.Name = "UpD_n";
            this.UpD_n.Size = new System.Drawing.Size(120, 20);
            this.UpD_n.TabIndex = 0;
            this.UpD_n.ValueChanged += new System.EventHandler(this.UpD_n_ValueChanged);
            // 
            // UpD_i
            // 
            this.UpD_i.Location = new System.Drawing.Point(168, 32);
            this.UpD_i.Name = "UpD_i";
            this.UpD_i.Size = new System.Drawing.Size(120, 20);
            this.UpD_i.TabIndex = 1;
            this.UpD_i.ValueChanged += new System.EventHandler(this.UpD_i_ValueChanged);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(407, 29);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 2;
            this.Start.Text = "button1";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // tBx_Rezult
            // 
            this.tBx_Rezult.Location = new System.Drawing.Point(12, 58);
            this.tBx_Rezult.Multiline = true;
            this.tBx_Rezult.Name = "tBx_Rezult";
            this.tBx_Rezult.Size = new System.Drawing.Size(470, 231);
            this.tBx_Rezult.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 301);
            this.Controls.Add(this.tBx_Rezult);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.UpD_i);
            this.Controls.Add(this.UpD_n);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.UpD_n)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpD_i)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown UpD_n;
        private System.Windows.Forms.NumericUpDown UpD_i;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.TextBox tBx_Rezult;
    }
}

