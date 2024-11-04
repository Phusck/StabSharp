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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNewInputForm
            // 
            this.buttonNewInputForm.Location = new System.Drawing.Point(362, 37);
            this.buttonNewInputForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNewInputForm.Name = "buttonNewInputForm";
            this.buttonNewInputForm.Size = new System.Drawing.Size(136, 55);
            this.buttonNewInputForm.TabIndex = 1;
            this.buttonNewInputForm.Text = "New Input form";
            this.buttonNewInputForm.UseVisualStyleBackColor = true;
            this.buttonNewInputForm.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(362, 96);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1024, 1024);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(11, 10);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(345, 621);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(1209, 29);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(252, 16);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Stable Diffusion prompt: a definitive guide";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabelLoras
            // 
            this.linkLabelLoras.AutoSize = true;
            this.linkLabelLoras.Location = new System.Drawing.Point(1332, 138);
            this.linkLabelLoras.Name = "linkLabelLoras";
            this.linkLabelLoras.Size = new System.Drawing.Size(86, 16);
            this.linkLabelLoras.TabIndex = 5;
            this.linkLabelLoras.TabStop = true;
            this.linkLabelLoras.Text = "Folder: Loras";
            this.linkLabelLoras.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLoras_LinkClicked);
            // 
            // linkLabelDownloads
            // 
            this.linkLabelDownloads.AutoSize = true;
            this.linkLabelDownloads.Location = new System.Drawing.Point(1332, 87);
            this.linkLabelDownloads.Name = "linkLabelDownloads";
            this.linkLabelDownloads.Size = new System.Drawing.Size(120, 16);
            this.linkLabelDownloads.TabIndex = 6;
            this.linkLabelDownloads.TabStop = true;
            this.linkLabelDownloads.Text = "Folder: Downloads";
            this.linkLabelDownloads.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDownloads_LinkClicked);
            // 
            // linkLabelModels
            // 
            this.linkLabelModels.AutoSize = true;
            this.linkLabelModels.Location = new System.Drawing.Point(1332, 114);
            this.linkLabelModels.Name = "linkLabelModels";
            this.linkLabelModels.Size = new System.Drawing.Size(97, 16);
            this.linkLabelModels.TabIndex = 7;
            this.linkLabelModels.TabStop = true;
            this.linkLabelModels.Text = "Folder: Models";
            this.linkLabelModels.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelModels_LinkClicked);
            // 
            // listboxRequests
            // 
            this.listboxRequests.FormattingEnabled = true;
            this.listboxRequests.ItemHeight = 16;
            this.listboxRequests.Location = new System.Drawing.Point(1316, 294);
            this.listboxRequests.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listboxRequests.Name = "listboxRequests";
            this.listboxRequests.Size = new System.Drawing.Size(219, 100);
            this.listboxRequests.TabIndex = 8;
            // 
            // buttonMoveToSave
            // 
            this.buttonMoveToSave.Location = new System.Drawing.Point(1335, 398);
            this.buttonMoveToSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonMoveToSave.Name = "buttonMoveToSave";
            this.buttonMoveToSave.Size = new System.Drawing.Size(136, 55);
            this.buttonMoveToSave.TabIndex = 9;
            this.buttonMoveToSave.Text = "Save";
            this.buttonMoveToSave.UseVisualStyleBackColor = true;
            this.buttonMoveToSave.Click += new System.EventHandler(this.buttonMoveToSave_Click);
            // 
            // buttonNewPonyInputform
            // 
            this.buttonNewPonyInputform.Location = new System.Drawing.Point(504, 37);
            this.buttonNewPonyInputform.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNewPonyInputform.Name = "buttonNewPonyInputform";
            this.buttonNewPonyInputform.Size = new System.Drawing.Size(136, 55);
            this.buttonNewPonyInputform.TabIndex = 10;
            this.buttonNewPonyInputform.Text = "New Pony Input form";
            this.buttonNewPonyInputform.UseVisualStyleBackColor = true;
            this.buttonNewPonyInputform.Click += new System.EventHandler(this.buttonNewPonyInputform_Click);
            // 
            // textBoxCurrentRequest
            // 
            this.textBoxCurrentRequest.Location = new System.Drawing.Point(1316, 229);
            this.textBoxCurrentRequest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxCurrentRequest.Name = "textBoxCurrentRequest";
            this.textBoxCurrentRequest.Size = new System.Drawing.Size(219, 22);
            this.textBoxCurrentRequest.TabIndex = 11;
            // 
            // labelCurrentTask
            // 
            this.labelCurrentTask.AutoSize = true;
            this.labelCurrentTask.Location = new System.Drawing.Point(1312, 210);
            this.labelCurrentTask.Name = "labelCurrentTask";
            this.labelCurrentTask.Size = new System.Drawing.Size(103, 16);
            this.labelCurrentTask.TabIndex = 12;
            this.labelCurrentTask.Text = "Current Request";
            // 
            // labelTaskQueue
            // 
            this.labelTaskQueue.AutoSize = true;
            this.labelTaskQueue.Location = new System.Drawing.Point(1312, 275);
            this.labelTaskQueue.Name = "labelTaskQueue";
            this.labelTaskQueue.Size = new System.Drawing.Size(92, 16);
            this.labelTaskQueue.TabIndex = 13;
            this.labelTaskQueue.Text = "Task Request";
            // 
            // progressBarCurrentRequest
            // 
            this.progressBarCurrentRequest.Location = new System.Drawing.Point(1316, 254);
            this.progressBarCurrentRequest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBarCurrentRequest.Name = "progressBarCurrentRequest";
            this.progressBarCurrentRequest.Size = new System.Drawing.Size(219, 18);
            this.progressBarCurrentRequest.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1413, 485);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 55);
            this.button1.TabIndex = 15;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowNewest
            // 
            this.checkBoxShowNewest.AutoSize = true;
            this.checkBoxShowNewest.Checked = true;
            this.checkBoxShowNewest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowNewest.Location = new System.Drawing.Point(362, 12);
            this.checkBoxShowNewest.Name = "checkBoxShowNewest";
            this.checkBoxShowNewest.Size = new System.Drawing.Size(110, 20);
            this.checkBoxShowNewest.TabIndex = 16;
            this.checkBoxShowNewest.Text = "Show Newest";
            this.checkBoxShowNewest.UseVisualStyleBackColor = true;
            // 
            // buttonDecimateImage
            // 
            this.buttonDecimateImage.Location = new System.Drawing.Point(12, 635);
            this.buttonDecimateImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDecimateImage.Name = "buttonDecimateImage";
            this.buttonDecimateImage.Size = new System.Drawing.Size(136, 55);
            this.buttonDecimateImage.TabIndex = 17;
            this.buttonDecimateImage.Text = "Decimate Window";
            this.buttonDecimateImage.UseVisualStyleBackColor = true;
            this.buttonDecimateImage.Click += new System.EventHandler(this.buttonDecimateImage_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1545, 931);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
    }
}

