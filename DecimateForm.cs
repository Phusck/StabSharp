using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace StabSharp
{
    public partial class DecimateForm : Form
    {
        public DecimateForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0)
            {
                return;
            }
            string file = files[0];
            Image image = Image.FromFile(file);
            pictureBox1.Image = image;

            string text = ExtractTextFromPng(file);
            textBoxRaw.Text = text;
            textBoxFormatted.Text = DivideTextToThings(text).ToString();
        }

        private void DecimateForm_Load(object sender, EventArgs e)
        {
            pictureBox1.AllowDrop = true;
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private string ExtractTextFromPng(string filePath)
        {
            StringBuilder allText = new StringBuilder();

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    fs.Seek(8, SeekOrigin.Begin); // Skip the PNG signature
                    while (fs.Position < fs.Length)
                    {
                        int length = ReadInt32(br);
                        string chunkType = Encoding.ASCII.GetString(br.ReadBytes(4));

                        if (chunkType == "tEXt")
                        {
                            byte[] chunkData = br.ReadBytes(length);
                            string chunkText = Encoding.ASCII.GetString(chunkData);
                            string[] parts = chunkText.Split('\0');
                            foreach (var part in parts)
                            {
                                allText.AppendLine(part);
                            }
                        }
                        else
                        {
                            fs.Seek(length, SeekOrigin.Current); // Skip the chunk data
                        }

                        fs.Seek(4, SeekOrigin.Current); // Skip the CRC
                    }
                }
            }

            return allText.ToString();
        }

        private Prompt DivideTextToThings(string inputText)
        {
            inputText = inputText.Replace("\r\n", "");
            inputText = inputText.Replace("\n\n", "");

            // Ensure the keywords are generated.
            CodeGeneration.PromptKeywordGenerator.GenerateKeywords();

            Prompt prompt = new Prompt();

            // Build markers dictionary using the KeywordInfos plus the negative prompt.
            var markers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "NegativePrompt", "Negative prompt:" }
    };

            foreach (var info in CodeGeneration.PromptKeywordGenerator.KeywordInfos)
            {
                markers[info.FieldName] = info.Marker;
            }

            // Find occurrences of each marker in the input.
            var occurrences = new List<(string Field, int Index, int MarkerLength)>();
            foreach (var kvp in markers)
            {
                int index = inputText.IndexOf(kvp.Value, StringComparison.OrdinalIgnoreCase);
                if (index >= 0)
                {
                    occurrences.Add((kvp.Key, index, kvp.Value.Length));
                }
            }
            // Sort markers by their position in the text.
            occurrences.Sort((a, b) => a.Index.CompareTo(b.Index));

            // The text before the first marker is the main prompt.
            string mainPromptText = occurrences.Count > 0 ? inputText.Substring(0, occurrences[0].Index).Trim() : inputText.Trim();
            prompt.PromptParts = mainPromptText
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToArray();

            // Extract keyword values based on marker positions.
            for (int i = 0; i < occurrences.Count; i++)
            {
                var current = occurrences[i];
                int start = current.Index + current.MarkerLength;
                int nextIndex = (i + 1 < occurrences.Count) ? occurrences[i + 1].Index : inputText.Length;
                int length = nextIndex - start;

                // Ensure the computed length isn't negative.
                length = Math.Max(0, length);

                string extractedText = inputText.Substring(start, length).Trim();

                if (current.Field.Equals("NegativePrompt", StringComparison.OrdinalIgnoreCase))
                {
                    prompt.NegativePromptParts = extractedText
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .ToArray();
                }
                else
                {
                    extractedText = CleanExtractedText(extractedText);
                    FieldInfo field = typeof(Prompt).GetField(current.Field);
                    if (field != null && field.FieldType == typeof(string))
                    {
                        // Because Prompt is a struct (value type), box then unbox after setting.
                        object boxedPrompt = prompt;
                        field.SetValue(boxedPrompt, extractedText);
                        prompt = (Prompt)boxedPrompt;
                    }
                }
            }
            return prompt;
        }

        private static string CleanExtractedText(string text)
        {
            // Split on comma. If there is only one value, remove any trailing comma.
            var parts = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
            {
                return parts[0].Trim();
            }
            return text.Trim();
        }

        private int ReadInt32(BinaryReader br)
        {
            byte[] bytes = br.ReadBytes(4);
            Array.Reverse(bytes); // PNG uses big-endian
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
