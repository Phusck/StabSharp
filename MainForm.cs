using StabSharp.CodeGeneration;
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
        private List<Prompt> promptQueue = new List<Prompt>();
        private Prompt? currentPrompt;
        private bool isUdatingProgress = false;
        private List<Checkpoint> checkpoints = new List<Checkpoint>();
        private bool isLoadingCheckpoints = false;

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
            this.Load += MainForm_Load;
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
                listView1.Items.Add(generatedImages[i].PromptData.Seed.ToString(), i);
            }
        }

        private void UpdateRequestOverview()
        {
            // Update the queue list box with first non-Lora prompt parts
            listboxRequests.DataSource = null;
            listboxRequests.Items.Clear();
            foreach (var prompt in promptQueue)
            {
                string displayText = prompt.GetFirstNonLoraPromptPart();
                if (string.IsNullOrEmpty(displayText))
                {
                    displayText = "(No prompt)";
                }
                listboxRequests.Items.Add(displayText);
            }

            // Update current request text box
            if (currentPrompt != null)
            {
                string displayText = currentPrompt.Value.GetFirstNonLoraPromptPart();
                textBoxCurrentRequest.Text = string.IsNullOrEmpty(displayText) ? "(No prompt)" : displayText;
            }
            else
            {
                textBoxCurrentRequest.Text = "";
            }
        }

        public void AddTextToImageRequestToQueue(Prompt promptData)
        {

            promptQueue.Add(promptData);

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
            List<PromptPart> promptParts = new List<PromptPart>
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
            Prompt? savedPrompt = SaveSystem.LoadLastPrompt();
            if (!savedPrompt.HasValue)
            {
                return;
            }
            InputForm inputForm = new InputForm(this);
            inputForm.Show();
            inputForm.SetupForm(savedPrompt.Value);
        }

        private void buttonClearQueue_Click(object sender, EventArgs e)
        {
            if (promptQueue.Count == 0)
            {
                return;
            }

            promptQueue.Clear();
            UpdateRequestOverview();
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
                sdi.PromptData.EnableHR = "true";
                sdi.PromptData.DenoisingStrength = "0.7";
                sdi.PromptData.HiresUpscaler = "Latent";
                sdi.PromptData.HiresScale = "1.1";


                AddTextToImageRequestToQueue(sdi.PromptData);
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
            if (promptQueue.Count == 0)
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
            currentPrompt = promptQueue[0];
            promptQueue.RemoveAt(0);
            UpdateRequestOverview();


            isUdatingProgress = true;
            updateProgress();
            var generatedImage = await sdapi.ImageRequest(currentPrompt.Value);
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


            UpdateRequestOverview();
            if (promptQueue.Count != 0)
            {
                popFromQueue();
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadCheckpointsAsync();
        }

        private async Task LoadCheckpointsAsync()
        {
            try
            {
                isLoadingCheckpoints = true;
                // Ensure the server rescans the checkpoints folder so /sd-models isn't stale.
                await sdapi.RefreshCheckpointsAsync();
                checkpoints = await sdapi.GetCheckpointsAsync();
                
                if (InvokeRequired)
                {
                    this.Invoke(new Action(() => {
                        comboBoxCheckpoints.Items.Clear();
                        foreach (var checkpoint in checkpoints)
                        {
                            if (!string.IsNullOrEmpty(checkpoint.Title))
                            {
                                comboBoxCheckpoints.Items.Add(checkpoint.Title);
                            }
                            else if (!string.IsNullOrEmpty(checkpoint.ModelName))
                            {
                                comboBoxCheckpoints.Items.Add(checkpoint.ModelName);
                            }
                        }
                        if (comboBoxCheckpoints.Items.Count > 0)
                        {
                            comboBoxCheckpoints.SelectedIndex = 0;
                        }
                    }));
                }
                else
                {
                    comboBoxCheckpoints.Items.Clear();
                    foreach (var checkpoint in checkpoints)
                    {
                        if (!string.IsNullOrEmpty(checkpoint.Title))
                        {
                            comboBoxCheckpoints.Items.Add(checkpoint.Title);
                        }
                        else if (!string.IsNullOrEmpty(checkpoint.ModelName))
                        {
                            comboBoxCheckpoints.Items.Add(checkpoint.ModelName);
                        }
                    }
                    if (comboBoxCheckpoints.Items.Count > 0)
                    {
                        comboBoxCheckpoints.SelectedIndex = 0;
                    }
                }
                
                if (checkpoints.Count == 0)
                {
                    if (InvokeRequired)
                    {
                        this.Invoke(new Action(() => {
                            MessageBox.Show("No checkpoints found. Make sure the Stable Diffusion API server is running.");
                        }));
                    }
                    else
                    {
                        MessageBox.Show("No checkpoints found. Make sure the Stable Diffusion API server is running.");
                    }
                }
            }
            catch (Exception ex)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action(() => {
                        MessageBox.Show($"Error loading checkpoints: {ex.Message}");
                    }));
                }
                else
                {
                    MessageBox.Show($"Error loading checkpoints: {ex.Message}");
                }
            }
            finally
            {
                isLoadingCheckpoints = false;
            }
        }

        private async void comboBoxCheckpoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Don't set checkpoint during initial load
            if (isLoadingCheckpoints)
            {
                return;
            }

            if (comboBoxCheckpoints.SelectedIndex >= 0 && comboBoxCheckpoints.SelectedIndex < checkpoints.Count)
            {
                var selectedCheckpoint = checkpoints[comboBoxCheckpoints.SelectedIndex];
                // Use Title as that's what the API expects for sd_model_checkpoint
                bool success = await sdapi.SetActiveCheckpointAsync(selectedCheckpoint.Title);
                if (!success)
                {
                    MessageBox.Show($"Failed to set checkpoint: {selectedCheckpoint.Title}");
                }
            }
        }


    }
}
