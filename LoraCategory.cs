using System.Collections.Generic;


namespace StabSharp
{
    public class LoraCategory
    {
        public string LoraCategoryName;
        public List<Lora> Loras = new List<Lora>();

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
