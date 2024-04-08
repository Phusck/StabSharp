using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StabSharp
{
    internal class PromptPartCategory
    {
        private string name;
        private List<PromptPart> promptParts = new List<PromptPart>();

        public List<PromptPart> PromptParts { get { return promptParts; } }

        public string Name
        {
            get
            { 
                return name;
            }
            set
            {
                name = value;
            }
        }

        public PromptPartCategory(string name)
        {
            this.name = name;
        }
        public override string ToString()
        {
            return name;
        }
    }

}
