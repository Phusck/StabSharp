using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;


namespace StabSharp
{
    internal class SaveSystem
    {

        private const string CATEGORIESFILE = "E:/StabSharp/categories.json";
        private const string LORASFILE = "E:/StabSharp/loras.json";
        private const string SAVEDIMAGESFOLDER = "E:/StabSharp/Saved/";
        private const string LASTPROMPTSETUPFILE = "E:/StabSharp/LastPromptFile.json";
        private string[] stringsToIgnorre = { "New Category", "New Prompt Part", "New Lora Part", "New Lora Part" };


        public static void SaveCategoriesToJson(ObservableCollection<PromptPartCategory> categories)
        {
            string json = JsonConvert.SerializeObject(categories, Formatting.Indented);
            File.WriteAllText(CATEGORIESFILE, json);
        }
        public static void SaveLorasToJson(ObservableCollection<Lora> loras)
        {
            string json = JsonConvert.SerializeObject(loras, Formatting.Indented);
            File.WriteAllText(LORASFILE, json);
        }
        public static void SaveLastPromptSetup(InputForm inputForm)
        {
            InputSave inputSave = new InputSave()
            {
                PromptParts = inputForm.PromptParts,
                NegativePrompt = inputForm.NegativePrompt
            };
            string json = JsonConvert.SerializeObject(inputSave,Formatting.Indented);
            File.WriteAllText(LASTPROMPTSETUPFILE, json);

        }

        //A safe way of Saving, So no data is lost, can only add to the file
        public static void SafeSaveCategoriesToJson(ObservableCollection<PromptPartCategory> categories)
        {
            //First we load the current data
            ObservableCollection<PromptPartCategory> currentCategories = LoadCategoriesFromJson();
            //then we merge the new data with the old data, Comparing the names of the categories
            foreach (PromptPartCategory newCategory in categories)
            {
                bool found = false;
                foreach (PromptPartCategory currentCategory in currentCategories)
                {
                    if (newCategory.Name == currentCategory.Name)
                    {
                        found = true;
                        foreach (PromptPart newPromptPart in newCategory.PromptParts)
                        {
                            bool foundPromptPart = false;
                            foreach (PromptPart currentPromptPart in currentCategory.PromptParts)
                            {
                                if (newPromptPart.Text == currentPromptPart.Text)
                                {
                                    foundPromptPart = true;
                                    break;
                                }
                            }
                            if (!foundPromptPart)
                            {
                                currentCategory.PromptParts.Add(newPromptPart);
                            }
                        }
                        break;
                    }
                }
                if (!found)
                {
                    currentCategories.Add(newCategory);
                }
            }
            //then we save the merged data
            SaveCategoriesToJson(currentCategories);
        }
        public static void SafeSaveLorasToJson(ObservableCollection<Lora> loras)
        {
            //First we load the current data
            ObservableCollection<Lora> currentLoras = LoadLorasFromJson();
            //then we merge the new data with the old data, Comparing the names of the categories
            foreach (Lora newLora in loras)
            {
                bool found = false;
                foreach (Lora currentLora in currentLoras)
                {
                    if (newLora.LoraName == currentLora.LoraName)
                    {
                        found = true;
                        foreach (PromptPart newPromptPart in newLora.Parts)
                        {
                            bool foundPromptPart = false;
                            foreach (PromptPart currentPromptPart in currentLora.Parts)
                            {
                                if (newPromptPart.Text == currentPromptPart.Text)
                                {
                                    foundPromptPart = true;
                                    break;
                                }
                            }
                            if (!foundPromptPart)
                            {
                                currentLora.Parts.Add(newPromptPart);
                            }
                        }
                        break;
                    }
                }
                if (!found)
                {
                    currentLoras.Add(newLora);
                }
            }
            //then we save the merged data
            SaveLorasToJson(currentLoras);
        }

        public static ObservableCollection<PromptPartCategory> LoadCategoriesFromJson()
        {

            if (File.Exists(CATEGORIESFILE))
            {
                string json = File.ReadAllText(CATEGORIESFILE);
                // Adjust deserialization settings if needed, for example to handle missing members, etc.
                var settings = new JsonSerializerSettings
                {
                    // If your JSON might contain additional data that's not represented in your classes, you might want to ignore those:
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                var categories = JsonConvert.DeserializeObject<ObservableCollection<PromptPartCategory>>(json, settings);

                if (categories == null)
                {
                    return new ObservableCollection<PromptPartCategory>(); // Ensure we never return null
                }
                return categories;
            }
            else
            {
                return new ObservableCollection<PromptPartCategory>();
            }
        }
        public static ObservableCollection<Lora> LoadLorasFromJson()
        {
            if (File.Exists(LORASFILE))
            {
                string json = File.ReadAllText(LORASFILE);
                // Adjust deserialization settings if needed, for example to handle missing members, etc.
                var settings = new JsonSerializerSettings
                {
                    // If your JSON might contain additional data that's not represented in your classes, you might want to ignore those:
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                var loras = JsonConvert.DeserializeObject<ObservableCollection<Lora>>(json, settings);

                if (loras == null)
                {
                    return new ObservableCollection<Lora>();
                }; // Ensure we never return null

                return loras;
            }
            else
            {
                return new ObservableCollection<Lora>();
            }
        }
        public static InputSave? LoadLastPrompt()
        {
            if (!File.Exists(LASTPROMPTSETUPFILE))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<InputSave>(File.ReadAllText(LASTPROMPTSETUPFILE));
             
        }

        public static void SaveCopyOfFileToSaveFolder(string fromPath)
        {
            if (!Directory.Exists(SAVEDIMAGESFOLDER))
            {
                Directory.CreateDirectory(SAVEDIMAGESFOLDER);
            }

            //Find next available name
            int i = 1;
            string iString = i.ToString("D3");
            while (File.Exists(SAVEDIMAGESFOLDER + iString + ".png"))
            {
                i++;
                iString = i.ToString("D3");
            }
            File.Copy(fromPath, SAVEDIMAGESFOLDER + iString +".png");
        }
    }


}
