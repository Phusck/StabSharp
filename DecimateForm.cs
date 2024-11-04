using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
            textBox1.Text = text;
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


        private int ReadInt32(BinaryReader br)
        {
            byte[] bytes = br.ReadBytes(4);
            Array.Reverse(bytes); // PNG uses big-endian
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
