using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StabSharp
{
    internal class PromptPartCategory
    {
        public string Name { get; set; } = string.Empty;

        // WinForms-friendly collection for data binding
        public BindingList<PromptPart> PromptParts { get; set; } = new BindingList<PromptPart>();

        public PromptPartCategory()
        {
            // for JSON
        }

        public PromptPartCategory(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }

}
