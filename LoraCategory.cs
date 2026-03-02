using System.ComponentModel;
using System.Collections.Generic;


namespace StabSharp
{
    public class LoraCategory
    {
        public string LoraCategoryName { get; set; } = string.Empty;

        // WinForms-friendly collection for data binding
        public BindingList<Lora> Loras { get; set; } = new BindingList<Lora>();

        public LoraCategory() { }

        public LoraCategory(string loraCategoryName)
        {
            LoraCategoryName = loraCategoryName;
        }

        override public string ToString()
        {
            return LoraCategoryName;
        }
    }
}
