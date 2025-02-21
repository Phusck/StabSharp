using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StabSharp
{
    static class ClipBoardHelper
    {
        public static string[] GetCommaSeperatedStrings()
        {
            string clipboardText = Clipboard.GetText();

            List<string> parts = clipboardText.Split(',').ToList();
            List<int> indicesToRemove = new List<int>();
            //Trim all
            for (int i = 0; i < parts.Count; i++)
            {
                parts[i] = parts[i].Trim();
            }
            //Remove all empty
            for (int i = 0; i < parts.Count; i++)
            {
                if (string.IsNullOrEmpty(parts[i]))
                {
                    indicesToRemove.Add(i);
                }
            }
            //Remove all empty, in reverse order
            for (int i = indicesToRemove.Count - 1; i >= 0; i--)
            {
                parts.RemoveAt(indicesToRemove[i]);
            }
            return parts.ToArray();
        }
    }
}
