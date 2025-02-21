using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;


namespace StabSharp
{
    public partial class MainForm : Form
    {
        private StableDiffusionAPI sdapi = new StableDiffusionAPI();
        private List<SDImage> generatedImages = new List<SDImage>();
        private List<Request> requestQueue = new List<Request>();
        private Request currentRequest;
        private bool isUdatingProgress = false;

        //TODO: Move this to StableDiffusionAPI
        private bool stableDiffusionAPIReady = true;
        private bool needRefresh = true;

        public MainForm()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.Columns.Add("Image                                 ", 0);
            listView1.Columns.Add("Seed", 50);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private ImageList GenerateImageList()
        {
            var imageList = new ImageList { ImageSize = new Size(100, 100) };
            foreach (var image in generatedImages)
            {
                try
                {
                    var img = Image.FromFile(image.ImagePath);
                    imageList.Images.Add(img);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading image: {ex.Message}");
                }
            }
            return imageList;
        }
        private void populateImageListView()
        {
            if (listView1.SmallImageList == null || needRefresh)
            {
                listView1.SmallImageList = GenerateImageList();
            }

            listView1.Items.Clear();
            for (int i = generatedImages.Count - 1; i >= 0; i--)
            {
                listView1.Items.Add(generatedImages[i].Parameters.Seed.ToString(), i);
            }
        }

        private void UpdateRequestOverview()
        {
            listboxRequests.DataSource = null;
            listboxRequests.DataSource = requestQueue;
            if (currentRequest != null)
            {
                textBoxCurrentRequest.Text = currentRequest?.ToString();
            }
            else
            {
                textBoxCurrentRequest.Text = "";
            }
        }

        public void AddTextToImageRequestToQueue(string prompt, string negativePrompt, bool doHires, int seed, int steps, string sampler, bool doClipSkip, int clipSkipNumber)
        {
            // Remove newlines and trailing whitespace from the prompt
            prompt = prompt.Replace("\r\n", "");
            prompt = prompt.Trim();
            requestQueue.Add(new Request(RequestType.TextToImage, sdapi.RequestTxtToImg(prompt, negativePrompt, doHires, seed, steps, sampler, doClipSkip, clipSkipNumber)));

            if (stableDiffusionAPIReady)
            {
                popFromQueue();
            }
            else
            {
                UpdateRequestOverview();
            }
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
        private void button2_Click(object sender, EventArgs e)
        {
            //Create a new InputForm
            InputForm inputForm = new InputForm(this);
            inputForm.Show();
        }
        private void buttonDecimateImage_Click(object sender, EventArgs e)
        {
            //Create a new DecimateForm
            DecimateForm decimateForm = new DecimateForm();
            decimateForm.Show();
        }
        private void buttonNewPonyInputform_Click(object sender, EventArgs e)
        {
            //Create a new InputForm
            InputForm inputForm = new InputForm(this);
            ObservableCollection<PromptPart> promptParts = new ObservableCollection<PromptPart>
            {
                new PromptPart("Score_9", 1f, 0, false),
                new PromptPart("Score_8_up", 1f, 0, false),
                new PromptPart("Score_7_up", 1f, 0, false),
                new PromptPart("StylesForPonyDiffusion", 1f, 0, true)
            };
            inputForm.Show();
            inputForm.SetupForm(promptParts, "score_6, score_5, score_4, pony, black and white, muscular, censored, furry, 3d,simple background");
        }
        private void buttonLoadLastInputForm_Click(object sender, EventArgs e)
        {
            InputSave? inputSave = SaveSystem.LoadLastPrompt();
            if (!inputSave.HasValue)
            {
                return;
            }
            InputForm inputForm = new InputForm(this);
            inputForm.Show();
            inputForm.SetupForm(inputSave.Value.PromptParts, inputSave.Value.NegativePrompt);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                int index = generatedImages.Count - 1 - listView1.SelectedIndices[0];

                pictureBox1.Image = Image.FromFile(generatedImages[index].ImagePath);
            }

        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                SDImage sdi = generatedImages[generatedImages.Count - 1 - listView1.SelectedIndices[0]];
                //TODO: Fix the 
                AddTextToImageRequestToQueue(sdi.Parameters.prompt, sdi.Parameters.negative_prompt, true, sdi.Parameters.Seed, (int)sdi.Parameters.steps, (string)sdi.Parameters.sampler_name, false, sdi.Parameters.clip_skip);
                MessageBox.Show("Fix Clip Skip for HighRes");
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            //checkBoxShowNewest.Checked = true;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://stable-diffusion-art.com/prompt-guide/");
        }
        private void linkLabelLoras_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", "E:\\stable-diffusion-webui\\models\\Lora\\");
        }
        private void linkLabelDownloads_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("shell:Downloads");
        }
        private void linkLabelModels_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", "E:\\stable-diffusion-webui\\models\\Stable-diffusion");
        }


        private void UpdatePictureBoxWithImage(string imagePath)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => UpdatePictureBoxWithImage(imagePath)));
                return;
            }

            using (Image newImage = Image.FromFile(imagePath))
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
        }

        private async void updateProgress()
        {
            if (isUdatingProgress)
            {
                var progress = await sdapi.GetProgress();
                progressBarCurrentRequest.Value = progress;
                await Task.Delay(500);
                updateProgress();
            }
            else
            {
                progressBarCurrentRequest.Value = 0;
            }
        }
        private async void popFromQueue()
        {
            // Check if the API is ready to process a new request (currently just set by this program, not by the API)
            if (!stableDiffusionAPIReady)
            {
                MessageBox.Show("Tried to pop from Queue when not ready, this should not happen");
                return;
            }
            // Check if the queue is empty
            if (requestQueue.Count == 0)
            {
                return;
            }
            // Check if the server is running
            if (!sdapi.IsServerRunning("127.0.0.1", 7860))
            {
                MessageBox.Show("Stable Diffusion API is not running");
                return;
            }

            stableDiffusionAPIReady = false;
            currentRequest = requestQueue[0];
            requestQueue.RemoveAt(0);
            UpdateRequestOverview();

            if (currentRequest.RequestType == RequestType.TextToImage)
            {
                isUdatingProgress = true;
                updateProgress();
                var generatedImage = await sdapi.ImageRequest(currentRequest.Prompt);
                isUdatingProgress = false;
                if (generatedImage == null)
                {
                    stableDiffusionAPIReady = true;
                    return;
                }
                if (!string.IsNullOrEmpty(generatedImage.ImagePath))
                {
                    generatedImages.Add(generatedImage);

                    if (checkBoxShowNewest.Checked)
                    {
                        UpdatePictureBoxWithImage(generatedImage.ImagePath);
                    }

                    populateImageListView();
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

            UpdateRequestOverview();
            if (requestQueue.Count != 0)
            {
                popFromQueue();
            }
        }


    }
}
