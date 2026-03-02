using System.ComponentModel;
using System.Collections.Generic;

namespace StabSharp
{
    public class Lora
    {
        public string LoraName { get; set; } = string.Empty;

        // WinForms-friendly collection for data binding
        public BindingList<PromptPart> Parts { get; set; } = new BindingList<PromptPart>();

        public Lora() { }

        override public string ToString()
        {
            return LoraName;
        }
    }
}
