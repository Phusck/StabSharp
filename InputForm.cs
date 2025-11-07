using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StabSharp
{
    public partial class InputForm : Form
    {

        private MainForm mainForm;

        private ObservableCollection<PromptPart> promptParts = new ObservableCollection<PromptPart>();
        private ObservableCollection<PromptPartCategory> promptPartCategories = new ObservableCollection<PromptPartCategory>();
        private ObservableCollection<Lora> _loras = new ObservableCollection<Lora>();
        private ObservableCollection<LoraCategory> _loraCategories = new ObservableCollection<LoraCategory>();

        private int _lastCategoryMovedTo = -1;

        private string[] yaks = { "Yakuza", "Manyak", "Yak of all trades", "Yak Nicholson", "Yak Costau", "Yak Black" };
        public string[] samplingMethods = {
            "Euler a",
            "DPM++ SDE",
            "Euler",
            "LMS",
            "Heun",
            "DPM2",
            "DPM2 a",
            "DPM++ 2S a",
            "DPM++ 2M",
            "DPM fast",
            "DPM adaptive",
            "LMS Karras",
            "DPM2 Karras",
            "DPM2 a Karras",
            "DPM++ 2S a Karras",
            "DPM++ 2M Karras",
            "DDIM",
            "PLMS",
            "DPM++ 3M SDE Exponential"
        };

        public ObservableCollection<PromptPart> PromptParts
        {
            get => promptParts;
        }
        public string NegativePrompt
        {
            get => textBoxNegativePrompt.Text;
        }


        public InputForm(MainForm mainform)
        {
            InitializeComponent();
            this.mainForm = mainform;
            promptPartCategories = SaveSystem.LoadCategoriesFromJson();
            _loras = SaveSystem.LoadLorasFromJson();
            _loraCategories = SaveSystem.LoadLoraCategoriesFromJson();
            refreshListboxCategories(false);
            refreshListBoxLoras(false);
            refreshListBoxLoraCategories(false);
            noScrollListBoxPromptParts.MouseWheel += MouseWheelOnPromptParts;
            listBoxCategory.MouseWheel += MouseWheelOnPromptPartsCategory;
            noScrollListBoxPromptParts.DataSource = promptParts;
            textBoxPrompt.AcceptsReturn = false;
            comboBoxSamplingMethod.DataSource = samplingMethods;
            comboBoxSamplingMethod.SelectedIndex = 0;

            this.AcceptButton = buttonGenerate;

            UpdateSelectedPromptPartControls();
        }

        private void MouseWheelOnPromptPartsCategory(object sender, MouseEventArgs e)
        {
            if (listBoxCategory.SelectedIndex == -1)
            {
                return;
            }

            float delta = e.Delta;
            if (Math.Abs(delta) <= 100)
            {
                return;
            }
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                //Move Selected index 
                if (delta < 0)
                {
                    if (listBoxCategory.SelectedIndex == promptPartCategories.Count - 1)
                    {
                        return;
                    }
                    promptPartCategories.Move(listBoxCategory.SelectedIndex, listBoxCategory.SelectedIndex + 1);
                    listBoxCategory.SelectedIndex++;

                }
                else
                {
                    if (listBoxCategory.SelectedIndex == 0)
                    {
                        return;
                    }
                    promptPartCategories.Move(listBoxCategory.SelectedIndex, listBoxCategory.SelectedIndex - 1);
                    listBoxCategory.SelectedIndex--;
                }
            }
            refreshListboxCategories(true);
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            Generate();
            SaveSystem.SaveLastPromptSetup(this);
        }

        private void Generate()
        {
            //Save the prompt parts and loras to a file
            SaveSystem.SafeSaveCategoriesToJson(promptPartCategories);
            SaveSystem.SafeSaveLorasToJson(_loras);
            SaveSystem.SafeSaveLoraCategoriesToJson(_loraCategories);

            //If checkBoxIgnorrePromptParts is not checked, update the prompt text with from the prompt parts
            if (!checkBoxIgnorrePromptParts.Checked)
            {
                if (promptParts.Count == 0)
                {

                    return;
                }
                StringBuilder sb = new StringBuilder();
                foreach (PromptPart part in promptParts)
                {
                    sb.Append(part.ToString());
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);
                textBoxPrompt.Text = sb.ToString();
                checkBoxIgnorrePromptParts.Checked = false;
            }
            //Make the Prompt
            Prompt promptData = new Prompt();

            promptData.PromptParts = promptParts.Select(part => part.ToString()).ToArray();
            promptData.NegativePromptParts = textBoxNegativePrompt.Text.Split(',').Select(part => part.Trim()).ToArray();
            promptData.Steps = trackBarSteps.Value.ToString();
            promptData.Sampler = samplingMethods[comboBoxSamplingMethod.SelectedIndex];
            promptData.CFGScale = trackBarCFG.Value.ToString();
            if (checkBoxClipSkip.Checked)
            {
                promptData.ClipSkip = numericUpDownClipSkip.Value.ToString();
            }
            if (checkHighRes.Checked)
            {
                promptData.EnableHR = "true";
                promptData.DenoisingStrength = "0.7";
                promptData.HiresUpscaler = "Latent";
                promptData.HiresScale = "1.1";
            }
            Random random = new Random();
            promptData.Seed = random.Next().ToString();

            mainForm.AddTextToImageRequestToQueue(promptData);
        }

        //Buttons
        private void buttonNewCategory_Click(object sender, EventArgs e)
        {
            promptPartCategories.Add(new PromptPartCategory("New Category"));
            refreshListboxCategories(false);
            listBoxCategory.SelectedIndex = listBoxCategory.Items.Count - 1;
            textBoxPromptPartCategoryName.Select();
        }
        private void buttonNewPromptPartForCategory_Click(object sender, EventArgs e)
        {
            if (listBoxCategory.SelectedIndex == -1)
            {
                return;
            }
            promptPartCategories[listBoxCategory.SelectedIndex].PromptParts.Add(new PromptPart("New Prompt Part"));
            refreshListBoxPromptsFromCategory(false);
            listBoxPromptsFromCatergory.SelectedIndex = listBoxPromptsFromCatergory.Items.Count - 1;

            refreshTabPromptPart();
            textBoxPromptPartName.Select();



        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveSystem.SaveCategoriesToJson(promptPartCategories);
            SaveSystem.SaveLorasToJson(_loras);
            SaveSystem.SaveLoraCategoriesToJson(_loraCategories);
        }
        private void buttonNewLoraCategory_Click(object sender, EventArgs e)
        {
            _loraCategories.Add(new LoraCategory("New Lora Category"));
            refreshListBoxLoraCategories(false);
            listBoxLoraCategories.SelectedIndex = listBoxLoraCategories.Items.Count - 1;
            refreshTabLoraCategories();
            textBoxLoraCategoryName.Select();
        }
        private void buttonNewLora_Click(object sender, EventArgs e)
        {
            if (listBoxLoraCategories.SelectedIndex == -1)
            {
                return;
            }


            //Open a new Lora form that returns a Lora object
            NewLoraForm newLora = new NewLoraForm();
            newLora.ShowDialog();
            if (newLora.DialogResult == DialogResult.OK)
            {
                Lora lora = newLora.GetLora();
                if (lora != null)
                {
                    if (_loraCategories[listBoxLoraCategories.SelectedIndex].Loras == null)
                    {
                        _loraCategories[listBoxLoraCategories.SelectedIndex].Loras = new List<Lora>();
                    }
                    _loraCategories[listBoxLoraCategories.SelectedIndex].Loras.Add(lora);
                    refreshListBoxLoras(false);
                    listBoxLoras.SelectedIndex = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras.Count - 1;
                    listBoxLoras_MouseClick(null, null);
                }
            }
        }
        private void buttonNewLoraPart_Click(object sender, EventArgs e)
        {
            if (listBoxLoras.SelectedIndex == -1)
            {
                return;
            }
            _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts.Add(new PromptPart("New Lora Part"));
            refreshListBoxPromptsFromLora(true);
            listBoxLoraParts.SelectedIndex = listBoxLoraParts.Items.Count - 1;
            refreshTabLoraPart();
        }
        private void buttonAddListOfPromptPartsFromClipBoard_Click(object sender, EventArgs e)
        {
            if (listBoxLoras.SelectedIndex == -1)
            {
                return;
            }
            foreach (string part in ClipBoardHelper.GetCommaSeperatedStrings())
            {
                _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts.Add(new PromptPart(part));
            }
            refreshListBoxPromptsFromLora(true);
        }
        private void buttonAddCustom_Click(object sender, EventArgs e)
        {
            promptParts.Add(new PromptPart("new Custom Prompt Part"));
            refreshListBoxPromptParts(false);
            noScrollListBoxPromptParts.SelectedIndex = noScrollListBoxPromptParts.Items.Count - 1;
            textBoxSelectedPromptPart.Select();
        }
        private void buttonDeleteCategory_Click(object sender, EventArgs e)
        {
            //Delete the selected category
            if (listBoxCategory.SelectedIndex == -1)
            {
                return;
            }
            promptPartCategories.RemoveAt(listBoxCategory.SelectedIndex);
            refreshListboxCategories(false);
        }
        private void buttonDeleteLora_Click(object sender, EventArgs e)
        {
            if (listBoxLoras.SelectedIndex == -1)
            {
                return;
            }
            _loras.RemoveAt(listBoxLoras.SelectedIndex);
            refreshListBoxLoras(false);
        }
        private void buttonDeleteLoraPart_Click(object sender, EventArgs e)
        {
            if (listBoxLoras.SelectedIndex == -1 || listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }
            _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts.RemoveAt(listBoxLoraParts.SelectedIndex);
            refreshListBoxPromptsFromLora(false);
        }
        private void buttonMoveLoraToOtherCategory_Click(object sender, EventArgs e)
        {
            if (_loraCategories[comboBoxLoraCategoriestoMoveTo.SelectedIndex].Loras == null)
            {
                _loraCategories[comboBoxLoraCategoriestoMoveTo.SelectedIndex].Loras = new List<Lora>();
            }
            _loraCategories[comboBoxLoraCategoriestoMoveTo.SelectedIndex].Loras.Add(_loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex]);
            _loraCategories[listBoxLoraCategories.SelectedIndex].Loras.RemoveAt(listBoxLoras.SelectedIndex);
            _lastCategoryMovedTo = comboBoxLoraCategoriestoMoveTo.SelectedIndex;
            refreshListBoxLoras(true);
            refreshTabLora();
        }

        //Listbox Clicked
        private void listBoxCategory_MouseClick(object sender, MouseEventArgs e)
        {
            refreshListBoxPromptsFromCategory(false);
            refreshTabCategory();
        }
        private void listBoxPromptsFromCatergory_MouseClick(object sender, MouseEventArgs e)
        {
            refreshTabPromptPart();
        }
        private void listBoxLoraCategories_Click(object sender, EventArgs e)
        {

            refreshTabLoraCategories();
            refreshListBoxLoras(false);
            refreshListBoxPromptsFromLora(false);
        }
        private void listBoxLoras_MouseClick(object sender, MouseEventArgs e)
        {

            refreshListBoxPromptsFromLora(false);
            refreshTabLora();
        }
        private void listBoxLoraParts_MouseClick(object sender, MouseEventArgs e)
        {

            refreshTabLoraPart();
        }

        //Refresh listboxes
        private void refreshListBoxPromptParts(bool keepSelectedIndex)
        {
            int selectedIndex = noScrollListBoxPromptParts.SelectedIndex;
            noScrollListBoxPromptParts.DataSource = null;
            noScrollListBoxPromptParts.DataSource = promptParts;
            if (keepSelectedIndex)
            {
                noScrollListBoxPromptParts.SelectedIndex = selectedIndex;
            }
            UpdateSelectedPromptPartControls();
        }
        private void refreshListboxCategories(bool keepIndex)
        {
            int selectedIndex = listBoxCategory.SelectedIndex;
            listBoxCategory.DataSource = null;
            listBoxCategory.DataSource = promptPartCategories;
            if (keepIndex)
            {
                listBoxCategory.SelectedIndex = selectedIndex;
            }
        }
        private void refreshListBoxPromptsFromCategory(bool keepSelectedIndex)
        {
            int selectedIndex = listBoxPromptsFromCatergory.SelectedIndex;
            listBoxPromptsFromCatergory.DataSource = null;
            if (listBoxCategory.SelectedIndex != -1)
            {
                listBoxPromptsFromCatergory.DataSource = promptPartCategories[listBoxCategory.SelectedIndex].PromptParts;
            }
            if (keepSelectedIndex)
            {
                listBoxPromptsFromCatergory.SelectedIndex = selectedIndex;
            }
        }
        private void refreshListBoxLoraCategories(bool keepIndex)
        {
            int selectedIndex = listBoxLoraCategories.SelectedIndex;
            listBoxLoraCategories.DataSource = null;
            listBoxLoraCategories.DataSource = _loraCategories;
            if (keepIndex)
            {
                listBoxLoraCategories.SelectedIndex = selectedIndex;
            }
        }
        private void refreshListBoxLoras(bool keepIndex)
        {
            if (listBoxLoraCategories.SelectedIndex == -1)
            {
                return;
            }
            int selectedIndex = listBoxLoras.SelectedIndex;
            listBoxLoras.DataSource = null;
            listBoxLoras.DataSource = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras;
            if (keepIndex)
            {
                if (_loraCategories[listBoxLoraCategories.SelectedIndex].Loras == null)
                {
                    return;
                }

                //Make sure that tha selected index is not out of range
                if (_loraCategories[listBoxLoraCategories.SelectedIndex].Loras.Count <= selectedIndex)
                {
                    listBoxLoras.SelectedIndex = selectedIndex;
                }
            }
        }
        private void refreshListBoxPromptsFromLora(bool keepIndex)
        {
            //Needs to be don in this order
            int selectedIndex = listBoxLoraParts.SelectedIndex;
            listBoxLoraParts.DataSource = null;
            if (listBoxLoras.SelectedIndex == -1)
            {
                return;
            }


            listBoxLoraParts.DataSource = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts;
            if (keepIndex)
            {
                //Make sure that the list exists
                if (_loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts == null)
                {
                    return;
                }
                //Make sure that tha selected index is not out of range
                if (_loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts.Count <= selectedIndex)
                {
                    return;
                }

                listBoxLoraParts.SelectedIndex = selectedIndex;
            }
        }

        private void refreshTabCategory()
        {
            if (listBoxCategory.SelectedIndex == -1)
            {
                return;
            }
            tabControlDetails.SelectTab(0);
            textBoxPromptPartCategoryName.Text = promptPartCategories[listBoxCategory.SelectedIndex].ToString();
        }
        private void refreshTabPromptPart()
        {
            if (listBoxPromptsFromCatergory.SelectedIndex == -1)
            {
                return;
            }
            tabControlDetails.SelectTab(1);
            textBoxPromptPartName.Text = promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[listBoxPromptsFromCatergory.SelectedIndex].Text;
            trackBarPromptPartWeight.Value = (int)(promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[listBoxPromptsFromCatergory.SelectedIndex].Weight * 100);
            trackBarNumberOfParantheses.Value = promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[listBoxPromptsFromCatergory.SelectedIndex].QuantityOfParantheses;
            refreshTextBoxPromptPartWeight();
        }
        private void refreshTabLoraCategories()
        {
            if (listBoxLoraCategories.SelectedIndex == -1)
            {
                return;
            }
            tabControlDetails.SelectTab(2);
            textBoxLoraCategoryName.Text = _loraCategories[listBoxLoraCategories.SelectedIndex].LoraCategoryName;
        }
        private void refreshTabLora()
        {
            if (listBoxLoras.SelectedIndex == -1)
            {
                return;
            }
            tabControlDetails.SelectTab(3);
            textBoxLoraName.Text = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].LoraName;

            //Get a list of Names of the Categories
            List<string> namesOfCategories = new List<string>();
            foreach (LoraCategory loraCategory in _loraCategories)
            {
                namesOfCategories.Add(loraCategory.LoraCategoryName);
            }

            comboBoxLoraCategoriestoMoveTo.DataSource = namesOfCategories;
            if (_lastCategoryMovedTo != -1)
            {
                if (_loraCategories.Count <= _lastCategoryMovedTo)
                {
                    return;
                }
                comboBoxLoraCategoriestoMoveTo.SelectedIndex = _lastCategoryMovedTo;
            }
        }
        private void refreshTabLoraPart()
        {
            if (listBoxLoras.SelectedIndex == -1 || listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }
            int loraPartIndex = listBoxLoraParts.SelectedIndex;
            tabControlDetails.SelectTab(4);
            textBoxLoraPartText.Text = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].Text;
            listBoxLoraParts.SelectedIndex = loraPartIndex;
            trackBarLoraPartWeight.Value = (int)(_loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].Weight * 100);
            trackBarNumberOfParantheses.Value = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].QuantityOfParantheses;
            checkBoxIsLora.Checked = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].IsLora;


        }

        private void refreshTextBoxPromptPartWeight()
        {
            if (listBoxPromptsFromCatergory.SelectedIndex == -1)
            {
                return;
            }
            textBoxPromptPartWeight.Text = promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[listBoxPromptsFromCatergory.SelectedIndex].Weight.ToString();
        }
        private void refreshTextBoxLoraPartWeight()
        {
            if (listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }
            textBoxLoraPartWeight.Text = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].Weight.ToString();
        }

        private void MouseWheelOnPromptParts(object sender, MouseEventArgs e)
        {
            if (noScrollListBoxPromptParts.SelectedIndex == -1)
            {
                return;
            }

            float delta = e.Delta;
            if (Math.Abs(delta) <= 100)
            {
                return;
            }

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                // Move Selected index
                if (delta < 0)
                {
                    if (noScrollListBoxPromptParts.SelectedIndex == promptParts.Count - 1)
                    {
                        return;
                    }
                    promptParts.Move(noScrollListBoxPromptParts.SelectedIndex, noScrollListBoxPromptParts.SelectedIndex + 1);
                    noScrollListBoxPromptParts.SelectedIndex++;
                }
                else
                {
                    if (noScrollListBoxPromptParts.SelectedIndex == 0)
                    {
                        return;
                    }
                    promptParts.Move(noScrollListBoxPromptParts.SelectedIndex, noScrollListBoxPromptParts.SelectedIndex - 1);
                    noScrollListBoxPromptParts.SelectedIndex--;
                }
            }
            else
            {
                delta = Math.Min(0.05f, delta);
                delta = Math.Max(-0.05f, delta);

                // Retrieve the struct, modify it, and update it back
                var selectedIndex = noScrollListBoxPromptParts.SelectedIndex;
                var selectedPromptPart = promptParts[selectedIndex]; // Retrieve by value
                selectedPromptPart.Weight += delta;                 // Modify property
                promptParts[selectedIndex] = selectedPromptPart;    // Reassign back
            }

            refreshListBoxPromptParts(true);
        }

        private void textBoxPrompt_TextChanged(object sender, EventArgs e)
        {
            checkBoxIgnorrePromptParts.Checked = true;
        }
        private void textBoxSelectedPromptPart_TextChanged(object sender, EventArgs e)
        {
            if (noScrollListBoxPromptParts.SelectedIndex != -1)
            {
                // Get the selected PromptPart from the list
                PromptPart selectedPromptPart = promptParts[noScrollListBoxPromptParts.SelectedIndex];

                // Modify the Text property of the struct
                selectedPromptPart.Text = textBoxSelectedPromptPart.Text;

                // Update the list with the modified struct
                promptParts[noScrollListBoxPromptParts.SelectedIndex] = selectedPromptPart;
            }
        }
        private void textBoxPromptPartName_TextChanged(object sender, EventArgs e)
        {
            // Change the name of the prompt part
            if (listBoxPromptsFromCatergory.SelectedIndex == -1)
            {
                return;
            }

            // Retrieve the struct by value
            var selectedCategory = promptPartCategories[listBoxCategory.SelectedIndex];
            var selectedPromptPart = selectedCategory.PromptParts[listBoxPromptsFromCatergory.SelectedIndex];

            // Modify the struct's property
            selectedPromptPart.Text = textBoxPromptPartName.Text;

            // Update the struct back into the collection
            selectedCategory.PromptParts[listBoxPromptsFromCatergory.SelectedIndex] = selectedPromptPart;
            promptPartCategories[listBoxCategory.SelectedIndex] = selectedCategory;

            refreshListBoxPromptsFromCategory(true);
        }
        private void textBoxPromptPartCategoryName_TextChanged(object sender, EventArgs e)
        {
            if (listBoxCategory.SelectedIndex == -1)
            {
                return;
            }
            promptPartCategories[listBoxCategory.SelectedIndex].Name = textBoxPromptPartCategoryName.Text;
            refreshListboxCategories(true);
        }
        private void textBoxLoraCategoryName_TextChanged(object sender, EventArgs e)
        {
            if (listBoxLoraCategories.SelectedIndex == -1)
            {
                return;
            }
            if (_loraCategories[listBoxLoraCategories.SelectedIndex].LoraCategoryName == textBoxLoraCategoryName.Text)
            {
                return;
            }
            _loraCategories[listBoxLoraCategories.SelectedIndex].LoraCategoryName = textBoxLoraCategoryName.Text;
            refreshListBoxLoraCategories(true);
        }
        private void textBoxLoraName_TextChanged(object sender, EventArgs e)
        {
            if (listBoxLoras.SelectedIndex == -1)
            {
                return;
            }
            if (_loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].LoraName == textBoxLoraName.Text)
            {
                return;
            }

            _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].LoraName = textBoxLoraName.Text;
            refreshListBoxLoras(true);
        }
        private void textBoxLoraPartText_TextChanged(object sender, EventArgs e)
        {
            if (listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }

            // Retrieve the struct, modify it, and update it back
            PromptPart selectedPart = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex];
            selectedPart.Text = textBoxLoraPartText.Text; // Modify property
            _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex] = selectedPart;

            refreshListBoxPromptsFromLora(true);
        }

        private void listBoxPromptsFromCatergory_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (listBoxPromptsFromCatergory.SelectedIndex == -1)
            {
                return;
            }
            //Add a copy of the selected prompt part to the prompt parts list
            promptParts.Add((PromptPart)promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[listBoxPromptsFromCatergory.SelectedIndex].Clone());

            refreshListBoxPromptParts(false);
        }
        private void noScrollListBoxPromptParts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (noScrollListBoxPromptParts.SelectedIndex == -1)
            {
                return;
            }
            promptParts.RemoveAt(noScrollListBoxPromptParts.SelectedIndex);
            refreshListBoxPromptParts(false);

        }
        private void listBoxLoras_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Add a copy of all the parts from the selected Lora to the prompt parts list
            if (listBoxLoras.SelectedIndex == -1)
            {
                return;
            }
            foreach (PromptPart part in _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts)
            {
                promptParts.Add((PromptPart)part.Clone());
            }
            refreshListBoxPromptParts(false);
        }

        private void trackBarPromptPartWeight_Scroll(object sender, EventArgs e)
        {
            // Round to the nearest 5
            trackBarPromptPartWeight.Value = (int)(Math.Round(trackBarPromptPartWeight.Value / 5.0) * 5);

            if (listBoxPromptsFromCatergory.SelectedIndex == -1)
            {
                return;
            }

            // Retrieve the struct, modify it, and update it back
            var selectedCategory = promptPartCategories[listBoxCategory.SelectedIndex];
            var selectedPromptPart = selectedCategory.PromptParts[listBoxPromptsFromCatergory.SelectedIndex]; // Retrieve by value

            selectedPromptPart.Weight = trackBarPromptPartWeight.Value / 100f; // Modify property

            selectedCategory.PromptParts[listBoxPromptsFromCatergory.SelectedIndex] = selectedPromptPart; // Reassign back
            promptPartCategories[listBoxCategory.SelectedIndex] = selectedCategory;

            refreshTextBoxPromptPartWeight();
            refreshListBoxPromptsFromCategory(true);
        }
        private void trackBarNumberOfParentheses_Scroll(object sender, EventArgs e)
        {
            if (listBoxPromptsFromCatergory.SelectedIndex == -1)
            {
                return;
            }

            // Retrieve the struct, modify it, and update it back
            var selectedCategory = promptPartCategories[listBoxCategory.SelectedIndex];
            var selectedPromptPart = selectedCategory.PromptParts[listBoxPromptsFromCatergory.SelectedIndex]; // Retrieve by value

            selectedPromptPart.QuantityOfParantheses = trackBarNumberOfParantheses.Value; // Modify property

            selectedCategory.PromptParts[listBoxPromptsFromCatergory.SelectedIndex] = selectedPromptPart; // Reassign back
            promptPartCategories[listBoxCategory.SelectedIndex] = selectedCategory;

            refreshListBoxPromptsFromCategory(true);
        }
        private void trackBarLoraPartWeight_Scroll(object sender, EventArgs e)
        {
            // Round to the nearest 5
            trackBarLoraPartWeight.Value = (int)(Math.Round(trackBarLoraPartWeight.Value / 5.0) * 5);

            if (listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }

            // Retrieve the struct, modify it, and update it back
            var selectedLora = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex];
            var selectedPart = selectedLora.Parts[listBoxLoraParts.SelectedIndex]; // Retrieve by value

            selectedPart.Weight = trackBarLoraPartWeight.Value / 100f; // Modify property

            selectedLora.Parts[listBoxLoraParts.SelectedIndex] = selectedPart; // Reassign back
            _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex] = selectedLora;

            refreshTextBoxLoraPartWeight();
            refreshListBoxPromptsFromLora(true);
        }
        private void trackBarLoraPartNumberOfParentheses_Scroll(object sender, EventArgs e)
        {
            if (listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }

            // Retrieve the struct, modify it, and update it back
            var selectedLora = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex];
            var selectedPart = selectedLora.Parts[listBoxLoraParts.SelectedIndex]; // Retrieve by value

            selectedPart.QuantityOfParantheses = trackBarNumberOfParantheses.Value; // Modify property

            selectedLora.Parts[listBoxLoraParts.SelectedIndex] = selectedPart; // Reassign back
            _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex] = selectedLora;
        }





        private void checkBoxIsLora_CheckedChanged(object sender, EventArgs e)
        {
            if (listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }

            // Retrieve the struct, modify it, and update it back
            var selectedLora = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex];
            var selectedPart = selectedLora.Parts[listBoxLoraParts.SelectedIndex]; // Retrieve by value

            // Update the IsLora property
            selectedPart.IsLora = checkBoxIsLora.Checked; // Modify property

            // Reassign the modified part back
            selectedLora.Parts[listBoxLoraParts.SelectedIndex] = selectedPart; // Reassign updated part
            _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex] = selectedLora; // Reassign updated Lora

            // Refresh the UI
            refreshListBoxPromptsFromLora(true);
        }




        public void SetupForm(ObservableCollection<PromptPart> prompts, string negativeprompts)
        {
            promptParts = prompts;
            refreshListBoxPromptParts(false);
            textBoxNegativePrompt.Text = negativeprompts;
        }

        private void trackBarSteps_Scroll(object sender, EventArgs e)
        {
            textBoxSteps.Text = trackBarSteps.Value.ToString();
        }
        private void trackBarCFG_Scroll(object sender, EventArgs e)
        {
            textBoxCFG.Text = trackBarCFG.Value.ToString();

        }

        private void buttonAddFromClipBoard_Click(object sender, EventArgs e)
        {
            foreach (string part in ClipBoardHelper.GetCommaSeperatedStrings())
            {
                promptParts.Add(new PromptPart(part));
            }
            refreshListBoxPromptParts(false);
        }

        private void numericUpDownClipSkip_ValueChanged(object sender, EventArgs e)
        {
            checkBoxClipSkip.Checked = true;
        }



        private void noScrollListBoxPromptParts_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedPromptPartControls();
        }

        private void textBoxSelectedPromptPart_Leave(object sender, EventArgs e)
        {
            refreshListBoxPromptParts(true);
        }

        private void InputForm_Load(object sender, EventArgs e)
        {

        }

        private void UpdateSelectedPromptPartControls()
        {
            int selectedIndex = noScrollListBoxPromptParts.SelectedIndex;
            bool hasSelection = selectedIndex != -1;

            textBoxSelectedPromptPart.Enabled = hasSelection;

            if (!hasSelection)
            {
                textBoxSelectedPromptPart.Text = string.Empty;
                return;
            }

            textBoxSelectedPromptPart.Text = promptParts[selectedIndex].Text;
        }
    }
}
