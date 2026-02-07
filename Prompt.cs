using System.Linq;
using System.Reflection;
using System.Text;

namespace StabSharp
{
    public struct Prompt
    {
        public string[] PromptParts;
        public string[] NegativePromptParts;

        [PromptKeyword("Steps:")]
        public string Steps;

        [PromptKeyword("Sampler:")]
        public string Sampler;

        [PromptKeyword("CFG scale:")]
        public string CFGScale;

        [PromptKeyword("Seed:")]
        public string Seed;

        [PromptKeyword("Size:")]
        public string Size;

        [PromptKeyword("Model hash:")]
        public string ModelHash;

        [PromptKeyword("Model:")]
        public string Model;

        [PromptKeyword("Denoising strength:")]
        public string DenoisingStrength;

        [PromptKeyword("Clip skip:")]
        public string ClipSkip;

        [PromptKeyword("ENSD:")]
        public string ENSD;

        [PromptKeyword("Hires upscale:")]
        public string HiresUpscale;

        [PromptKeyword("Hires steps:")]
        public string HiresSteps;

        [PromptKeyword("Hires upscaler:")]
        public string HiresUpscaler;

        [PromptKeyword("Lora hashes:")]
        public string LoraHashes;

        [PromptKeyword("TI hashes:")]
        public string TIHashes;

        [PromptKeyword("Version:")]
        public string Version;

        [PromptKeyword("Hashes:")]
        public string Hashes;

        [PromptKeyword("Enable_HR:")]
        public string EnableHR;

        [PromptKeyword("hr_scale:")]
        public string HiresScale;

        [PromptKeyword("hr_upscaler")]
        public string HiresUpscalerName;


        /// <summary>
        /// Checks if high resolution is enabled for this prompt.
        /// </summary>
        /// <returns>True if high resolution is enabled, false otherwise.</returns>
        private bool IsHighResEnabled()
        {
            if (IsTrueLike(EnableHR))
            {
                return true;
            }

            return !string.IsNullOrWhiteSpace(HiresUpscaler) ||
                   !string.IsNullOrWhiteSpace(HiresUpscalerName) ||
                   !string.IsNullOrWhiteSpace(HiresUpscale) ||
                   !string.IsNullOrWhiteSpace(HiresScale) ||
                   !string.IsNullOrWhiteSpace(HiresSteps);
        }

        /// <summary>
        /// Checks if a string value is "true-like" (case-insensitive comparison).
        /// </summary>
        private static bool IsTrueLike(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            string v = value.Trim();
            return v.Equals("true", System.StringComparison.OrdinalIgnoreCase) ||
                   v.Equals("1", System.StringComparison.OrdinalIgnoreCase) ||
                   v.Equals("yes", System.StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the first prompt part that is not a LoRA, with high resolution status if applicable.
        /// </summary>
        /// <returns>The text of the first non-LoRA prompt part with HR status, or an empty string if none found.</returns>
        public string GetFirstNonLoraPromptPart()
        {
            string promptPart = string.Empty;

            if (PromptParts != null && PromptParts.Length > 0)
            {
                foreach (var part in PromptParts)
                {
                    if (string.IsNullOrWhiteSpace(part))
                    {
                        continue;
                    }

                    string trimmedPart = part.Trim();
                    // LoRA parts start with "<lora:"
                    if (!trimmedPart.StartsWith("<lora:", System.StringComparison.OrdinalIgnoreCase))
                    {
                        promptPart = trimmedPart;
                        break;
                    }
                }
            }

            // Append high resolution status if enabled
            if (!string.IsNullOrEmpty(promptPart) && IsHighResEnabled())
            {
                promptPart += " (High Res)";
            }

            return promptPart;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            // Output the main prompt parts if available.
            if (PromptParts != null && PromptParts.Length > 0)
            {
                string promptText = string.Join(", ", PromptParts.Select(p => p.ToString().Trim()).Where(s => !string.IsNullOrEmpty(s)));
                if (!string.IsNullOrEmpty(promptText))
                {
                    sb.AppendLine("Prompt: " + promptText);
                }
            }

            // Output the negative prompt parts.
            if (NegativePromptParts != null && NegativePromptParts.Length > 0)
            {
                string negPromptText = string.Join(", ", NegativePromptParts.Select(p => p.ToString().Trim()).Where(s => !string.IsNullOrEmpty(s)));
                if (!string.IsNullOrEmpty(negPromptText))
                {
                    sb.AppendLine("Negative prompt: " + negPromptText);
                }
            }

            // Reflect over all other public instance fields.
            var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(f => f.Name != "PromptParts" && f.Name != "NegativePromptParts");

            foreach (var field in fields)
            {
                // Assume string type – new fields should be added as string.
                string value = field.GetValue(this) as string;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    // Retrieve the marker text from our attribute.
                    var attr = field.GetCustomAttribute<PromptKeywordAttribute>();
                    string marker = attr != null && !string.IsNullOrEmpty(attr.Marker)
                        ? attr.Marker
                        : ConvertKeywordToMarker(field.Name);
                    sb.AppendLine($"{marker} {value}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Converts a field name to a default marker text by inserting spaces before uppercase letters and appending a colon.
        /// </summary>
        private static string ConvertKeywordToMarker(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return keyword;
            }

            var newText = keyword[0].ToString();
            for (int i = 1; i < keyword.Length; i++)
            {
                if (char.IsUpper(keyword[i]) && !char.IsWhiteSpace(keyword[i - 1]))
                {
                    newText += " ";
                }
                newText += keyword[i];
            }
            return newText + ":";
        }

        /*
        public class Parameters

        public bool enable_hr { get; set; }
        public double? denoising_strength { get; set; }
        public int? firstphase_width { get; set; }
        public int? firstphase_height { get; set; }
        public string prompt { get; set; }
        public object styles { get; set; }
        public int Seed { get; set; }
        public int? subseed { get; set; }
        public int? subseed_strength { get; set; }
        public int? seed_resize_from_h { get; set; }
        public int? seed_resize_from_w { get; set; }
        public object sampler_name { get; set; }
        public int? batch_size { get; set; }
        public int? n_iter { get; set; }
        public int? steps { get; set; }
        public double? cfg_scale { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public bool? restore_faces { get; set; }
        public bool? tiling { get; set; }
        public string negative_prompt { get; set; }
        public object eta { get; set; }
        public double? s_churn { get; set; }
        public object s_tmax { get; set; }
        public double? s_tmin { get; set; }
        public double? s_noise { get; set; }
        public object override_settings { get; set; }
        public string sampler_index { get; set; }
        public int clip_skip { get; set; }
    */
    }
}
