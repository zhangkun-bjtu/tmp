namespace SearchPath
{
    partial class showans
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(showans));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.clicktoshowai = new System.Windows.Forms.Button();
            this.clicktoshowstep = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(8, 11);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(437, 334);
            this.textBox1.TabIndex = 0;
            // 
            // clicktoshowai
            // 
            this.clicktoshowai.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clicktoshowai.Location = new System.Drawing.Point(22, 293);
            this.clicktoshowai.Name = "clicktoshowai";
            this.clicktoshowai.Size = new System.Drawing.Size(183, 56);
            this.clicktoshowai.TabIndex = 1;
            this.clicktoshowai.Text = "Auto-display";
            this.clicktoshowai.UseVisualStyleBackColor = true;
            this.clicktoshowai.Click += new System.EventHandler(this.clicktoshowai_Click);
            // 
            // clicktoshowstep
            // 
            this.clicktoshowstep.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clicktoshowstep.Location = new System.Drawing.Point(211, 293);
            this.clicktoshowstep.Name = "clicktoshowstep";
            this.clicktoshowstep.Size = new System.Drawing.Size(189, 56);
            this.clicktoshowstep.TabIndex = 2;
            this.clicktoshowstep.Text = "Display step by step";
            this.clicktoshowstep.UseVisualStyleBackColor = true;
            this.clicktoshowstep.Click += new System.EventHandler(this.clicktoshowstep_Click);
            // 
            // showans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 367);
            this.Controls.Add(this.clicktoshowstep);
            this.Controls.Add(this.clicktoshowai);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "showans";
            this.Text = "The Answer";
            this.Load += new System.EventHandler(this.showans_Load);
            this.Resize += new System.EventHandler(this.showans_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Button clicktoshowai;
        public System.Windows.Forms.Button clicktoshowstep;
    }
}