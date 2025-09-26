using System.Collections.Generic;

namespace StabSharp
{
    public class Lora
    {
        public string LoraName;
        public List<PromptPart> Parts;

        override public string ToString()
        {
            return LoraName;
        }
    }
}
