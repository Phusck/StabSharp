using System;
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
        List<Request> requestQueue = new List<Request>();

        //TODO: Move this to StableDiffusionAPI
        private bool stableDiffusionAPIReady = true;
        private bool needRefresh = true;


        public MainForm()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.Columns.Add("Image                                 ",0);
            listView1.Columns.Add("Seed",50);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void populateListView()
        {
            // Only create a new ImageList if it is null or needs to be refreshed completely
            if (listView1.SmallImageList == null || needRefresh)
            {
                ImageList imgs = new ImageList();
                imgs.ImageSize = new Size(100, 100);

                foreach (SDImage image in generatedImages)
                {
                    try
                    {
                        Image img = Image.FromFile(image.ImagePath); // Load the image
                        imgs.Images.Add(img); // Add the image to the ImageList
                                              // Do not dispose here as the ImageList needs to use the image
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (e.g., log them)
                        Console.WriteLine("Error loading image: " + ex.Message);
                    }
                }

                listView1.SmallImageList = imgs; // Assign the newly created ImageList to the ListView
            }

            // Update the ListView items
            listView1.Items.Clear();
            for (int i = generatedImages.Count - 1; i > -1; i--)
            {
                listView1.Items.Add(generatedImages[i].Parameters.Seed.ToString(), i);
            }

            // Restore selection if applicable
            int listIndex = listView1.SelectedIndices.Count > 0 ? listView1.SelectedIndices[0] : -1;
            if (listIndex != -1 && listIndex < listView1.Items.Count)
            {
                listView1.Items[listIndex].Selected = true;
                listView1.EnsureVisible(listIndex);
            }
        }

        private void populateReqestQueue()
        {
            listboxRequests.DataSource = null;
            listboxRequests.DataSource = requestQueue;
        }

        public void AddTextToImageRequestToQueue(string prompt, string negativePrompt, bool doHires = false, int seed = -1)
        {
            prompt = prompt.Replace("\r\n", "");
            prompt = prompt.Trim();

            requestQueue.Add(new Request(RequestType.TextToImage, sdapi.RequestTxtToImg(prompt, negativePrompt, doHires, seed)));

            populateReqestQueue();

            if (stableDiffusionAPIReady)
            {
                popFromQueue();
            }


        }

        private async void popFromQueue()
        {
            if (!stableDiffusionAPIReady)
            {
                MessageBox.Show("Tried to pop from Queue when not ready, this should not happen");
                return;
            }

            if (requestQueue.Count == 0)
            {
                return;
            }

            stableDiffusionAPIReady = false;
            Request request = requestQueue[0];
            requestQueue.RemoveAt(0);
            if (request.RequestType == RequestType.TextToImage)
            {
                var generatedImage = await sdapi.ImageRequest(request.Prompt);
                if (!string.IsNullOrEmpty(generatedImage.ImagePath))
                {
                    generatedImages.Add(generatedImage);

                    // Update the existing PictureBox with the new image.
                    this.Invoke(new Action(() => {
                        using (Image newImage = Image.FromFile(generatedImage.ImagePath))
                        {
                            if (pictureBox1.Image != null)
                            {
                                var oldImage = pictureBox1.Image;
                                pictureBox1.Image = new Bitmap(newImage);
                                oldImage.Dispose(); // Dispose the old image to free memory
                            }
                            else
                            {
                                pictureBox1.Image = new Bitmap(newImage);
                            }
                        }
                    }));

                    populateListView();
                    stableDiffusionAPIReady = true;
                }
                else
                {
                    MessageBox.Show("Failed to generate image.");
                }
            }
            else
            {
                MessageBox.Show("Unknown request type");
            }
            populateReqestQueue();
            if (requestQueue.Count != 0)
            {
                popFromQueue();
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
                AddTextToImageRequestToQueue(sdi.Parameters.prompt, sdi.Parameters.negative_prompt,true, sdi.Parameters.Seed);
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
