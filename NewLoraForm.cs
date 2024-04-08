using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StabSharp
{
    public partial class NewLoraForm : Form
    {
        public NewLoraForm()
        {
            InitializeComponent();
        }

        internal Lora GetLora()
        {
            Lora lora = new Lora();
            lora.LoraName = textBoxManualInput.Text;
            lora.Parts = new List<PromptPart>();
            return lora;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //close form with dialog result OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
