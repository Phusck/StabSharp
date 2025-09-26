using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StabSharp.CodeGeneration
{
    public struct PromptKeywordInfo
    {
        public string FieldName
        {
            get; set;
        }
        public string Marker
        {
            get; set;
        }
    }

    public static class PromptKeywordGenerator
    {
        public static List<PromptKeywordInfo> KeywordInfos
        {
            get; private set;
        }

        public static void GenerateKeywords()
        {
            var keywordInfos = new List<PromptKeywordInfo>();

            var fields = typeof(Prompt).GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                // We ignore the prompt arrays.
                if (field.FieldType == typeof(string) && field.Name != "PromptParts" && field.Name != "NegativePromptParts")
                {
                    // Look for the attribute.
                    var attribute = field.GetCustomAttribute<PromptKeywordAttribute>();
                    string marker;
                    if (attribute != null && !string.IsNullOrEmpty(attribute.Marker))
                    {
                        marker = attribute.Marker;
                    }
                    else
                    {
                        marker = ConvertKeywordToMarker(field.Name);
                    }
                    keywordInfos.Add(new PromptKeywordInfo { FieldName = field.Name, Marker = marker });
                }
            }
            KeywordInfos = keywordInfos;
        }

        private static string ConvertKeywordToMarker(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                {
                return keyword;
            }

            // Default conversion: insert a space before each uppercase letter (except first) and add a colon.
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
    }
}
