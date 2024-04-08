using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StabSharp
{
    internal class Lora
    {
        public string LoraName;
        public List<PromptPart> Parts;


        override public string ToString()
        {
            return LoraName;
        }
    }
}
