using System;
using System.Globalization;
using System.Text;

namespace StabSharp
{
    [Serializable]
    public struct PromptPart : ICloneable
    {
        public string Text;
        public float Weight;
        public int QuantityOfParantheses;
        public bool IsLora;


        public PromptPart(string text)
        {
            Text = text;
            Weight = 1f;
            QuantityOfParantheses = 0;
            IsLora = false;
        }

        public PromptPart(string text, float weight, int quantityOfCurlyBrackets, bool isLora)
        {
            Text = text;
            Weight = weight;
            QuantityOfParantheses = quantityOfCurlyBrackets;
            IsLora = isLora;
        }

        public object Clone() 
        {
            return new PromptPart(Text, Weight, QuantityOfParantheses, IsLora);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            bool hasWeight = !(Math.Abs(Weight-1f) < 0.00005f);
            if (IsLora)
            {
                sb.Append("<lora:");
            }
            else
            {
                for (int i = 0; i < QuantityOfParantheses; i++)
                {
                    sb.Append("(");
                }
                if (hasWeight)
                {
                    sb.Append("(");
                }
                sb.Append(Text);
                if (hasWeight)
                {
                    sb.Append($": {Weight.ToString("0.00", CultureInfo.InvariantCulture)})");
                }
                for (int i = 0; i < QuantityOfParantheses; i++)
                {
                    sb.Append(")");
                }
            }
            if (IsLora)
            {
                sb.Append(Text);
                sb.Append($": {Weight.ToString("0.00", CultureInfo.InvariantCulture)}");
                sb.Append(">");
            }
            return sb.ToString();
        }

    }
}