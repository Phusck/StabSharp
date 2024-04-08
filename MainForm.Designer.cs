namespace StabSharp
{
    partial class MainForm
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
            this.buttonNewInputForm = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabelLoras = new System.Windows.Forms.LinkLabel();
            this.linkLabelDownloads = new System.Windows.Forms.LinkLabel();
            this.linkLabelModels = new System.Windows.Forms.LinkLabel();
            this.listBoxTasks = new System.Windows.Forms.ListBox();
            this.buttonMoveToSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNewInputForm
            // 
            this.buttonNewInputForm.Location = new System.Drawing.Point(406, 12);
            this.buttonNewInputForm.Name = "buttonNewInputForm";
            this.buttonNewInputForm.Size = new System.Drawing.Size(153, 69);
            this.buttonNewInputForm.TabIndex = 1;
            this.buttonNewInputForm.Text = "New Input form";
            this.buttonNewInputForm.UseVisualStyleBackColor = true;
            this.buttonNewInputForm.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(406, 87);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1024, 1024);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(388, 775);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(1360, 36);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(302, 20);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Stable Diffusion prompt: a definitive guide";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabelLoras
            // 
            this.linkLabelLoras.AutoSize = true;
            this.linkLabelLoras.Location = new System.Drawing.Point(1498, 173);
            this.linkLabelLoras.Name = "linkLabelLoras";
            this.linkLabelLoras.Size = new System.Drawing.Size(102, 20);
            this.linkLabelLoras.TabIndex = 5;
            this.linkLabelLoras.TabStop = true;
            this.linkLabelLoras.Text = "Folder: Loras";
            this.linkLabelLoras.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLoras_LinkClicked);
            // 
            // linkLabelDownloads
            // 
            this.linkLabelDownloads.AutoSize = true;
            this.linkLabelDownloads.Location = new System.Drawing.Point(1498, 109);
            this.linkLabelDownloads.Name = "linkLabelDownloads";
            this.linkLabelDownloads.Size = new System.Drawing.Size(141, 20);
            this.linkLabelDownloads.TabIndex = 6;
            this.linkLabelDownloads.TabStop = true;
            this.linkLabelDownloads.Text = "Folder: Downloads";
            this.linkLabelDownloads.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDownloads_LinkClicked);
            // 
            // linkLabelModels
            // 
            this.linkLabelModels.AutoSize = true;
            this.linkLabelModels.Location = new System.Drawing.Point(1498, 142);
            this.linkLabelModels.Name = "linkLabelModels";
            this.linkLabelModels.Size = new System.Drawing.Size(113, 20);
            this.linkLabelModels.TabIndex = 7;
            this.linkLabelModels.TabStop = true;
            this.linkLabelModels.Text = "Folder: Models";
            this.linkLabelModels.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelModels_LinkClicked);
            // 
            // listBoxTasks
            // 
            this.listBoxTasks.FormattingEnabled = true;
            this.listBoxTasks.ItemHeight = 20;
            this.listBoxTasks.Location = new System.Drawing.Point(1480, 233);
            this.listBoxTasks.Name = "listBoxTasks";
            this.listBoxTasks.Size = new System.Drawing.Size(246, 124);
            this.listBoxTasks.TabIndex = 8;
            // 
            // buttonMoveToSave
            // 
            this.buttonMoveToSave.Location = new System.Drawing.Point(1502, 398);
            this.buttonMoveToSave.Name = "buttonMoveToSave";
            this.buttonMoveToSave.Size = new System.Drawing.Size(153, 69);
            this.buttonMoveToSave.TabIndex = 9;
            this.buttonMoveToSave.Text = "Save";
            this.buttonMoveToSave.UseVisualStyleBackColor = true;
            this.buttonMoveToSave.Click += new System.EventHandler(this.buttonMoveToSave_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1738, 1164);
            this.Controls.Add(this.buttonMoveToSave);
            this.Controls.Add(this.listBoxTasks);
            this.Controls.Add(this.linkLabelModels);
            this.Controls.Add(this.linkLabelDownloads);
            this.Controls.Add(this.linkLabelLoras);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonNewInputForm);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonNewInputForm;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.LinkLabel linkLabelLoras;
        private System.Windows.Forms.LinkLabel linkLabelDownloads;
        private System.Windows.Forms.LinkLabel linkLabelModels;
        private System.Windows.Forms.ListBox listBoxTasks;
        private System.Windows.Forms.Button buttonMoveToSave;
    }
}

