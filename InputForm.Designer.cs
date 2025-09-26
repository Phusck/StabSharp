namespace StabSharp
{
    partial class InputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputForm));
            this.textBoxPrompt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.checkBoxIgnorrePromptParts = new System.Windows.Forms.CheckBox();
            this.textBoxNegativePrompt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonNewCategory = new System.Windows.Forms.Button();
            this.listBoxCategory = new System.Windows.Forms.ListBox();
            this.tabControlDetails = new System.Windows.Forms.TabControl();
            this.tabPageCategory = new System.Windows.Forms.TabPage();
            this.buttonDeleteCategory = new System.Windows.Forms.Button();
            this.labelCatergoryName = new System.Windows.Forms.Label();
            this.textBoxPromptPartCategoryName = new System.Windows.Forms.TextBox();
            this.tabPagePromptPart = new System.Windows.Forms.TabPage();
            this.trackBarNumberOfParantheses = new System.Windows.Forms.TrackBar();
            this.textBoxPromptPartWeight = new System.Windows.Forms.TextBox();
            this.trackBarPromptPartWeight = new System.Windows.Forms.TrackBar();
            this.textBoxPromptPartName = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.labelLoraCategoryName = new System.Windows.Forms.Label();
            this.textBoxLoraCategoryName = new System.Windows.Forms.TextBox();
            this.tabLoras = new System.Windows.Forms.TabPage();
            this.comboBoxLoraCategoriestoMoveTo = new System.Windows.Forms.ComboBox();
            this.buttonMoveLoraToOtherCategory = new System.Windows.Forms.Button();
            this.buttonDeleteLora = new System.Windows.Forms.Button();
            this.labelLoraName = new System.Windows.Forms.Label();
            this.textBoxLoraName = new System.Windows.Forms.TextBox();
            this.tabPageLoraParts = new System.Windows.Forms.TabPage();
            this.buttonDeleteLoraPart = new System.Windows.Forms.Button();
            this.checkBoxIsLora = new System.Windows.Forms.CheckBox();
            this.trackBarLoraPartNumberOfParantheses = new System.Windows.Forms.TrackBar();
            this.textBoxLoraPartWeight = new System.Windows.Forms.TextBox();
            this.trackBarLoraPartWeight = new System.Windows.Forms.TrackBar();
            this.textBoxLoraPartText = new System.Windows.Forms.TextBox();
            this.listBoxPromptsFromCatergory = new System.Windows.Forms.ListBox();
            this.buttonNewPromptPartForCategory = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.listBoxLoras = new System.Windows.Forms.ListBox();
            this.listBoxLoraParts = new System.Windows.Forms.ListBox();
            this.buttonNewLora = new System.Windows.Forms.Button();
            this.buttonNewLoraPart = new System.Windows.Forms.Button();
            this.trackBarSteps = new System.Windows.Forms.TrackBar();
            this.buttonAddFromClipBoard = new System.Windows.Forms.Button();
            this.comboBoxSamplingMethod = new System.Windows.Forms.ComboBox();
            this.numericUpDownClipSkip = new System.Windows.Forms.NumericUpDown();
            this.checkBoxClipSkip = new System.Windows.Forms.CheckBox();
            this.labelCurretlySelected = new System.Windows.Forms.Label();
            this.textBoxSelectedPromptPart = new System.Windows.Forms.TextBox();
            this.buttonAddCustom = new System.Windows.Forms.Button();
            this.buttonAddListOfPromptPartsFromClipBoard = new System.Windows.Forms.Button();
            this.listBoxLoraCategories = new System.Windows.Forms.ListBox();
            this.buttonNewLoraCategory = new System.Windows.Forms.Button();
            this.noScrollListBoxPromptParts = new StabSharp.NoScrollListBox();
            this.textBoxSteps = new System.Windows.Forms.TextBox();
            this.textBoxCFG = new System.Windows.Forms.TextBox();
            this.trackBarCFG = new System.Windows.Forms.TrackBar();
            this.checkHighRes = new System.Windows.Forms.CheckBox();
            this.tabControlDetails.SuspendLayout();
            this.tabPageCategory.SuspendLayout();
            this.tabPagePromptPart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNumberOfParantheses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPromptPartWeight)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabLoras.SuspendLayout();
            this.tabPageLoraParts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLoraPartNumberOfParantheses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLoraPartWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClipSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCFG)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxPrompt
            // 
            this.textBoxPrompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPrompt.Location = new System.Drawing.Point(8, 758);
            this.textBoxPrompt.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPrompt.Multiline = true;
            this.textBoxPrompt.Name = "textBoxPrompt";
            this.textBoxPrompt.Size = new System.Drawing.Size(273, 67);
            this.textBoxPrompt.TabIndex = 6;
            this.textBoxPrompt.Text = "Metal Golem, Cowboy Hat";
            this.textBoxPrompt.TextChanged += new System.EventHandler(this.textBoxPrompt_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 743);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(594, 8);
            this.buttonGenerate.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(77, 37);
            this.buttonGenerate.TabIndex = 4;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // checkBoxIgnorrePromptParts
            // 
            this.checkBoxIgnorrePromptParts.AutoSize = true;
            this.checkBoxIgnorrePromptParts.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxIgnorrePromptParts.Location = new System.Drawing.Point(285, 761);
            this.checkBoxIgnorrePromptParts.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxIgnorrePromptParts.Name = "checkBoxIgnorrePromptParts";
            this.checkBoxIgnorrePromptParts.Size = new System.Drawing.Size(165, 22);
            this.checkBoxIgnorrePromptParts.TabIndex = 8;
            this.checkBoxIgnorrePromptParts.Text = "Ignorre Prompt Parts";
            this.checkBoxIgnorrePromptParts.UseVisualStyleBackColor = true;
            // 
            // textBoxNegativePrompt
            // 
            this.textBoxNegativePrompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNegativePrompt.Location = new System.Drawing.Point(8, 840);
            this.textBoxNegativePrompt.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxNegativePrompt.Multiline = true;
            this.textBoxNegativePrompt.Name = "textBoxNegativePrompt";
            this.textBoxNegativePrompt.Size = new System.Drawing.Size(273, 67);
            this.textBoxNegativePrompt.TabIndex = 10;
            this.textBoxNegativePrompt.Text = resources.GetString("textBoxNegativePrompt.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 824);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "label2";
            // 
            // buttonNewCategory
            // 
            this.buttonNewCategory.Location = new System.Drawing.Point(868, 23);
            this.buttonNewCategory.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewCategory.Name = "buttonNewCategory";
            this.buttonNewCategory.Size = new System.Drawing.Size(77, 37);
            this.buttonNewCategory.TabIndex = 12;
            this.buttonNewCategory.Text = "New Category";
            this.buttonNewCategory.UseVisualStyleBackColor = true;
            this.buttonNewCategory.Click += new System.EventHandler(this.buttonNewCategory_Click);
            // 
            // listBoxCategory
            // 
            this.listBoxCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxCategory.FormattingEnabled = true;
            this.listBoxCategory.ItemHeight = 20;
            this.listBoxCategory.Location = new System.Drawing.Point(868, 63);
            this.listBoxCategory.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxCategory.Name = "listBoxCategory";
            this.listBoxCategory.Size = new System.Drawing.Size(241, 364);
            this.listBoxCategory.TabIndex = 13;
            this.listBoxCategory.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBoxCategory_MouseClick);
            // 
            // tabControlDetails
            // 
            this.tabControlDetails.Controls.Add(this.tabPageCategory);
            this.tabControlDetails.Controls.Add(this.tabPagePromptPart);
            this.tabControlDetails.Controls.Add(this.tabPage1);
            this.tabControlDetails.Controls.Add(this.tabLoras);
            this.tabControlDetails.Controls.Add(this.tabPageLoraParts);
            this.tabControlDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlDetails.Location = new System.Drawing.Point(1553, 68);
            this.tabControlDetails.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlDetails.Name = "tabControlDetails";
            this.tabControlDetails.SelectedIndex = 0;
            this.tabControlDetails.Size = new System.Drawing.Size(510, 362);
            this.tabControlDetails.TabIndex = 14;
            // 
            // tabPageCategory
            // 
            this.tabPageCategory.Controls.Add(this.buttonDeleteCategory);
            this.tabPageCategory.Controls.Add(this.labelCatergoryName);
            this.tabPageCategory.Controls.Add(this.textBoxPromptPartCategoryName);
            this.tabPageCategory.Location = new System.Drawing.Point(4, 29);
            this.tabPageCategory.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageCategory.Name = "tabPageCategory";
            this.tabPageCategory.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageCategory.Size = new System.Drawing.Size(502, 329);
            this.tabPageCategory.TabIndex = 0;
            this.tabPageCategory.Text = "Category";
            this.tabPageCategory.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteCategory
            // 
            this.buttonDeleteCategory.Location = new System.Drawing.Point(252, 71);
            this.buttonDeleteCategory.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDeleteCategory.Name = "buttonDeleteCategory";
            this.buttonDeleteCategory.Size = new System.Drawing.Size(72, 35);
            this.buttonDeleteCategory.TabIndex = 2;
            this.buttonDeleteCategory.Text = "Delete";
            this.buttonDeleteCategory.UseVisualStyleBackColor = true;
            this.buttonDeleteCategory.Click += new System.EventHandler(this.buttonDeleteCategory_Click);
            // 
            // labelCatergoryName
            // 
            this.labelCatergoryName.AutoSize = true;
            this.labelCatergoryName.Location = new System.Drawing.Point(21, 15);
            this.labelCatergoryName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCatergoryName.Name = "labelCatergoryName";
            this.labelCatergoryName.Size = new System.Drawing.Size(124, 20);
            this.labelCatergoryName.TabIndex = 1;
            this.labelCatergoryName.Text = "Catergory Name";
            // 
            // textBoxPromptPartCategoryName
            // 
            this.textBoxPromptPartCategoryName.Location = new System.Drawing.Point(167, 12);
            this.textBoxPromptPartCategoryName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPromptPartCategoryName.Name = "textBoxPromptPartCategoryName";
            this.textBoxPromptPartCategoryName.Size = new System.Drawing.Size(175, 26);
            this.textBoxPromptPartCategoryName.TabIndex = 0;
            this.textBoxPromptPartCategoryName.TextChanged += new System.EventHandler(this.textBoxPromptPartCategoryName_TextChanged);
            // 
            // tabPagePromptPart
            // 
            this.tabPagePromptPart.Controls.Add(this.trackBarNumberOfParantheses);
            this.tabPagePromptPart.Controls.Add(this.textBoxPromptPartWeight);
            this.tabPagePromptPart.Controls.Add(this.trackBarPromptPartWeight);
            this.tabPagePromptPart.Controls.Add(this.textBoxPromptPartName);
            this.tabPagePromptPart.Location = new System.Drawing.Point(4, 29);
            this.tabPagePromptPart.Margin = new System.Windows.Forms.Padding(2);
            this.tabPagePromptPart.Name = "tabPagePromptPart";
            this.tabPagePromptPart.Padding = new System.Windows.Forms.Padding(2);
            this.tabPagePromptPart.Size = new System.Drawing.Size(502, 329);
            this.tabPagePromptPart.TabIndex = 1;
            this.tabPagePromptPart.Text = "Prompt Part";
            this.tabPagePromptPart.UseVisualStyleBackColor = true;
            // 
            // trackBarNumberOfParantheses
            // 
            this.trackBarNumberOfParantheses.LargeChange = 50;
            this.trackBarNumberOfParantheses.Location = new System.Drawing.Point(52, 127);
            this.trackBarNumberOfParantheses.Margin = new System.Windows.Forms.Padding(2);
            this.trackBarNumberOfParantheses.Maximum = 4;
            this.trackBarNumberOfParantheses.Name = "trackBarNumberOfParantheses";
            this.trackBarNumberOfParantheses.Size = new System.Drawing.Size(141, 45);
            this.trackBarNumberOfParantheses.TabIndex = 4;
            this.trackBarNumberOfParantheses.Scroll += new System.EventHandler(this.trackBarNumberOfParentheses_Scroll);
            // 
            // textBoxPromptPartWeight
            // 
            this.textBoxPromptPartWeight.Location = new System.Drawing.Point(262, 56);
            this.textBoxPromptPartWeight.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPromptPartWeight.Name = "textBoxPromptPartWeight";
            this.textBoxPromptPartWeight.ReadOnly = true;
            this.textBoxPromptPartWeight.Size = new System.Drawing.Size(48, 26);
            this.textBoxPromptPartWeight.TabIndex = 3;
            // 
            // trackBarPromptPartWeight
            // 
            this.trackBarPromptPartWeight.LargeChange = 50;
            this.trackBarPromptPartWeight.Location = new System.Drawing.Point(52, 65);
            this.trackBarPromptPartWeight.Margin = new System.Windows.Forms.Padding(2);
            this.trackBarPromptPartWeight.Maximum = 200;
            this.trackBarPromptPartWeight.Name = "trackBarPromptPartWeight";
            this.trackBarPromptPartWeight.Size = new System.Drawing.Size(141, 45);
            this.trackBarPromptPartWeight.SmallChange = 5;
            this.trackBarPromptPartWeight.TabIndex = 2;
            this.trackBarPromptPartWeight.TickFrequency = 5;
            this.trackBarPromptPartWeight.Value = 100;
            this.trackBarPromptPartWeight.Scroll += new System.EventHandler(this.trackBarPromptPartWeight_Scroll);
            // 
            // textBoxPromptPartName
            // 
            this.textBoxPromptPartName.Location = new System.Drawing.Point(52, 35);
            this.textBoxPromptPartName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPromptPartName.Name = "textBoxPromptPartName";
            this.textBoxPromptPartName.Size = new System.Drawing.Size(175, 26);
            this.textBoxPromptPartName.TabIndex = 1;
            this.textBoxPromptPartName.TextChanged += new System.EventHandler(this.textBoxPromptPartName_TextChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.labelLoraCategoryName);
            this.tabPage1.Controls.Add(this.textBoxLoraCategoryName);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(502, 329);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "Lora Categories";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(387, 94);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 34);
            this.button1.TabIndex = 8;
            this.button1.Text = "Delete";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // labelLoraCategoryName
            // 
            this.labelLoraCategoryName.AutoSize = true;
            this.labelLoraCategoryName.Location = new System.Drawing.Point(23, 44);
            this.labelLoraCategoryName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLoraCategoryName.Name = "labelLoraCategoryName";
            this.labelLoraCategoryName.Size = new System.Drawing.Size(159, 20);
            this.labelLoraCategoryName.TabIndex = 7;
            this.labelLoraCategoryName.Text = "Lora Category Name:";
            // 
            // textBoxLoraCategoryName
            // 
            this.textBoxLoraCategoryName.Location = new System.Drawing.Point(186, 44);
            this.textBoxLoraCategoryName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLoraCategoryName.Name = "textBoxLoraCategoryName";
            this.textBoxLoraCategoryName.Size = new System.Drawing.Size(279, 26);
            this.textBoxLoraCategoryName.TabIndex = 6;
            this.textBoxLoraCategoryName.TextChanged += new System.EventHandler(this.textBoxLoraCategoryName_TextChanged);
            // 
            // tabLoras
            // 
            this.tabLoras.Controls.Add(this.comboBoxLoraCategoriestoMoveTo);
            this.tabLoras.Controls.Add(this.buttonMoveLoraToOtherCategory);
            this.tabLoras.Controls.Add(this.buttonDeleteLora);
            this.tabLoras.Controls.Add(this.labelLoraName);
            this.tabLoras.Controls.Add(this.textBoxLoraName);
            this.tabLoras.Location = new System.Drawing.Point(4, 29);
            this.tabLoras.Margin = new System.Windows.Forms.Padding(2);
            this.tabLoras.Name = "tabLoras";
            this.tabLoras.Padding = new System.Windows.Forms.Padding(2);
            this.tabLoras.Size = new System.Drawing.Size(502, 329);
            this.tabLoras.TabIndex = 2;
            this.tabLoras.Text = "Lora";
            this.tabLoras.UseVisualStyleBackColor = true;
            // 
            // comboBoxLoraCategoriestoMoveTo
            // 
            this.comboBoxLoraCategoriestoMoveTo.FormattingEnabled = true;
            this.comboBoxLoraCategoriestoMoveTo.Location = new System.Drawing.Point(178, 220);
            this.comboBoxLoraCategoriestoMoveTo.Name = "comboBoxLoraCategoriestoMoveTo";
            this.comboBoxLoraCategoriestoMoveTo.Size = new System.Drawing.Size(121, 28);
            this.comboBoxLoraCategoriestoMoveTo.TabIndex = 7;
            // 
            // buttonMoveLoraToOtherCategory
            // 
            this.buttonMoveLoraToOtherCategory.Location = new System.Drawing.Point(95, 216);
            this.buttonMoveLoraToOtherCategory.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMoveLoraToOtherCategory.Name = "buttonMoveLoraToOtherCategory";
            this.buttonMoveLoraToOtherCategory.Size = new System.Drawing.Size(78, 34);
            this.buttonMoveLoraToOtherCategory.TabIndex = 6;
            this.buttonMoveLoraToOtherCategory.Text = "Move";
            this.buttonMoveLoraToOtherCategory.UseVisualStyleBackColor = true;
            this.buttonMoveLoraToOtherCategory.Click += new System.EventHandler(this.buttonMoveLoraToOtherCategory_Click);
            // 
            // buttonDeleteLora
            // 
            this.buttonDeleteLora.Location = new System.Drawing.Point(233, 32);
            this.buttonDeleteLora.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDeleteLora.Name = "buttonDeleteLora";
            this.buttonDeleteLora.Size = new System.Drawing.Size(78, 34);
            this.buttonDeleteLora.TabIndex = 5;
            this.buttonDeleteLora.Text = "Delete";
            this.buttonDeleteLora.UseVisualStyleBackColor = true;
            this.buttonDeleteLora.Click += new System.EventHandler(this.buttonDeleteLora_Click);
            // 
            // labelLoraName
            // 
            this.labelLoraName.AutoSize = true;
            this.labelLoraName.Location = new System.Drawing.Point(4, 5);
            this.labelLoraName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLoraName.Name = "labelLoraName";
            this.labelLoraName.Size = new System.Drawing.Size(87, 20);
            this.labelLoraName.TabIndex = 4;
            this.labelLoraName.Text = "Lora Name";
            // 
            // textBoxLoraName
            // 
            this.textBoxLoraName.Location = new System.Drawing.Point(95, 2);
            this.textBoxLoraName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLoraName.Name = "textBoxLoraName";
            this.textBoxLoraName.Size = new System.Drawing.Size(175, 26);
            this.textBoxLoraName.TabIndex = 3;
            this.textBoxLoraName.TextChanged += new System.EventHandler(this.textBoxLoraName_TextChanged);
            // 
            // tabPageLoraParts
            // 
            this.tabPageLoraParts.Controls.Add(this.buttonDeleteLoraPart);
            this.tabPageLoraParts.Controls.Add(this.checkBoxIsLora);
            this.tabPageLoraParts.Controls.Add(this.trackBarLoraPartNumberOfParantheses);
            this.tabPageLoraParts.Controls.Add(this.textBoxLoraPartWeight);
            this.tabPageLoraParts.Controls.Add(this.trackBarLoraPartWeight);
            this.tabPageLoraParts.Controls.Add(this.textBoxLoraPartText);
            this.tabPageLoraParts.Location = new System.Drawing.Point(4, 29);
            this.tabPageLoraParts.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageLoraParts.Name = "tabPageLoraParts";
            this.tabPageLoraParts.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageLoraParts.Size = new System.Drawing.Size(502, 329);
            this.tabPageLoraParts.TabIndex = 3;
            this.tabPageLoraParts.Text = "Lora Parts";
            this.tabPageLoraParts.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteLoraPart
            // 
            this.buttonDeleteLoraPart.Location = new System.Drawing.Point(269, 11);
            this.buttonDeleteLoraPart.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDeleteLoraPart.Name = "buttonDeleteLoraPart";
            this.buttonDeleteLoraPart.Size = new System.Drawing.Size(77, 42);
            this.buttonDeleteLoraPart.TabIndex = 10;
            this.buttonDeleteLoraPart.Text = "Delete";
            this.buttonDeleteLoraPart.UseVisualStyleBackColor = true;
            this.buttonDeleteLoraPart.Click += new System.EventHandler(this.buttonDeleteLoraPart_Click);
            // 
            // checkBoxIsLora
            // 
            this.checkBoxIsLora.AutoSize = true;
            this.checkBoxIsLora.Location = new System.Drawing.Point(242, 110);
            this.checkBoxIsLora.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxIsLora.Name = "checkBoxIsLora";
            this.checkBoxIsLora.Size = new System.Drawing.Size(77, 24);
            this.checkBoxIsLora.TabIndex = 9;
            this.checkBoxIsLora.Text = "Is Lora";
            this.checkBoxIsLora.UseVisualStyleBackColor = true;
            this.checkBoxIsLora.CheckedChanged += new System.EventHandler(this.checkBoxIsLora_CheckedChanged);
            // 
            // trackBarLoraPartNumberOfParantheses
            // 
            this.trackBarLoraPartNumberOfParantheses.LargeChange = 50;
            this.trackBarLoraPartNumberOfParantheses.Location = new System.Drawing.Point(60, 100);
            this.trackBarLoraPartNumberOfParantheses.Margin = new System.Windows.Forms.Padding(2);
            this.trackBarLoraPartNumberOfParantheses.Maximum = 4;
            this.trackBarLoraPartNumberOfParantheses.Name = "trackBarLoraPartNumberOfParantheses";
            this.trackBarLoraPartNumberOfParantheses.Size = new System.Drawing.Size(141, 45);
            this.trackBarLoraPartNumberOfParantheses.TabIndex = 8;
            this.trackBarLoraPartNumberOfParantheses.Scroll += new System.EventHandler(this.trackBarLoraPartNumberOfParentheses_Scroll);
            // 
            // textBoxLoraPartWeight
            // 
            this.textBoxLoraPartWeight.Location = new System.Drawing.Point(210, 52);
            this.textBoxLoraPartWeight.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLoraPartWeight.Name = "textBoxLoraPartWeight";
            this.textBoxLoraPartWeight.ReadOnly = true;
            this.textBoxLoraPartWeight.Size = new System.Drawing.Size(48, 26);
            this.textBoxLoraPartWeight.TabIndex = 7;
            // 
            // trackBarLoraPartWeight
            // 
            this.trackBarLoraPartWeight.LargeChange = 50;
            this.trackBarLoraPartWeight.Location = new System.Drawing.Point(60, 44);
            this.trackBarLoraPartWeight.Margin = new System.Windows.Forms.Padding(2);
            this.trackBarLoraPartWeight.Maximum = 200;
            this.trackBarLoraPartWeight.Name = "trackBarLoraPartWeight";
            this.trackBarLoraPartWeight.Size = new System.Drawing.Size(141, 45);
            this.trackBarLoraPartWeight.SmallChange = 5;
            this.trackBarLoraPartWeight.TabIndex = 6;
            this.trackBarLoraPartWeight.TickFrequency = 5;
            this.trackBarLoraPartWeight.Value = 100;
            this.trackBarLoraPartWeight.Scroll += new System.EventHandler(this.trackBarLoraPartWeight_Scroll);
            // 
            // textBoxLoraPartText
            // 
            this.textBoxLoraPartText.Location = new System.Drawing.Point(60, 11);
            this.textBoxLoraPartText.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLoraPartText.Name = "textBoxLoraPartText";
            this.textBoxLoraPartText.Size = new System.Drawing.Size(175, 26);
            this.textBoxLoraPartText.TabIndex = 5;
            this.textBoxLoraPartText.TextChanged += new System.EventHandler(this.textBoxLoraPartText_TextChanged);
            // 
            // listBoxPromptsFromCatergory
            // 
            this.listBoxPromptsFromCatergory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPromptsFromCatergory.FormattingEnabled = true;
            this.listBoxPromptsFromCatergory.ItemHeight = 20;
            this.listBoxPromptsFromCatergory.Location = new System.Drawing.Point(1113, 61);
            this.listBoxPromptsFromCatergory.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxPromptsFromCatergory.Name = "listBoxPromptsFromCatergory";
            this.listBoxPromptsFromCatergory.Size = new System.Drawing.Size(263, 364);
            this.listBoxPromptsFromCatergory.TabIndex = 15;
            this.listBoxPromptsFromCatergory.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBoxPromptsFromCatergory_MouseClick);
            this.listBoxPromptsFromCatergory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxPromptsFromCatergory_MouseDoubleClick);
            // 
            // buttonNewPromptPartForCategory
            // 
            this.buttonNewPromptPartForCategory.Location = new System.Drawing.Point(1113, 22);
            this.buttonNewPromptPartForCategory.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewPromptPartForCategory.Name = "buttonNewPromptPartForCategory";
            this.buttonNewPromptPartForCategory.Size = new System.Drawing.Size(77, 37);
            this.buttonNewPromptPartForCategory.TabIndex = 16;
            this.buttonNewPromptPartForCategory.Text = "New Prompt Part";
            this.buttonNewPromptPartForCategory.UseVisualStyleBackColor = true;
            this.buttonNewPromptPartForCategory.Click += new System.EventHandler(this.buttonNewPromptPartForCategory_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(1834, 27);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(77, 37);
            this.buttonSave.TabIndex = 17;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // listBoxLoras
            // 
            this.listBoxLoras.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxLoras.FormattingEnabled = true;
            this.listBoxLoras.ItemHeight = 20;
            this.listBoxLoras.Location = new System.Drawing.Point(1113, 492);
            this.listBoxLoras.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxLoras.Name = "listBoxLoras";
            this.listBoxLoras.Size = new System.Drawing.Size(241, 364);
            this.listBoxLoras.TabIndex = 18;
            this.listBoxLoras.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBoxLoras_MouseClick);
            this.listBoxLoras.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxLoras_MouseDoubleClick);
            // 
            // listBoxLoraParts
            // 
            this.listBoxLoraParts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxLoraParts.FormattingEnabled = true;
            this.listBoxLoraParts.ItemHeight = 20;
            this.listBoxLoraParts.Location = new System.Drawing.Point(1358, 492);
            this.listBoxLoraParts.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxLoraParts.Name = "listBoxLoraParts";
            this.listBoxLoraParts.Size = new System.Drawing.Size(263, 364);
            this.listBoxLoraParts.TabIndex = 19;
            this.listBoxLoraParts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBoxLoraParts_MouseClick);
            // 
            // buttonNewLora
            // 
            this.buttonNewLora.Location = new System.Drawing.Point(1113, 452);
            this.buttonNewLora.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewLora.Name = "buttonNewLora";
            this.buttonNewLora.Size = new System.Drawing.Size(77, 37);
            this.buttonNewLora.TabIndex = 20;
            this.buttonNewLora.Text = "New Lora";
            this.buttonNewLora.UseVisualStyleBackColor = true;
            this.buttonNewLora.Click += new System.EventHandler(this.buttonNewLora_Click);
            // 
            // buttonNewLoraPart
            // 
            this.buttonNewLoraPart.Location = new System.Drawing.Point(1358, 451);
            this.buttonNewLoraPart.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewLoraPart.Name = "buttonNewLoraPart";
            this.buttonNewLoraPart.Size = new System.Drawing.Size(77, 37);
            this.buttonNewLoraPart.TabIndex = 21;
            this.buttonNewLoraPart.Text = "New Prompt Part for Lora";
            this.buttonNewLoraPart.UseVisualStyleBackColor = true;
            this.buttonNewLoraPart.Click += new System.EventHandler(this.buttonNewLoraPart_Click);
            // 
            // trackBarSteps
            // 
            this.trackBarSteps.LargeChange = 20;
            this.trackBarSteps.Location = new System.Drawing.Point(595, 238);
            this.trackBarSteps.Margin = new System.Windows.Forms.Padding(2);
            this.trackBarSteps.Maximum = 200;
            this.trackBarSteps.Name = "trackBarSteps";
            this.trackBarSteps.Size = new System.Drawing.Size(176, 45);
            this.trackBarSteps.SmallChange = 5;
            this.trackBarSteps.TabIndex = 23;
            this.trackBarSteps.TabStop = false;
            this.trackBarSteps.TickFrequency = 5;
            this.trackBarSteps.Value = 25;
            this.trackBarSteps.Scroll += new System.EventHandler(this.trackBarSteps_Scroll);
            // 
            // buttonAddFromClipBoard
            // 
            this.buttonAddFromClipBoard.Location = new System.Drawing.Point(8, 691);
            this.buttonAddFromClipBoard.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddFromClipBoard.Name = "buttonAddFromClipBoard";
            this.buttonAddFromClipBoard.Size = new System.Drawing.Size(76, 50);
            this.buttonAddFromClipBoard.TabIndex = 24;
            this.buttonAddFromClipBoard.Text = "Add Prompts From ClipBoard";
            this.buttonAddFromClipBoard.UseVisualStyleBackColor = true;
            this.buttonAddFromClipBoard.Click += new System.EventHandler(this.buttonAddFromClipBoard_Click);
            // 
            // comboBoxSamplingMethod
            // 
            this.comboBoxSamplingMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSamplingMethod.FormattingEnabled = true;
            this.comboBoxSamplingMethod.Location = new System.Drawing.Point(594, 206);
            this.comboBoxSamplingMethod.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxSamplingMethod.Name = "comboBoxSamplingMethod";
            this.comboBoxSamplingMethod.Size = new System.Drawing.Size(92, 28);
            this.comboBoxSamplingMethod.TabIndex = 25;
            // 
            // numericUpDownClipSkip
            // 
            this.numericUpDownClipSkip.Location = new System.Drawing.Point(623, 481);
            this.numericUpDownClipSkip.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownClipSkip.Name = "numericUpDownClipSkip";
            this.numericUpDownClipSkip.Size = new System.Drawing.Size(90, 20);
            this.numericUpDownClipSkip.TabIndex = 26;
            this.numericUpDownClipSkip.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownClipSkip.ValueChanged += new System.EventHandler(this.numericUpDownClipSkip_ValueChanged);
            // 
            // checkBoxClipSkip
            // 
            this.checkBoxClipSkip.AutoSize = true;
            this.checkBoxClipSkip.Location = new System.Drawing.Point(623, 460);
            this.checkBoxClipSkip.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxClipSkip.Name = "checkBoxClipSkip";
            this.checkBoxClipSkip.Size = new System.Drawing.Size(73, 17);
            this.checkBoxClipSkip.TabIndex = 27;
            this.checkBoxClipSkip.Text = "Clip Skip?";
            this.checkBoxClipSkip.UseVisualStyleBackColor = true;
            // 
            // labelCurretlySelected
            // 
            this.labelCurretlySelected.AutoSize = true;
            this.labelCurretlySelected.Location = new System.Drawing.Point(667, 62);
            this.labelCurretlySelected.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCurretlySelected.Name = "labelCurretlySelected";
            this.labelCurretlySelected.Size = new System.Drawing.Size(107, 13);
            this.labelCurretlySelected.TabIndex = 4;
            this.labelCurretlySelected.Text = "Selected Prompt Part";
            // 
            // textBoxSelectedPromptPart
            // 
            this.textBoxSelectedPromptPart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSelectedPromptPart.Location = new System.Drawing.Point(594, 68);
            this.textBoxSelectedPromptPart.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSelectedPromptPart.Name = "textBoxSelectedPromptPart";
            this.textBoxSelectedPromptPart.Size = new System.Drawing.Size(175, 26);
            this.textBoxSelectedPromptPart.TabIndex = 3;
            this.textBoxSelectedPromptPart.TextChanged += new System.EventHandler(this.textBoxSelectedPromptPart_TextChanged);
            this.textBoxSelectedPromptPart.Leave += new System.EventHandler(this.textBoxSelectedPromptPart_Leave);
            // 
            // buttonAddCustom
            // 
            this.buttonAddCustom.Location = new System.Drawing.Point(594, 98);
            this.buttonAddCustom.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddCustom.Name = "buttonAddCustom";
            this.buttonAddCustom.Size = new System.Drawing.Size(77, 37);
            this.buttonAddCustom.TabIndex = 28;
            this.buttonAddCustom.Text = "Add Custom";
            this.buttonAddCustom.UseVisualStyleBackColor = true;
            this.buttonAddCustom.Click += new System.EventHandler(this.buttonAddCustom_Click);
            // 
            // buttonAddListOfPromptPartsFromClipBoard
            // 
            this.buttonAddListOfPromptPartsFromClipBoard.Location = new System.Drawing.Point(1439, 452);
            this.buttonAddListOfPromptPartsFromClipBoard.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddListOfPromptPartsFromClipBoard.Name = "buttonAddListOfPromptPartsFromClipBoard";
            this.buttonAddListOfPromptPartsFromClipBoard.Size = new System.Drawing.Size(77, 37);
            this.buttonAddListOfPromptPartsFromClipBoard.TabIndex = 29;
            this.buttonAddListOfPromptPartsFromClipBoard.Text = "Add List Of Prompt Parts From ClipBoard";
            this.buttonAddListOfPromptPartsFromClipBoard.UseVisualStyleBackColor = true;
            this.buttonAddListOfPromptPartsFromClipBoard.Click += new System.EventHandler(this.buttonAddListOfPromptPartsFromClipBoard_Click);
            // 
            // listBoxLoraCategories
            // 
            this.listBoxLoraCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxLoraCategories.FormattingEnabled = true;
            this.listBoxLoraCategories.ItemHeight = 20;
            this.listBoxLoraCategories.Location = new System.Drawing.Point(868, 492);
            this.listBoxLoraCategories.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxLoraCategories.Name = "listBoxLoraCategories";
            this.listBoxLoraCategories.Size = new System.Drawing.Size(241, 364);
            this.listBoxLoraCategories.TabIndex = 31;
            this.listBoxLoraCategories.Click += new System.EventHandler(this.listBoxLoraCategories_Click);
            // 
            // buttonNewLoraCategory
            // 
            this.buttonNewLoraCategory.Location = new System.Drawing.Point(868, 452);
            this.buttonNewLoraCategory.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewLoraCategory.Name = "buttonNewLoraCategory";
            this.buttonNewLoraCategory.Size = new System.Drawing.Size(77, 37);
            this.buttonNewLoraCategory.TabIndex = 30;
            this.buttonNewLoraCategory.Text = "New Lora Category";
            this.buttonNewLoraCategory.UseVisualStyleBackColor = true;
            this.buttonNewLoraCategory.Click += new System.EventHandler(this.buttonNewLoraCategory_Click);
            // 
            // noScrollListBoxPromptParts
            // 
            this.noScrollListBoxPromptParts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noScrollListBoxPromptParts.FormattingEnabled = true;
            this.noScrollListBoxPromptParts.ItemHeight = 20;
            this.noScrollListBoxPromptParts.Location = new System.Drawing.Point(8, 8);
            this.noScrollListBoxPromptParts.Margin = new System.Windows.Forms.Padding(2);
            this.noScrollListBoxPromptParts.Name = "noScrollListBoxPromptParts";
            this.noScrollListBoxPromptParts.Size = new System.Drawing.Size(478, 684);
            this.noScrollListBoxPromptParts.TabIndex = 9;
            this.noScrollListBoxPromptParts.SelectedIndexChanged += new System.EventHandler(this.noScrollListBoxPromptParts_SelectedIndexChanged);
            this.noScrollListBoxPromptParts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.noScrollListBoxPromptParts_MouseDoubleClick);
            // 
            // textBoxSteps
            // 
            this.textBoxSteps.Location = new System.Drawing.Point(775, 245);
            this.textBoxSteps.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSteps.Name = "textBoxSteps";
            this.textBoxSteps.ReadOnly = true;
            this.textBoxSteps.Size = new System.Drawing.Size(48, 20);
            this.textBoxSteps.TabIndex = 5;
            this.textBoxSteps.Text = "25";
            // 
            // textBoxCFG
            // 
            this.textBoxCFG.Location = new System.Drawing.Point(778, 344);
            this.textBoxCFG.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCFG.Name = "textBoxCFG";
            this.textBoxCFG.ReadOnly = true;
            this.textBoxCFG.Size = new System.Drawing.Size(48, 20);
            this.textBoxCFG.TabIndex = 32;
            this.textBoxCFG.Text = "7";
            // 
            // trackBarCFG
            // 
            this.trackBarCFG.LargeChange = 20;
            this.trackBarCFG.Location = new System.Drawing.Point(598, 337);
            this.trackBarCFG.Margin = new System.Windows.Forms.Padding(2);
            this.trackBarCFG.Maximum = 20;
            this.trackBarCFG.Name = "trackBarCFG";
            this.trackBarCFG.Size = new System.Drawing.Size(176, 45);
            this.trackBarCFG.TabIndex = 33;
            this.trackBarCFG.TabStop = false;
            this.trackBarCFG.TickFrequency = 5;
            this.trackBarCFG.Value = 7;
            this.trackBarCFG.Scroll += new System.EventHandler(this.trackBarCFG_Scroll);
            // 
            // checkHighRes
            // 
            this.checkHighRes.AutoSize = true;
            this.checkHighRes.Location = new System.Drawing.Point(675, 23);
            this.checkHighRes.Margin = new System.Windows.Forms.Padding(2);
            this.checkHighRes.Name = "checkHighRes";
            this.checkHighRes.Size = new System.Drawing.Size(70, 17);
            this.checkHighRes.TabIndex = 34;
            this.checkHighRes.Text = "High Res";
            this.checkHighRes.UseVisualStyleBackColor = true;
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2078, 954);
            this.Controls.Add(this.checkHighRes);
            this.Controls.Add(this.textBoxCFG);
            this.Controls.Add(this.trackBarCFG);
            this.Controls.Add(this.listBoxLoraCategories);
            this.Controls.Add(this.buttonNewLoraCategory);
            this.Controls.Add(this.buttonAddListOfPromptPartsFromClipBoard);
            this.Controls.Add(this.buttonAddCustom);
            this.Controls.Add(this.labelCurretlySelected);
            this.Controls.Add(this.textBoxSelectedPromptPart);
            this.Controls.Add(this.checkBoxClipSkip);
            this.Controls.Add(this.numericUpDownClipSkip);
            this.Controls.Add(this.comboBoxSamplingMethod);
            this.Controls.Add(this.buttonAddFromClipBoard);
            this.Controls.Add(this.textBoxSteps);
            this.Controls.Add(this.trackBarSteps);
            this.Controls.Add(this.buttonNewLoraPart);
            this.Controls.Add(this.buttonNewLora);
            this.Controls.Add(this.listBoxLoraParts);
            this.Controls.Add(this.listBoxLoras);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonNewPromptPartForCategory);
            this.Controls.Add(this.listBoxPromptsFromCatergory);
            this.Controls.Add(this.listBoxCategory);
            this.Controls.Add(this.buttonNewCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNegativePrompt);
            this.Controls.Add(this.noScrollListBoxPromptParts);
            this.Controls.Add(this.checkBoxIgnorrePromptParts);
            this.Controls.Add(this.textBoxPrompt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.tabControlDetails);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "InputForm";
            this.Text = "InputForm";
            this.Load += new System.EventHandler(this.InputForm_Load);
            this.tabControlDetails.ResumeLayout(false);
            this.tabPageCategory.ResumeLayout(false);
            this.tabPageCategory.PerformLayout();
            this.tabPagePromptPart.ResumeLayout(false);
            this.tabPagePromptPart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNumberOfParantheses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPromptPartWeight)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabLoras.ResumeLayout(false);
            this.tabLoras.PerformLayout();
            this.tabPageLoraParts.ResumeLayout(false);
            this.tabPageLoraParts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLoraPartNumberOfParantheses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLoraPartWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClipSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCFG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxPrompt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.CheckBox checkBoxIgnorrePromptParts;
        private NoScrollListBox noScrollListBoxPromptParts;
        private System.Windows.Forms.TextBox textBoxNegativePrompt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonNewCategory;
        private System.Windows.Forms.ListBox listBoxCategory;
        private System.Windows.Forms.TabControl tabControlDetails;
        private System.Windows.Forms.TabPage tabPageCategory;
        private System.Windows.Forms.ListBox listBoxPromptsFromCatergory;
        private System.Windows.Forms.Button buttonNewPromptPartForCategory;
        private System.Windows.Forms.TextBox textBoxPromptPartCategoryName;
        private System.Windows.Forms.Label labelCatergoryName;
        private System.Windows.Forms.TabPage tabPagePromptPart;
        private System.Windows.Forms.TextBox textBoxPromptPartName;
        private System.Windows.Forms.TabPage tabLoras;
        private System.Windows.Forms.TrackBar trackBarPromptPartWeight;
        private System.Windows.Forms.TextBox textBoxPromptPartWeight;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonDeleteCategory;
        private System.Windows.Forms.TrackBar trackBarNumberOfParantheses;
        private System.Windows.Forms.ListBox listBoxLoras;
        private System.Windows.Forms.ListBox listBoxLoraParts;
        private System.Windows.Forms.Button buttonNewLora;
        private System.Windows.Forms.Button buttonNewLoraPart;
        private System.Windows.Forms.TabPage tabPageLoraParts;
        private System.Windows.Forms.Button buttonDeleteLora;
        private System.Windows.Forms.Label labelLoraName;
        private System.Windows.Forms.TextBox textBoxLoraName;
        private System.Windows.Forms.TextBox textBoxLoraPartWeight;
        private System.Windows.Forms.TrackBar trackBarLoraPartWeight;
        private System.Windows.Forms.TextBox textBoxLoraPartText;
        private System.Windows.Forms.CheckBox checkBoxIsLora;
        private System.Windows.Forms.TrackBar trackBarLoraPartNumberOfParantheses;
        private System.Windows.Forms.Button buttonDeleteLoraPart;
        private System.Windows.Forms.TrackBar trackBarSteps;
        private System.Windows.Forms.Button buttonAddFromClipBoard;
        private System.Windows.Forms.ComboBox comboBoxSamplingMethod;
        private System.Windows.Forms.NumericUpDown numericUpDownClipSkip;
        private System.Windows.Forms.CheckBox checkBoxClipSkip;
        private System.Windows.Forms.Label labelCurretlySelected;
        private System.Windows.Forms.TextBox textBoxSelectedPromptPart;
        private System.Windows.Forms.Button buttonAddCustom;
        private System.Windows.Forms.Button buttonAddListOfPromptPartsFromClipBoard;
        private System.Windows.Forms.ListBox listBoxLoraCategories;
        private System.Windows.Forms.Button buttonNewLoraCategory;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelLoraCategoryName;
        private System.Windows.Forms.TextBox textBoxLoraCategoryName;
        private System.Windows.Forms.ComboBox comboBoxLoraCategoriestoMoveTo;
        private System.Windows.Forms.Button buttonMoveLoraToOtherCategory;
        private System.Windows.Forms.TextBox textBoxSteps;
        private System.Windows.Forms.TextBox textBoxCFG;
        private System.Windows.Forms.TrackBar trackBarCFG;
        private System.Windows.Forms.CheckBox checkHighRes;
    }
}