﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace StabSharp
{
    internal class StableDiffusionAPI
    {
        string myPersistentDataPath = "C:/TempTemp";


        public async Task<SDImage> RequestTxtToImg(string prompt, string negativePrompt, bool doHires, int seed)
        {
            return await MakeRequest(prompt, negativePrompt, doHires, seed);
        }


        private async Task<SDImage> MakeRequest(string prompt, string negativePrompt, bool doHires, int seed)
        {

            Random random = new Random();


            Dictionary<string,string> requestData = new Dictionary<string,string>();
            requestData.Add("prompt", prompt);
            requestData.Add("negative_prompt", negativePrompt);
            if(seed == -1)
            {
                requestData.Add("seed", random.Next().ToString());
            }
            else
            {
                requestData.Add("seed", seed.ToString());
            }
            if (doHires)
            {
                requestData.Add("enable_hr", "true");
                requestData.Add("denoising_strength", "0.7");
                requestData.Add("hr_scale", "1.5");
                requestData.Add("hr_upscaler", "Latent");
            }

            string json = OleJsonTool(requestData);

            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));

                var response = await client.PostAsync("http://127.0.0.1:7860/sdapi/v1/txt2img", content);
                if (response.IsSuccessStatusCode)
                {
                    string imageData = await response.Content.ReadAsStringAsync();
                    ImageData myImageData = JsonConvert.DeserializeObject<ImageData>(imageData);

                    string newImageFileNumber = GetNextImageNumberForFileName(true);

                    string newImageFileName = "image_" + newImageFileNumber + ".png";
                    string fullPath = Path.Combine(myPersistentDataPath, newImageFileName);
                    File.WriteAllBytes(fullPath, Convert.FromBase64String(myImageData.images[0]));
                    return new SDImage(fullPath, myImageData.parameters);
                }
                else
                {
                    // Consider throwing an exception or returning null to indicate failure.
                    return null;
                }
            }
        }




        string GetNextImageNumberForFileName(bool isNewImageFile)
        {
            List<int> imageNumbers = new List<int>();


            int maxImageNumber = 0;

            string[] files = Directory.GetFiles(myPersistentDataPath);

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                string justFileName = fi.Name;
                string extn = fi.Extension;

                if (extn == ".png")
                {
                    string fileNumberString = justFileName.Substring(6, 3);
                    int fileNumber = int.Parse(fileNumberString);
                    imageNumbers.Add(fileNumber);
                }
            }


            string strMaxImageNumber;

            if (imageNumbers.Count > 0)
            {
                maxImageNumber = imageNumbers.Max();
                if (isNewImageFile)//if this method is being called to return a number for a new image file then we have to take the current max and add 1
                {
                    maxImageNumber++;
                }

                strMaxImageNumber = (maxImageNumber).ToString();
                if (maxImageNumber < 10)
                {
                    strMaxImageNumber = "0" + strMaxImageNumber;
                }
                if (maxImageNumber < 100)
                {
                    strMaxImageNumber = "0" + strMaxImageNumber;
                }

                //Debug.Log("strMaxImageNumber " + strMaxImageNumber);
            }
            else
            {
                strMaxImageNumber = "001";
            }


            return strMaxImageNumber;
        }

        private string OleJsonTool(Dictionary<string, string> dict)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (KeyValuePair<string, string> entry in dict)
            {
                sb.Append($"\"{entry.Key}\":\"{entry.Value}\",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();

        }

    }


    public class Parameters
    {
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
    }

    public class ImageData
    {
        public List<string> images { get; set; }
        public Parameters parameters { get; set; }
        public string info { get; set; }
    }

    public class SDImage
    {
        public string ImagePath;
        public Parameters Parameters;

        public SDImage(string imagePath, Parameters parameters)
        {
            ImagePath = imagePath;
            Parameters = parameters;
        }
    }

}