using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Windows.Forms;


namespace StabSharp
{
    internal class StableDiffusionAPI
    {
        const string myPersistentDataPath = "C:/TempTemp";

        public async Task<SDImage> ImageRequest(Prompt prompt)
        {


            if (!IsServerRunning("127.0.0.1", 7860))
            {
                // message box
                MessageBox.Show("Stable Diffusion API is not running,                 -    We failed inside       -   Should not happen     -   *     *     .");
                return null;
            }

            string jsonReqeustString = PromptToJson(prompt);

            using (var client = new HttpClient())
            {
                var content = new StringContent(jsonReqeustString, Encoding.UTF8, "application/json");
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
                    return new SDImage(fullPath, prompt);
                }
                else
                {
                    // Consider throwing an exception or returning null to indicate failure.
                    return null;
                }
            }
        }

        public async Task<int> GetProgress()
        {
    
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://127.0.0.1:7860/sdapi/v1/progress?skip_current_image=false");
                if (response.IsSuccessStatusCode)
                {
                    string progressData = await response.Content.ReadAsStringAsync();
                    dynamic progressJson = JsonConvert.DeserializeObject(progressData);
                    double progress = progressJson.progress;
                    int progressPercentage = (int)(progress * 100);
                    return progressPercentage;
                }
                else
                {
                    // Consider throwing an exception or returning a default value to indicate failure.
                    return -1;
                }
            }
        }

        string GetNextImageNumberForFileName(bool isNewImageFile)
        {
            List<int> imageNumbers = new List<int>();


            int maxImageNumber;

            if (!Directory.Exists(myPersistentDataPath))
            {
                Directory.CreateDirectory(myPersistentDataPath);
            }
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

        public static string PromptToJson(Prompt prompt)
        {
            // build a payload dictionary from whatever parts of Prompt are set
            var payload = new Dictionary<string, object>
            {
                ["prompt"] = prompt.PromptParts != null
                    ? string.Join(", ", prompt.PromptParts)
                    : string.Empty,
                ["negative_prompt"] = prompt.NegativePromptParts != null
                    ? string.Join(", ", prompt.NegativePromptParts)
                    : string.Empty,
            };

            if (int.TryParse(prompt.Steps, out var steps))
            {
                payload["steps"] = steps;
            }

            if (!string.IsNullOrWhiteSpace(prompt.Sampler))
            {
                payload["sampler_name"] = prompt.Sampler;
            }

            if (double.TryParse(prompt.CFGScale, out var cfg))
            {
                payload["cfg_scale"] = cfg;
            }

            if (int.TryParse(prompt.Seed, out var seed))
            {
                payload["seed"] = seed;
            }

            // example of pulling in ClipSkip if present
            if (int.TryParse(prompt.ClipSkip, out var clipSkip))
            {
                payload["override_settings"] = new
                {
                    CLIP_stop_at_last_layers = clipSkip
                };
            }

            // you can mirror that pattern for any of the other Prompt fields
            // e.g. hires:
            if (double.TryParse(prompt.DenoisingStrength, out var ds))
            {
                payload["denoising_strength"] = ds;
            }

            if (!string.IsNullOrWhiteSpace(prompt.HiresUpscaler))
            {
                payload["enable_hr"] = true;
                if (double.TryParse(prompt.HiresUpscale, out var hrScale))
                {
                    payload["hr_scale"] = hrScale;
                }

                if (int.TryParse(prompt.HiresSteps, out var hrSteps))
                {
                    payload["hr_steps"] = hrSteps;
                }

                payload["hr_upscaler"] = prompt.HiresUpscaler;
            }


            //Serialize the object and add it to the users clipboard
            string json = JsonConvert.SerializeObject(payload, Formatting.Indented);
            Clipboard.SetText(json);
            
            return json;
        }

        public bool IsServerRunning(string server, int port)
        {
            try
            {
                using (var client = new TcpClient(server, port))
                {
                    return true;
                }
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public async Task<List<Checkpoint>> GetCheckpointsAsync()
        {
            if (!IsServerRunning("127.0.0.1", 7860))
            {
                return new List<Checkpoint>();
            }

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync("http://127.0.0.1:7860/sdapi/v1/sd-models");
                    if (response.IsSuccessStatusCode)
                    {
                        string checkpointsData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Checkpoints API Response: {checkpointsData}");
                        var checkpoints = JsonConvert.DeserializeObject<List<Checkpoint>>(checkpointsData);
                        if (checkpoints != null)
                        {
                            Console.WriteLine($"Deserialized {checkpoints.Count} checkpoints");
                        }
                        return checkpoints ?? new List<Checkpoint>();
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"API Error: {response.StatusCode} - {errorContent}");
                        return new List<Checkpoint>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in GetCheckpointsAsync: {ex.Message}");
                    return new List<Checkpoint>();
                }
            }
        }

        public async Task<bool> SetActiveCheckpointAsync(string checkpointName)
        {
            if (!IsServerRunning("127.0.0.1", 7860))
            {
                return false;
            }

            using (var client = new HttpClient())
            {
                try
                {
                    var payload = new Dictionary<string, object>
                    {
                        ["sd_model_checkpoint"] = checkpointName
                    };
                    string json = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://127.0.0.1:7860/sdapi/v1/options", content);
                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

    }

    public class ImageData
    {
        public List<string> images { get; set; }
        public Prompt promptData{ get; set; }
        public string info { get; set; }
    }

    public class SDImage
    {
        public string ImagePath;
        public Prompt PromptData;

        public SDImage(string imagePath, Prompt promptData)
        {
            ImagePath = imagePath;
            PromptData = promptData;
        }
    }

    public struct Checkpoint
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("model_name")]
        public string ModelName { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("config")]
        public string Config { get; set; }
    }

}
