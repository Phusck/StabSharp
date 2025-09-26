namespace StabSharp
{
    partial class DecimateForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxRaw = new System.Windows.Forms.TextBox();
            this.textBoxFormatted = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.Location = new System.Drawing.Point(9, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(462, 362);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBox1_DragDrop);
            this.pictureBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox1_DragEnter);
            // 
            // textBoxRaw
            // 
            this.textBoxRaw.Location = new System.Drawing.Point(486, 11);
            this.textBoxRaw.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxRaw.Multiline = true;
            this.textBoxRaw.Name = "textBoxRaw";
            this.textBoxRaw.Size = new System.Drawing.Size(1093, 284);
            this.textBoxRaw.TabIndex = 1;
            // 
            // textBoxFormatted
            // 
            this.textBoxFormatted.Location = new System.Drawing.Point(486, 309);
            this.textBoxFormatted.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxFormatted.Multiline = true;
            this.textBoxFormatted.Name = "textBoxFormatted";
            this.textBoxFormatted.Size = new System.Drawing.Size(1093, 284);
            this.textBoxFormatted.TabIndex = 2;
            // 
            // DecimateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1827, 862);
            this.Controls.Add(this.textBoxFormatted);
            this.Controls.Add(this.textBoxRaw);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DecimateForm";
            this.Text = "DecimateWindow";
            this.Load += new System.EventHandler(this.DecimateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxRaw;
        private System.Windows.Forms.TextBox textBoxFormatted;
    }
}