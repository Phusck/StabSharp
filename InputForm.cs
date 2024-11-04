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
        private ObservableCollection<Lora> loras = new ObservableCollection<Lora>();

        private string[] yaks = { "Yakuza", "Manyak", "Yak of all trades", "Yak Nicholson", "Yak Costau", "Yak Black" };
        public string[] samplingMethods = {
            "Euler a",
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




        public InputForm(MainForm mainform)
        {
            InitializeComponent();
            this.mainForm = mainform;
            promptPartCategories = SaveSystem.LoadCategoriesFromJson();
            loras = SaveSystem.LoadLorasFromJson();
            refreshListboxCategories(false);
            refreshListBoxLoras(false);
            noScrollListBoxPromptParts.MouseWheel += MouseWheelOnPromptParts;
            listBoxCategory.MouseWheel += MouseWheelOnPromptPartsCategory;
            noScrollListBoxPromptParts.DataSource = promptParts;
            textBoxPrompt.AcceptsReturn = false;
            comboBoxSamplingMethod.DataSource = samplingMethods;
            comboBoxSamplingMethod.SelectedIndex = 0;

            this.AcceptButton = buttonGenerate;
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
        }

        private void Generate()
        {
            //Save the prompt parts and loras to a file
            SaveSystem.SafeSaveCategoriesToJson(promptPartCategories);
            SaveSystem.SafeSaveLorasToJson(loras);

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
            //Add the prompt to the request queue
            mainForm.AddTextToImageRequestToQueue(textBoxPrompt.Text, textBoxNegativePrompt.Text, false, -1, trackBarSteps.Value, samplingMethods[comboBoxSamplingMethod.SelectedIndex],checkBoxClipSkip.Checked,(int)numericUpDownClipSkip.Value);
        }

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
            tabControlDetails.SelectTab(1);
            refreshTabPromptPart();
            textBoxPromptPartName.Select();



        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveSystem.SaveCategoriesToJson(promptPartCategories);
            SaveSystem.SaveLorasToJson(loras);
        }
        private void buttonNewLora_Click(object sender, EventArgs e)
        {
            //Open a new Lora form that returns a Lora object
            NewLoraForm newLora = new NewLoraForm();
            newLora.ShowDialog();
            if (newLora.DialogResult == DialogResult.OK)
            {
                Lora lora = newLora.GetLora();
                if (lora != null)
                {
                    loras.Add(lora);
                    refreshListBoxLoras(false);
                }
            }
        }
        private void buttonNewLoraPart_Click(object sender, EventArgs e)
        {
            if (listBoxLoras.SelectedIndex == -1)
            {
                return;
            }
            loras[listBoxLoras.SelectedIndex].Parts.Add(new PromptPart("New Lora Part"));
            refreshListBoxPromptsFromLora(true);
            listBoxLoraParts.SelectedIndex = listBoxLoraParts.Items.Count - 1;

        }

        //Listbox Clicked
        private void listBoxCategory_MouseClick(object sender, MouseEventArgs e)
        {

            refreshListBoxPromptsFromCategory(false);
            tabControlDetails.SelectTab(0);
            refreshTabCategory();

        }
        private void listBoxPromptsFromCatergory_MouseClick(object sender, MouseEventArgs e)
        {
            tabControlDetails.SelectTab(1);
            refreshTabPromptPart();

        }
        private void listBoxLoras_MouseClick(object sender, MouseEventArgs e)
        {
            tabControlDetails.SelectTab(2);
            refreshListBoxPromptsFromLora(false);
            refreshTabLora();
        }
        private void listBoxLoraParts_MouseClick(object sender, MouseEventArgs e)
        {
            tabControlDetails.SelectTab(3);
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

        }
        private void refreshListBoxLoras(bool keepIndex)
        {
            int selectedIndex = listBoxLoras.SelectedIndex;
            listBoxLoras.DataSource = null;
            listBoxLoras.DataSource = loras;
            if (keepIndex)
            {
                listBoxLoras.SelectedIndex = selectedIndex;
            }
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
        private void refreshListBoxPromptsFromLora(bool keepIndex)
        {
            int selectedIndex = listBoxLoraParts.SelectedIndex;
            listBoxLoraParts.DataSource = null;
            if (listBoxLoras.SelectedIndex == -1)
            {
                return;
            }
            listBoxLoraParts.DataSource = loras[listBoxLoras.SelectedIndex].Parts;
            if (keepIndex)
            {
                listBoxLoraParts.SelectedIndex = selectedIndex;
            }
        }

        private void refreshTabPromptPart()
        {
            if (listBoxPromptsFromCatergory.SelectedIndex == -1)
            {
                return;
            }
            textBoxPromptPartName.Text = promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[listBoxPromptsFromCatergory.SelectedIndex].Text;
            trackBarPromptPartWeight.Value = (int)(promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[listBoxPromptsFromCatergory.SelectedIndex].Weight * 100);
            trackBarNumberOfParantheses.Value = promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[listBoxPromptsFromCatergory.SelectedIndex].QuantityOfParantheses;
            refreshTextBoxPromptPartWeight();
        }
        private void refreshTabCategory()
        {
            if (listBoxCategory.SelectedIndex == -1)
            {
                return;
            }

            textBoxPromptPartCategoryName.Text = promptPartCategories[listBoxCategory.SelectedIndex].ToString();
            //string isnow = textBoxPromptPartCategoryName.Text;
            //string shouldbe = promptPartCategories[listBoxPromptPartsCategory.SelectedIndex].ToString();

            //if (textBoxPromptPartCategoryName.Text != promptPartCategories[listBoxPromptPartsCategory.SelectedIndex].ToString())
            //{
            //    textBoxPromptPartCategoryName.Text = promptPartCategories[listBoxPromptPartsCategory.SelectedIndex].ToString();
            //}


        }
        private void refreshTabLora()
        {
            if (listBoxLoras.SelectedIndex == -1)
            {
                return;
            }
            textBoxLoraName.Text = loras[listBoxLoras.SelectedIndex].LoraName;
        }
        private void refreshTabLoraPart()
        {
            if (listBoxLoras.SelectedIndex == -1 || listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }
            int loraPartIndex = listBoxLoraParts.SelectedIndex;
            textBoxLoraPartText.Text = loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].Text;
            listBoxLoraParts.SelectedIndex = loraPartIndex;
            trackBarLoraPartWeight.Value = (int)(loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].Weight * 100);
            trackBarNumberOfParantheses.Value = loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].QuantityOfParantheses;
            checkBoxIsLora.Checked = loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].IsLora;
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
                //Move Selected index 
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
                promptParts[noScrollListBoxPromptParts.SelectedIndex].Weight += delta;
            }

            refreshListBoxPromptParts(true);

        }

        private void textBoxPrompt_TextChanged(object sender, EventArgs e)
        {
            checkBoxIgnorrePromptParts.Checked = true;
        }
        private void textBoxPromptPartName_TextChanged(object sender, EventArgs e)
        {
            //Change the name of the prompt part
            if (listBoxPromptsFromCatergory.SelectedIndex == -1)
            {
                return;
            }
            promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[listBoxPromptsFromCatergory.SelectedIndex].Text = textBoxPromptPartName.Text;
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
        private void textBoxLoraName_TextChanged(object sender, EventArgs e)
        {
            if (listBoxLoras.SelectedIndex == -1)
            {
                return;
            }
            loras[listBoxLoras.SelectedIndex].LoraName = textBoxLoraName.Text;
            refreshListBoxLoras(true);

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
            //Add a copy of all the parts from the selected Lory to the prompt parts list
            if (listBoxLoras.SelectedIndex == -1)
            {
                return;
            }
            foreach (PromptPart part in loras[listBoxLoras.SelectedIndex].Parts)
            {
                promptParts.Add((PromptPart)part.Clone());
            }
            refreshListBoxPromptParts(false);
        }

        private void trackBarPromptPartWeight_Scroll(object sender, EventArgs e)
        {
            //round to nearest 5
            trackBarPromptPartWeight.Value = (int)(Math.Round(trackBarPromptPartWeight.Value / 5.0) * 5);
            if (listBoxPromptsFromCatergory.SelectedIndex == -1)
            {
                return;
            }
            promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[listBoxPromptsFromCatergory.SelectedIndex].Weight = trackBarPromptPartWeight.Value / 100f;
            refreshTextBoxPromptPartWeight();
            refreshListBoxPromptsFromCategory(true);
        }
        private void trackBarNumberOfParentheses_Scroll(object sender, EventArgs e)
        {
            if (listBoxPromptsFromCatergory.SelectedIndex == -1)
            {
                return;
            }
            promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[listBoxPromptsFromCatergory.SelectedIndex].QuantityOfParantheses = trackBarNumberOfParantheses.Value;
            refreshListBoxPromptsFromCategory(true);
        }
        private void trackBarLoraPartWeight_Scroll(object sender, EventArgs e)
        {
            //round to nearest 5
            trackBarLoraPartWeight.Value = (int)(Math.Round(trackBarLoraPartWeight.Value / 5.0) * 5);
            if (listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }
            loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].Weight = trackBarLoraPartWeight.Value / 100f;
            refreshTextBoxLoraPartWeight();
            refreshListBoxPromptsFromLora(true);

        }
        private void trackBarLoraPartNumberOfParentheses_Scroll(object sender, EventArgs e)
        {
            if (listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }
            loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].QuantityOfParantheses = trackBarNumberOfParantheses.Value;
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
            textBoxLoraPartWeight.Text = loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].Weight.ToString();
        }

        private void textBoxLoraPartText_TextChanged(object sender, EventArgs e)
        {
            if (listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }
            loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].Text = textBoxLoraPartText.Text;
            refreshListBoxPromptsFromLora(true);
        }

        private void checkBoxIsLora_CheckedChanged(object sender, EventArgs e)
        {
            if (listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }
            loras[listBoxLoras.SelectedIndex].Parts[listBoxLoraParts.SelectedIndex].IsLora = checkBoxIsLora.Checked;
            refreshListBoxPromptsFromLora(true);
        }

        private void buttonDeleteLora_Click(object sender, EventArgs e)
        {
            if (listBoxLoras.SelectedIndex == -1)
            {
                return;
            }
            loras.RemoveAt(listBoxLoras.SelectedIndex);
            refreshListBoxLoras(false);
        }

        private void buttonDeleteLoraPart_Click(object sender, EventArgs e)
        {
            if (listBoxLoras.SelectedIndex == -1 || listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }
            loras[listBoxLoras.SelectedIndex].Parts.RemoveAt(listBoxLoraParts.SelectedIndex);
            refreshListBoxPromptsFromLora(false);
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

        private void buttonAddFromClipBoard_Click(object sender, EventArgs e)
        {
            string clipboardText = Clipboard.GetText();
            string[] parts = clipboardText.Split(',');
            foreach (string part in parts)
            {
                if (!string.IsNullOrEmpty(part.Trim()))
                {
                    promptParts.Add(new PromptPart(part.Trim()));
                }
            }
            refreshListBoxPromptParts(false);
        }

        private void numericUpDownClipSkip_ValueChanged(object sender, EventArgs e)
        {
            checkBoxClipSkip.Checked = true;
        }

        private void textBoxSelectedPromptPart_TextChanged(object sender, EventArgs e)
        {
            if (noScrollListBoxPromptParts.SelectedIndex != -1)
            {
                promptParts[noScrollListBoxPromptParts.SelectedIndex].Text = textBoxSelectedPromptPart.Text;

            }
        }

        private void noScrollListBoxPromptParts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noScrollListBoxPromptParts.SelectedIndex == -1)
            {
                textBoxSelectedPromptPart.Text = "";
                return;
            }
            
            textBoxSelectedPromptPart.Text = promptParts[noScrollListBoxPromptParts.SelectedIndex].Text;
        }

        private void textBoxSelectedPromptPart_Leave(object sender, EventArgs e)
        {
            refreshListBoxPromptParts(true);
        }

        private void buttonAddCustom_Click(object sender, EventArgs e)
        {
            promptParts.Add(new PromptPart("new Custom Prompt Part"));
            refreshListBoxPromptParts(false);
            noScrollListBoxPromptParts.SelectedIndex = noScrollListBoxPromptParts.Items.Count - 1;
            textBoxSelectedPromptPart.Select();
        }
    }
}
