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
            this.listboxRequests = new System.Windows.Forms.ListBox();
            this.buttonMoveToSave = new System.Windows.Forms.Button();
            this.buttonNewPonyInputform = new System.Windows.Forms.Button();
            this.textBoxCurrentRequest = new System.Windows.Forms.TextBox();
            this.labelCurrentTask = new System.Windows.Forms.Label();
            this.labelTaskQueue = new System.Windows.Forms.Label();
            this.progressBarCurrentRequest = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxShowNewest = new System.Windows.Forms.CheckBox();
            this.buttonDecimateImage = new System.Windows.Forms.Button();
            this.buttonLoadLastInputForm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNewInputForm
            // 
            this.buttonNewInputForm.Location = new System.Drawing.Point(272, 30);
            this.buttonNewInputForm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonNewInputForm.Name = "buttonNewInputForm";
            this.buttonNewInputForm.Size = new System.Drawing.Size(102, 45);
            this.buttonNewInputForm.TabIndex = 1;
            this.buttonNewInputForm.Text = "New Input form";
            this.buttonNewInputForm.UseVisualStyleBackColor = true;
            this.buttonNewInputForm.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(272, 78);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1024, 1024);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(8, 8);
            this.listView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(260, 505);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(907, 24);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(202, 13);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Stable Diffusion prompt: a definitive guide";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabelLoras
            // 
            this.linkLabelLoras.AutoSize = true;
            this.linkLabelLoras.Location = new System.Drawing.Point(999, 112);
            this.linkLabelLoras.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabelLoras.Name = "linkLabelLoras";
            this.linkLabelLoras.Size = new System.Drawing.Size(68, 13);
            this.linkLabelLoras.TabIndex = 5;
            this.linkLabelLoras.TabStop = true;
            this.linkLabelLoras.Text = "Folder: Loras";
            this.linkLabelLoras.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLoras_LinkClicked);
            // 
            // linkLabelDownloads
            // 
            this.linkLabelDownloads.AutoSize = true;
            this.linkLabelDownloads.Location = new System.Drawing.Point(999, 71);
            this.linkLabelDownloads.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabelDownloads.Name = "linkLabelDownloads";
            this.linkLabelDownloads.Size = new System.Drawing.Size(95, 13);
            this.linkLabelDownloads.TabIndex = 6;
            this.linkLabelDownloads.TabStop = true;
            this.linkLabelDownloads.Text = "Folder: Downloads";
            this.linkLabelDownloads.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDownloads_LinkClicked);
            // 
            // linkLabelModels
            // 
            this.linkLabelModels.AutoSize = true;
            this.linkLabelModels.Location = new System.Drawing.Point(999, 93);
            this.linkLabelModels.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabelModels.Name = "linkLabelModels";
            this.linkLabelModels.Size = new System.Drawing.Size(76, 13);
            this.linkLabelModels.TabIndex = 7;
            this.linkLabelModels.TabStop = true;
            this.linkLabelModels.Text = "Folder: Models";
            this.linkLabelModels.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelModels_LinkClicked);
            // 
            // listboxRequests
            // 
            this.listboxRequests.FormattingEnabled = true;
            this.listboxRequests.Location = new System.Drawing.Point(987, 239);
            this.listboxRequests.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listboxRequests.Name = "listboxRequests";
            this.listboxRequests.Size = new System.Drawing.Size(165, 82);
            this.listboxRequests.TabIndex = 8;
            // 
            // buttonMoveToSave
            // 
            this.buttonMoveToSave.Location = new System.Drawing.Point(1001, 323);
            this.buttonMoveToSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonMoveToSave.Name = "buttonMoveToSave";
            this.buttonMoveToSave.Size = new System.Drawing.Size(102, 45);
            this.buttonMoveToSave.TabIndex = 9;
            this.buttonMoveToSave.Text = "Save";
            this.buttonMoveToSave.UseVisualStyleBackColor = true;
            this.buttonMoveToSave.Click += new System.EventHandler(this.buttonMoveToSave_Click);
            // 
            // buttonNewPonyInputform
            // 
            this.buttonNewPonyInputform.Location = new System.Drawing.Point(378, 30);
            this.buttonNewPonyInputform.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonNewPonyInputform.Name = "buttonNewPonyInputform";
            this.buttonNewPonyInputform.Size = new System.Drawing.Size(102, 45);
            this.buttonNewPonyInputform.TabIndex = 10;
            this.buttonNewPonyInputform.Text = "New Pony Input form";
            this.buttonNewPonyInputform.UseVisualStyleBackColor = true;
            this.buttonNewPonyInputform.Click += new System.EventHandler(this.buttonNewPonyInputform_Click);
            // 
            // textBoxCurrentRequest
            // 
            this.textBoxCurrentRequest.Location = new System.Drawing.Point(987, 186);
            this.textBoxCurrentRequest.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxCurrentRequest.Name = "textBoxCurrentRequest";
            this.textBoxCurrentRequest.Size = new System.Drawing.Size(165, 20);
            this.textBoxCurrentRequest.TabIndex = 11;
            // 
            // labelCurrentTask
            // 
            this.labelCurrentTask.AutoSize = true;
            this.labelCurrentTask.Location = new System.Drawing.Point(984, 171);
            this.labelCurrentTask.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCurrentTask.Name = "labelCurrentTask";
            this.labelCurrentTask.Size = new System.Drawing.Size(84, 13);
            this.labelCurrentTask.TabIndex = 12;
            this.labelCurrentTask.Text = "Current Request";
            // 
            // labelTaskQueue
            // 
            this.labelTaskQueue.AutoSize = true;
            this.labelTaskQueue.Location = new System.Drawing.Point(984, 223);
            this.labelTaskQueue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTaskQueue.Name = "labelTaskQueue";
            this.labelTaskQueue.Size = new System.Drawing.Size(74, 13);
            this.labelTaskQueue.TabIndex = 13;
            this.labelTaskQueue.Text = "Task Request";
            // 
            // progressBarCurrentRequest
            // 
            this.progressBarCurrentRequest.Location = new System.Drawing.Point(987, 206);
            this.progressBarCurrentRequest.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressBarCurrentRequest.Name = "progressBarCurrentRequest";
            this.progressBarCurrentRequest.Size = new System.Drawing.Size(164, 15);
            this.progressBarCurrentRequest.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1060, 394);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 45);
            this.button1.TabIndex = 15;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowNewest
            // 
            this.checkBoxShowNewest.AutoSize = true;
            this.checkBoxShowNewest.Checked = true;
            this.checkBoxShowNewest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowNewest.Location = new System.Drawing.Point(272, 10);
            this.checkBoxShowNewest.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxShowNewest.Name = "checkBoxShowNewest";
            this.checkBoxShowNewest.Size = new System.Drawing.Size(92, 17);
            this.checkBoxShowNewest.TabIndex = 16;
            this.checkBoxShowNewest.Text = "Show Newest";
            this.checkBoxShowNewest.UseVisualStyleBackColor = true;
            // 
            // buttonDecimateImage
            // 
            this.buttonDecimateImage.Location = new System.Drawing.Point(9, 516);
            this.buttonDecimateImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonDecimateImage.Name = "buttonDecimateImage";
            this.buttonDecimateImage.Size = new System.Drawing.Size(102, 45);
            this.buttonDecimateImage.TabIndex = 17;
            this.buttonDecimateImage.Text = "Decimate Window";
            this.buttonDecimateImage.UseVisualStyleBackColor = true;
            this.buttonDecimateImage.Click += new System.EventHandler(this.buttonDecimateImage_Click);
            // 
            // buttonLoadLastInputForm
            // 
            this.buttonLoadLastInputForm.Location = new System.Drawing.Point(484, 29);
            this.buttonLoadLastInputForm.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLoadLastInputForm.Name = "buttonLoadLastInputForm";
            this.buttonLoadLastInputForm.Size = new System.Drawing.Size(102, 45);
            this.buttonLoadLastInputForm.TabIndex = 18;
            this.buttonLoadLastInputForm.Text = "Load last Input form";
            this.buttonLoadLastInputForm.UseVisualStyleBackColor = true;
            this.buttonLoadLastInputForm.Click += new System.EventHandler(this.buttonLoadLastInputForm_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 756);
            this.Controls.Add(this.buttonLoadLastInputForm);
            this.Controls.Add(this.buttonDecimateImage);
            this.Controls.Add(this.checkBoxShowNewest);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBarCurrentRequest);
            this.Controls.Add(this.labelTaskQueue);
            this.Controls.Add(this.labelCurrentTask);
            this.Controls.Add(this.textBoxCurrentRequest);
            this.Controls.Add(this.buttonNewPonyInputform);
            this.Controls.Add(this.buttonMoveToSave);
            this.Controls.Add(this.listboxRequests);
            this.Controls.Add(this.linkLabelModels);
            this.Controls.Add(this.linkLabelDownloads);
            this.Controls.Add(this.linkLabelLoras);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonNewInputForm);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.ListBox listboxRequests;
        private System.Windows.Forms.Button buttonMoveToSave;
        private System.Windows.Forms.Button buttonNewPonyInputform;
        private System.Windows.Forms.TextBox textBoxCurrentRequest;
        private System.Windows.Forms.Label labelCurrentTask;
        private System.Windows.Forms.Label labelTaskQueue;
        private System.Windows.Forms.ProgressBar progressBarCurrentRequest;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxShowNewest;
        private System.Windows.Forms.Button buttonDecimateImage;
        private System.Windows.Forms.Button buttonLoadLastInputForm;
    }
}

