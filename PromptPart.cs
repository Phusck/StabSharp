using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace StabSharp
{
    [Serializable]
    public class PromptPart : ICloneable, INotifyPropertyChanged
    {
        private string _text = string.Empty;
        private float _weight = 1f;
        private int _quantityOfParantheses = 0;
        private bool _isLora = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayText => ToString();

        // For JSON deserialization
        public PromptPart() { }

        public PromptPart(string text)
        {
            _text = text;
            _weight = 1f;
            _quantityOfParantheses = 0;
            _isLora = false;
        }

        public PromptPart(string text, float weight, int quantityOfCurlyBrackets, bool isLora)
        {
            _text = text;
            _weight = weight;
            _quantityOfParantheses = quantityOfCurlyBrackets;
            _isLora = isLora;
        }

        public string Text
        {
            get => _text;
            set
            {
                if (string.Equals(_text, value, StringComparison.Ordinal))
                {
                    return;
                }
                _text = value;
                OnPropertyChanged(nameof(Text));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public float Weight
        {
            get => _weight;
            set
            {
                if (Math.Abs(_weight - value) < 0.00001f)
                {
                    return;
                }
                _weight = value;
                OnPropertyChanged(nameof(Weight));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public int QuantityOfParantheses
        {
            get => _quantityOfParantheses;
            set
            {
                if (_quantityOfParantheses == value)
                {
                    return;
                }
                _quantityOfParantheses = value;
                OnPropertyChanged(nameof(QuantityOfParantheses));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public bool IsLora
        {
            get => _isLora;
            set
            {
                if (_isLora == value)
                {
                    return;
                }
                _isLora = value;
                OnPropertyChanged(nameof(IsLora));
                OnPropertyChanged(nameof(DisplayText));
            }
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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Parses a prompt-part string (as produced by <see cref="ToString"/>) back into a structured <see cref="PromptPart"/>.
        /// Best-effort: on malformed input, returns false and sets <paramref name="part"/> to a default <see cref="PromptPart"/> with Text set.
        /// </summary>
        public static bool TryParseFromPromptString(string s, out PromptPart part)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                part = new PromptPart(string.Empty);
                return false;
            }

            string t = s.Trim();

            // LoRA form: <lora:Name: 0.70>
            if (t.StartsWith("<lora:", StringComparison.OrdinalIgnoreCase) && t.EndsWith(">", StringComparison.Ordinal))
            {
                string inner = t.Substring("<lora:".Length, t.Length - "<lora:".Length - 1).Trim();
                int lastColon = inner.LastIndexOf(':');

                if (lastColon < 0)
                {
                    part = new PromptPart(inner, 1f, 0, true);
                    return true;
                }

                string namePart = inner.Substring(0, lastColon).Trim();
                string weightPart = inner.Substring(lastColon + 1).Trim();

                if (float.TryParse(weightPart, NumberStyles.Float, CultureInfo.InvariantCulture, out float weight))
                {
                    part = new PromptPart(namePart, weight, 0, true);
                    return true;
                }

                part = new PromptPart(namePart, 1f, 0, true);
                return true;
            }

            // Non-LoRA: optional outer parentheses for emphasis, plus optional (Text: weight) wrapper.
            int outerParens = 0;
            while (t.Length >= 2 && t[0] == '(' && t[t.Length - 1] == ')')
            {
                outerParens++;
                t = t.Substring(1, t.Length - 2).Trim();
            }

            string innerText = t.Trim();
            float parsedWeight2 = 1f;
            bool hasParsedWeight = false;

            // Weight in our formatting is always wrapped in at least one pair of parentheses.
            // This reduces false positives for plain text like "foo: 1.2".
            if (outerParens > 0)
            {
                int lastColon = innerText.LastIndexOf(':');
                if (lastColon > 0)
                {
                    string maybeWeight = innerText.Substring(lastColon + 1).Trim();
                    if (float.TryParse(maybeWeight, NumberStyles.Float, CultureInfo.InvariantCulture, out parsedWeight2))
                    {
                        string maybeText = innerText.Substring(0, lastColon).Trim();
                        if (!string.IsNullOrEmpty(maybeText))
                        {
                            innerText = maybeText;
                            hasParsedWeight = true;
                        }
                    }
                }
            }

            int qtyParens = outerParens - (hasParsedWeight ? 1 : 0);
            if (qtyParens < 0)
            {
                qtyParens = 0;
            }

            part = new PromptPart(innerText, parsedWeight2, qtyParens, false);
            return true;
        }

        public static PromptPart ParseFromPromptStringOrDefault(string s)
        {
            return TryParseFromPromptString(s, out PromptPart part) ? part : new PromptPart(s?.Trim() ?? string.Empty);
        }

    }
}