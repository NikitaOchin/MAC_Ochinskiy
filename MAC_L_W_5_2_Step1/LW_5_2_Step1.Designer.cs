namespace MAC_L_W_5_2_Step1
{
    partial class LW_5_2_Step1
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
            this.button1 = new System.Windows.Forms.Button();
            this.UpD_N = new System.Windows.Forms.NumericUpDown();
            this.tBx_Rezult = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.UpD_N)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(138, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // UpD_N
            // 
            this.UpD_N.Location = new System.Drawing.Point(12, 20);
            this.UpD_N.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.UpD_N.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.UpD_N.Name = "UpD_N";
            this.UpD_N.Size = new System.Drawing.Size(120, 20);
            this.UpD_N.TabIndex = 3;
            this.UpD_N.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.UpD_N.Click += new System.EventHandler(this.UpD_N_ValueChanged);
            // 
            // tBx_Rezult
            // 
            this.tBx_Rezult.Location = new System.Drawing.Point(13, 47);
            this.tBx_Rezult.Multiline = true;
            this.tBx_Rezult.Name = "tBx_Rezult";
            this.tBx_Rezult.Size = new System.Drawing.Size(200, 101);
            this.tBx_Rezult.TabIndex = 4;
            // 
            // LW_5_2_Step1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 160);
            this.Controls.Add(this.tBx_Rezult);
            this.Controls.Add(this.UpD_N);
            this.Controls.Add(this.button1);
            this.Name = "LW_5_2_Step1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.UpD_N)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown UpD_N;
        private System.Windows.Forms.TextBox tBx_Rezult;
    }
}

