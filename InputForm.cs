using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

        private BindingList<PromptPart> promptParts = new BindingList<PromptPart>();
        private BindingList<PromptPartCategory> promptPartCategories = new BindingList<PromptPartCategory>();
        private BindingList<Lora> _loras = new BindingList<Lora>();
        private BindingList<LoraCategory> _loraCategories = new BindingList<LoraCategory>();

        // BindingSources so we never need to "rebind" (DataSource = null) to refresh.
        private readonly BindingSource _bsPromptParts = new BindingSource();
        private readonly BindingSource _bsPromptPartCategories = new BindingSource();
        private readonly BindingSource _bsPromptsFromCategory = new BindingSource();
        private readonly BindingSource _bsLoraCategories = new BindingSource();
        private readonly BindingSource _bsLoras = new BindingSource();
        private readonly BindingSource _bsLoraParts = new BindingSource();

        // Prevent programmatic UI updates from re-triggering model updates while typing.
        private bool _suppressSelectedPromptPartTextChanged = false;

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

        public BindingList<PromptPart> PromptParts
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

            InitializeBindings();

            noScrollListBoxPromptParts.MouseWheel += MouseWheelOnPromptParts;
            listBoxCategory.MouseWheel += MouseWheelOnPromptPartsCategory;
            textBoxPrompt.AcceptsReturn = false;
            comboBoxSamplingMethod.DataSource = samplingMethods;
            comboBoxSamplingMethod.SelectedIndex = 0;

            this.AcceptButton = buttonGenerate;
            this.KeyPreview = true;
            this.KeyDown += InputForm_KeyDown;

            UpdateSelectedPromptPartControls();
        }

        private void InitializeBindings()
        {
            _bsPromptParts.DataSource = promptParts;
            noScrollListBoxPromptParts.DataSource = _bsPromptParts;
            noScrollListBoxPromptParts.DisplayMember = nameof(PromptPart.DisplayText);

            _bsPromptPartCategories.DataSource = promptPartCategories;
            listBoxCategory.DataSource = _bsPromptPartCategories;
            listBoxCategory.DisplayMember = nameof(PromptPartCategory.Name);

            listBoxPromptsFromCatergory.DataSource = _bsPromptsFromCategory;
            listBoxPromptsFromCatergory.DisplayMember = nameof(PromptPart.DisplayText);

            _bsLoraCategories.DataSource = _loraCategories;
            listBoxLoraCategories.DataSource = _bsLoraCategories;
            listBoxLoraCategories.DisplayMember = nameof(LoraCategory.LoraCategoryName);

            listBoxLoras.DataSource = _bsLoras;
            listBoxLoraParts.DataSource = _bsLoraParts;
            listBoxLoras.DisplayMember = nameof(Lora.LoraName);
            listBoxLoraParts.DisplayMember = nameof(PromptPart.DisplayText);

            RefreshCategoryDependentBindings(keepPromptIndex: false);
            RefreshLoraDependentBindings(keepLoraIndex: false, keepPartIndex: false);
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

        public Prompt BuildPromptSnapshotForSave()
        {
            var promptData = new Prompt();

            promptData.PromptParts = promptParts != null
                ? promptParts.Select(part => part.ToString()).ToArray()
                : Array.Empty<string>();

            promptData.NegativePromptParts = !string.IsNullOrWhiteSpace(textBoxNegativePrompt.Text)
                ? textBoxNegativePrompt.Text.Split(',').Select(part => part.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToArray()
                : Array.Empty<string>();

            promptData.Steps = trackBarSteps.Value.ToString();
            promptData.Sampler = samplingMethods[comboBoxSamplingMethod.SelectedIndex];
            promptData.CFGScale = trackBarCFG.Value.ToString();

            if (checkBoxClipSkip.Checked)
            {
                promptData.ClipSkip = decimal.ToInt32(numericUpDownClipSkip.Value).ToString();
            }

            if (checkHighRes.Checked)
            {
                promptData.EnableHR = "true";
            }

            return promptData;
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
                        _loraCategories[listBoxLoraCategories.SelectedIndex].Loras = new BindingList<Lora>();
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
            BeginInvoke((Action)(() =>
            {
                textBoxSelectedPromptPart.Focus();
                textBoxSelectedPromptPart.SelectAll();
            }));
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
            if (listBoxLoraCategories.SelectedIndex == -1 || listBoxLoras.SelectedIndex == -1)
            {
                return;
            }
            _loraCategories[listBoxLoraCategories.SelectedIndex].Loras.RemoveAt(listBoxLoras.SelectedIndex);
            refreshListBoxLoras(true);
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
                _loraCategories[comboBoxLoraCategoriestoMoveTo.SelectedIndex].Loras = new BindingList<Lora>();
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
            int topIndex = noScrollListBoxPromptParts.TopIndex;

            _bsPromptParts.ResetBindings(false);
            if (keepSelectedIndex)
            {
                if (selectedIndex >= 0 && selectedIndex < noScrollListBoxPromptParts.Items.Count)
                {
                    noScrollListBoxPromptParts.SelectedIndex = selectedIndex;
                }
            }

            if (noScrollListBoxPromptParts.Items.Count > 0)
            {
                int maxTop = Math.Max(0, noScrollListBoxPromptParts.Items.Count - 1);
                noScrollListBoxPromptParts.TopIndex = Math.Min(Math.Max(0, topIndex), maxTop);
            }
            UpdateSelectedPromptPartControls();
        }
        private void refreshListboxCategories(bool keepIndex)
        {
            int selectedIndex = listBoxCategory.SelectedIndex;
            int topIndex = listBoxCategory.TopIndex;

            _bsPromptPartCategories.ResetBindings(false);
            if (keepIndex)
            {
                if (selectedIndex >= 0 && selectedIndex < listBoxCategory.Items.Count)
                {
                    listBoxCategory.SelectedIndex = selectedIndex;
                }
            }

            if (listBoxCategory.Items.Count > 0)
            {
                int maxTop = Math.Max(0, listBoxCategory.Items.Count - 1);
                listBoxCategory.TopIndex = Math.Min(Math.Max(0, topIndex), maxTop);
            }
        }
        private void refreshListBoxPromptsFromCategory(bool keepSelectedIndex)
        {
            int selectedIndex = listBoxPromptsFromCatergory.SelectedIndex;
            RefreshCategoryDependentBindings(keepPromptIndex: keepSelectedIndex);

            if (keepSelectedIndex && selectedIndex >= 0 && selectedIndex < listBoxPromptsFromCatergory.Items.Count)
            {
                listBoxPromptsFromCatergory.SelectedIndex = selectedIndex;
            }
        }
        private void refreshListBoxLoraCategories(bool keepIndex)
        {
            int selectedIndex = listBoxLoraCategories.SelectedIndex;
            _bsLoraCategories.ResetBindings(false);
            if (keepIndex)
            {
                if (selectedIndex >= 0 && selectedIndex < listBoxLoraCategories.Items.Count)
                {
                    listBoxLoraCategories.SelectedIndex = selectedIndex;
                }
            }
        }
        private void refreshListBoxLoras(bool keepIndex)
        {
            if (listBoxLoraCategories.SelectedIndex == -1)
            {
                return;
            }
            int selectedIndex = listBoxLoras.SelectedIndex;
            RefreshLoraDependentBindings(keepLoraIndex: keepIndex, keepPartIndex: true);

            if (keepIndex && selectedIndex >= 0 && selectedIndex < listBoxLoras.Items.Count)
            {
                listBoxLoras.SelectedIndex = selectedIndex;
            }
        }
        private void refreshListBoxPromptsFromLora(bool keepIndex)
        {
            //Needs to be don in this order
            int selectedIndex = listBoxLoraParts.SelectedIndex;
            RefreshLoraDependentBindings(keepLoraIndex: true, keepPartIndex: keepIndex);

            if (keepIndex && selectedIndex >= 0 && selectedIndex < listBoxLoraParts.Items.Count)
            {
                listBoxLoraParts.SelectedIndex = selectedIndex;
            }
        }

        private void RefreshCategoryDependentBindings(bool keepPromptIndex)
        {
            int selectedPromptIndex = listBoxPromptsFromCatergory.SelectedIndex;

            if (listBoxCategory.SelectedIndex != -1)
            {
                _bsPromptsFromCategory.DataSource = promptPartCategories[listBoxCategory.SelectedIndex].PromptParts;
            }
            else
            {
                _bsPromptsFromCategory.DataSource = null;
            }

            _bsPromptsFromCategory.ResetBindings(false);

            if (keepPromptIndex && selectedPromptIndex >= 0 && selectedPromptIndex < listBoxPromptsFromCatergory.Items.Count)
            {
                listBoxPromptsFromCatergory.SelectedIndex = selectedPromptIndex;
            }
        }

        private void RefreshLoraDependentBindings(bool keepLoraIndex, bool keepPartIndex)
        {
            int selectedLoraIndex = listBoxLoras.SelectedIndex;
            int selectedPartIndex = listBoxLoraParts.SelectedIndex;

            if (listBoxLoraCategories.SelectedIndex != -1)
            {
                _bsLoras.DataSource = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras;
            }
            else
            {
                _bsLoras.DataSource = null;
            }

            _bsLoras.ResetBindings(false);

            if (keepLoraIndex && selectedLoraIndex >= 0 && selectedLoraIndex < listBoxLoras.Items.Count)
            {
                listBoxLoras.SelectedIndex = selectedLoraIndex;
            }

            if (listBoxLoraCategories.SelectedIndex != -1 && listBoxLoras.SelectedIndex != -1)
            {
                _bsLoraParts.DataSource = _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts;
            }
            else
            {
                _bsLoraParts.DataSource = null;
            }

            _bsLoraParts.ResetBindings(false);

            if (keepPartIndex && selectedPartIndex >= 0 && selectedPartIndex < listBoxLoraParts.Items.Count)
            {
                listBoxLoraParts.SelectedIndex = selectedPartIndex;
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

                // Reorder in the underlying BindingList and refresh selection/UI state.
                refreshListBoxPromptParts(true);
            }
            else
            {
                delta = Math.Min(0.05f, delta);
                delta = Math.Max(-0.05f, delta);

                var selectedIndex = noScrollListBoxPromptParts.SelectedIndex;
                promptParts[selectedIndex].Weight += delta;
            }
        }

        private void textBoxPrompt_TextChanged(object sender, EventArgs e)
        {
            checkBoxIgnorrePromptParts.Checked = true;
        }
        private void textBoxSelectedPromptPart_TextChanged(object sender, EventArgs e)
        {
            // Minimal, side-effect-free: only update the underlying model for the currently selected item.
            // No rebinding, no selection changes, no refresh calls.
            if (_suppressSelectedPromptPartTextChanged)
            {
                return;
            }

            int idx = noScrollListBoxPromptParts.SelectedIndex;
            if (idx < 0 || idx >= promptParts.Count)
            {
                return;
            }

            promptParts[idx].Text = textBoxSelectedPromptPart.Text;
        }
        private void textBoxPromptPartName_TextChanged(object sender, EventArgs e)
        {
            // Change the name of the prompt part
            if (listBoxPromptsFromCatergory.SelectedIndex == -1)
            {
                return;
            }

            int idx = listBoxPromptsFromCatergory.SelectedIndex;
            promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[idx].Text = textBoxPromptPartName.Text;

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

            int idx = listBoxLoraParts.SelectedIndex;
            _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts[idx].Text = textBoxLoraPartText.Text;
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

            int idx = listBoxPromptsFromCatergory.SelectedIndex;
            promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[idx].Weight = trackBarPromptPartWeight.Value / 100f;

            refreshTextBoxPromptPartWeight();
            refreshListBoxPromptsFromCategory(true);
        }
        private void trackBarNumberOfParentheses_Scroll(object sender, EventArgs e)
        {
            if (listBoxPromptsFromCatergory.SelectedIndex == -1)
            {
                return;
            }

            int idx = listBoxPromptsFromCatergory.SelectedIndex;
            promptPartCategories[listBoxCategory.SelectedIndex].PromptParts[idx].QuantityOfParantheses = trackBarNumberOfParantheses.Value;

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

            int idx = listBoxLoraParts.SelectedIndex;
            _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts[idx].Weight = trackBarLoraPartWeight.Value / 100f;

            refreshTextBoxLoraPartWeight();
            refreshListBoxPromptsFromLora(true);
        }
        private void trackBarLoraPartNumberOfParentheses_Scroll(object sender, EventArgs e)
        {
            if (listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }

            int idx = listBoxLoraParts.SelectedIndex;
            _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts[idx].QuantityOfParantheses = trackBarNumberOfParantheses.Value;
        }





        private void checkBoxIsLora_CheckedChanged(object sender, EventArgs e)
        {
            if (listBoxLoraParts.SelectedIndex == -1)
            {
                return;
            }

            int idx = listBoxLoraParts.SelectedIndex;
            _loraCategories[listBoxLoraCategories.SelectedIndex].Loras[listBoxLoras.SelectedIndex].Parts[idx].IsLora = checkBoxIsLora.Checked;

            // Refresh the UI
            refreshListBoxPromptsFromLora(true);
        }




        public void SetupForm(IEnumerable<PromptPart> prompts, string negativeprompts)
        {
            promptParts = new BindingList<PromptPart>((prompts ?? Array.Empty<PromptPart>()).ToList());
            _bsPromptParts.DataSource = promptParts;
            refreshListBoxPromptParts(false);
            textBoxNegativePrompt.Text = negativeprompts;
        }

        public void SetupForm(Prompt saved)
        {
            // Prompt parts (parse back into structured PromptPart so sliders/IsLora can match)
            var parts = saved.PromptParts ?? Array.Empty<string>();
            promptParts = new BindingList<PromptPart>(parts.Select(PromptPart.ParseFromPromptStringOrDefault).ToList());
            _bsPromptParts.DataSource = promptParts;
            refreshListBoxPromptParts(false);

            // Negative prompt
            textBoxNegativePrompt.Text = saved.NegativePromptParts != null
                ? string.Join(", ", saved.NegativePromptParts.Select(p => p?.Trim()).Where(p => !string.IsNullOrEmpty(p)))
                : string.Empty;

            // Steps
            if (int.TryParse(saved.Steps, NumberStyles.Integer, CultureInfo.InvariantCulture, out int steps))
            {
                steps = Math.Max(trackBarSteps.Minimum, Math.Min(trackBarSteps.Maximum, steps));
                trackBarSteps.Value = steps;
                textBoxSteps.Text = steps.ToString(CultureInfo.InvariantCulture);
            }

            // CFG
            int cfgValue;
            if (int.TryParse(saved.CFGScale, NumberStyles.Integer, CultureInfo.InvariantCulture, out cfgValue) ||
                (double.TryParse(saved.CFGScale, NumberStyles.Float, CultureInfo.InvariantCulture, out double cfgDouble) &&
                 (cfgValue = (int)Math.Round(cfgDouble)) >= 0))
            {
                cfgValue = Math.Max(trackBarCFG.Minimum, Math.Min(trackBarCFG.Maximum, cfgValue));
                trackBarCFG.Value = cfgValue;
                textBoxCFG.Text = cfgValue.ToString(CultureInfo.InvariantCulture);
            }

            // Sampler
            if (!string.IsNullOrWhiteSpace(saved.Sampler))
            {
                int idx = Array.IndexOf(samplingMethods, saved.Sampler);
                if (idx >= 0)
                {
                    comboBoxSamplingMethod.SelectedIndex = idx;
                }
            }

            // Clip skip
            if (int.TryParse(saved.ClipSkip, NumberStyles.Integer, CultureInfo.InvariantCulture, out int clipSkip))
            {
                checkBoxClipSkip.Checked = true;
                decimal val = clipSkip;
                if (val < numericUpDownClipSkip.Minimum) val = numericUpDownClipSkip.Minimum;
                if (val > numericUpDownClipSkip.Maximum) val = numericUpDownClipSkip.Maximum;
                numericUpDownClipSkip.Value = val;
            }
            else
            {
                checkBoxClipSkip.Checked = false;
            }

            // High res (checkbox only)
            bool enableHr =
                IsTrueLike(saved.EnableHR) ||
                !string.IsNullOrWhiteSpace(saved.HiresUpscaler) ||
                !string.IsNullOrWhiteSpace(saved.HiresUpscalerName) ||
                !string.IsNullOrWhiteSpace(saved.HiresUpscale) ||
                !string.IsNullOrWhiteSpace(saved.HiresScale) ||
                !string.IsNullOrWhiteSpace(saved.HiresSteps) ||
                !string.IsNullOrWhiteSpace(saved.DenoisingStrength);

            checkHighRes.Checked = enableHr;
        }

        private static bool IsTrueLike(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            string v = value.Trim();
            return v.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                   v.Equals("1", StringComparison.OrdinalIgnoreCase) ||
                   v.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                   v.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                   v.Equals("on", StringComparison.OrdinalIgnoreCase);
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
            // With BindingList + INotifyPropertyChanged, we don't need to force-refresh on leave.
        }

        private void InputForm_Load(object sender, EventArgs e)
        {

        }

        private void InputForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.E)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                buttonAddCustom.PerformClick();
            }
        }

        private void UpdateSelectedPromptPartControls()
        {
            int selectedIndex = noScrollListBoxPromptParts.SelectedIndex;
            bool hasSelection = selectedIndex != -1;

            // Some bound-list updates can momentarily cause SelectedIndex to flicker to -1.
            // Don't disable/clear the textbox while the user is actively typing.
            if (!hasSelection && textBoxSelectedPromptPart.Focused)
            {
                return;
            }

            textBoxSelectedPromptPart.Enabled = hasSelection;

            if (!hasSelection)
            {
                _suppressSelectedPromptPartTextChanged = true;
                try
                {
                    textBoxSelectedPromptPart.Text = string.Empty;
                }
                finally
                {
                    _suppressSelectedPromptPartTextChanged = false;
                }
                return;
            }

            string desired = promptParts[selectedIndex].Text ?? string.Empty;
            if (!string.Equals(textBoxSelectedPromptPart.Text, desired, StringComparison.Ordinal))
            {
                _suppressSelectedPromptPartTextChanged = true;
                try
                {
                    textBoxSelectedPromptPart.Text = desired;
                    textBoxSelectedPromptPart.SelectionStart = textBoxSelectedPromptPart.TextLength;
                    textBoxSelectedPromptPart.SelectionLength = 0;
                }
                finally
                {
                    _suppressSelectedPromptPartTextChanged = false;
                }
            }
        }
    }
}
