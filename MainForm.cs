﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;


namespace StabSharp
{
    public partial class MainForm : Form
    {
        StableDiffusionAPI sdapi = new StableDiffusionAPI();
        List<SDImage> generatedImages = new List<SDImage>();

        public MainForm()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.Columns.Add("Image                                 ",0);
            listView1.Columns.Add("Seed",50);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void populate()
        {
            ImageList imgs = new ImageList();
            imgs.ImageSize = new Size(100,100);
            foreach (SDImage image in generatedImages)
            {
                imgs.Images.Add(Image.FromFile(image.ImagePath));
            }
            listView1.SmallImageList = imgs;
            listView1.Items.Clear();
            for (int i = generatedImages.Count - 1; i > -1; i--)
            {
                listView1.Items.Add(generatedImages[i].Parameters.Seed.ToString(), i);
            }

        }

        public async void AddRequestToQueue(string prompt,string negativePrompt, bool doHires = false, int seed = -1)
        {
            prompt = prompt.Replace("\r\n", "");
            prompt = prompt.Trim();

            var generatedImage = await sdapi.RequestTxtToImg(prompt,negativePrompt, doHires, seed);
            if (!string.IsNullOrEmpty(generatedImage.ImagePath))
            {
                generatedImages.Add(generatedImage);
                var newImage = Image.FromFile(generatedImage.ImagePath);
 

                PictureBox newPictureBox = new PictureBox
                {
                    Image = newImage,
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Visible = true,
                    BorderStyle = BorderStyle.Fixed3D // Optional, adds a border around the PictureBox.
                };

                // Set the location of the new PictureBox. This is an example; you'll need to customize it.
                // For instance, you might want to arrange multiple PictureBoxes in rows/columns.
                newPictureBox.Location = new Point(10, 10); // You might want to dynamically calculate this.

                // Add the new PictureBox to the form.
                //this.Controls.Add(newPictureBox);
                populate();

            }
            else
            {
                MessageBox.Show("Failed to generate image.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Create a new InputForm
            InputForm inputForm = new InputForm(this);
            inputForm.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1) 
            {
                int index = generatedImages.Count-1-listView1.SelectedIndices[0];

                pictureBox1.Image = Image.FromFile(generatedImages[index].ImagePath);
            }

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                SDImage sdi = generatedImages[generatedImages.Count - 1 - listView1.SelectedIndices[0]];
                AddRequestToQueue(sdi.Parameters.prompt, sdi.Parameters.negative_prompt,true, sdi.Parameters.Seed);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://stable-diffusion-art.com/prompt-guide/");
        }

        private void linkLabelLoras_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", "D:\\Back up - Desktop\\stable-diffusion-webui\\models\\Lora\\");
        }

        private void linkLabelDownloads_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("shell:Downloads");
        }

        private void linkLabelModels_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", "D:\\Back up - Desktop\\stable-diffusion-webui\\models\\Stable-diffusion");
        }

        private void buttonMoveToSave_Click(object sender, EventArgs e)
        {
            // Copy the selected image to the save folder
            if (listView1.SelectedItems.Count == 1)
            {
                int index = generatedImages.Count - 1 - listView1.SelectedIndices[0];
                string sourceFile = generatedImages[index].ImagePath;
                SaveSystem.SaveCopyOfFileToSaveFolder(sourceFile);
            }
        }
    }
}