using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;


namespace StabSharp
{
    internal class SaveSystem
    {

        private const string CATEGORIESFILE = "E:/StabSharp/categories.json";
        private const string LORASFILE = "E:/StabSharp/loras.json";
        private const string LORACATEGORIESFILE = "E:/StabSharp/loraCategories.json";
        private const string SAVEDIMAGESFOLDER = "E:/StabSharp/Saved/";
        private const string LASTPROMPTSETUPFILE = "E:/StabSharp/LastPromptFile.json";
        private string[] stringsToIgnorre = { "New Category", "New Prompt Part", "New Lora Part", "New Lora Part" };

        // Legacy shape for backward compatibility with older LastPromptFile.json.
        private sealed class LegacyInputSave
        {
            public ObservableCollection<PromptPart> PromptParts { get; set; }
            public string NegativePrompt { get; set; }
        }


        public static void SaveCategoriesToJson(BindingList<PromptPartCategory> categories)
        {
            string json = JsonConvert.SerializeObject(categories, Formatting.Indented);
            File.WriteAllText(CATEGORIESFILE, json);
        }
        public static void SaveLorasToJson(BindingList<Lora> loras)
        {
            string json = JsonConvert.SerializeObject(loras, Formatting.Indented);
            File.WriteAllText(LORASFILE, json);
        }
        public static void SaveLoraCategoriesToJson(BindingList<LoraCategory> loraCategories)
        {
            string json = JsonConvert.SerializeObject(loraCategories, Formatting.Indented);
            File.WriteAllText(LORACATEGORIESFILE, json);
        }
        public static void SaveLastPromptSetup(InputForm inputForm)
        {
            if (inputForm == null)
            {
                return;
            }

            Prompt promptToSave = inputForm.BuildPromptSnapshotForSave();
            string json = JsonConvert.SerializeObject(promptToSave, Formatting.Indented);
            File.WriteAllText(LASTPROMPTSETUPFILE, json);

        }

        //A safe way of Saving, So no data is lost, can only add to the file
        public static void SafeSaveCategoriesToJson(BindingList<PromptPartCategory> categories)
        {
            //First we load the current data
            BindingList<PromptPartCategory> currentCategories = LoadCategoriesFromJson();
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
        public static void SafeSaveLorasToJson(BindingList<Lora> loras)
        {
            //First we load the current data
            BindingList<Lora> currentLoras = LoadLorasFromJson();
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
        public static void SafeSaveLoraCategoriesToJson(BindingList<LoraCategory> loraCategories)
        {
            // First load the current data
            var currentCategories = LoadLoraCategoriesFromJson();

            // Merge incoming categories into current
            foreach (var newCategory in loraCategories)
            {
                bool foundCategory = false;
                foreach (var currentCategory in currentCategories)
                {
                    if (newCategory.LoraCategoryName == currentCategory.LoraCategoryName)
                    {
                        foundCategory = true;
                        if (newCategory.Loras == null)
                        {
                            continue;
                        }

                        // Merge each Lora in this category
                        foreach (var newLora in newCategory.Loras)
                        {
                            if (currentCategory.Loras == null)
                            {
                                continue; // Skip the ignored Lora names
                            }

                            bool foundLora = false;
                            foreach (var currentLora in currentCategory.Loras)
                            {
                                if (newLora.LoraName == currentLora.LoraName)
                                {
                                    foundLora = true;
                                    // Merge parts within this Lora
                                    foreach (var newPart in newLora.Parts)
                                    {
                                        if (!currentLora.Parts.Any(p => p.Text == newPart.Text))
                                        {
                                            currentLora.Parts.Add(newPart);
                                        }
                                    }
                                    break;
                                }
                            }
                            if (!foundLora)
                            {
                                currentCategory.Loras.Add(newLora);
                            }
                        }
                        break;
                    }
                }
                if (!foundCategory)
                {
                    currentCategories.Add(newCategory);
                }
            }

            // Save the merged data
            SaveLoraCategoriesToJson(currentCategories);
        }

        public static BindingList<PromptPartCategory> LoadCategoriesFromJson()
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
                var categories = JsonConvert.DeserializeObject<BindingList<PromptPartCategory>>(json, settings) ?? new BindingList<PromptPartCategory>();
                NormalizeCategories(categories);
                return categories;
            }
            else
            {
                return new BindingList<PromptPartCategory>();
            }
        }
        public static BindingList<Lora> LoadLorasFromJson()
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
                var loras = JsonConvert.DeserializeObject<BindingList<Lora>>(json, settings) ?? new BindingList<Lora>();
                NormalizeLoras(loras);
                return loras;
            }
            else
            {
                return new BindingList<Lora>();
            }
        }
        internal static BindingList<LoraCategory> LoadLoraCategoriesFromJson()
        {

            if (File.Exists(LORACATEGORIESFILE))
            {
                string json = File.ReadAllText(LORACATEGORIESFILE);
                // Adjust deserialization settings if needed, for example to handle missing members, etc.
                var settings = new JsonSerializerSettings
                {
                    // If your JSON might contain additional data that's not represented in your classes, you might want to ignore those:
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                var loraCategories = JsonConvert.DeserializeObject<BindingList<LoraCategory>>(json, settings) ?? new BindingList<LoraCategory>();
                NormalizeLoraCategories(loraCategories);
                return loraCategories;
            }
            else
            {
                return new BindingList<LoraCategory>();
            }
        }

        private static void NormalizeCategories(BindingList<PromptPartCategory> categories)
        {
            foreach (var c in categories)
            {
                if (c.PromptParts == null)
                {
                    c.PromptParts = new BindingList<PromptPart>();
                }
            }
        }

        private static void NormalizeLoras(BindingList<Lora> loras)
        {
            foreach (var l in loras)
            {
                if (l.Parts == null)
                {
                    l.Parts = new BindingList<PromptPart>();
                }
            }
        }

        private static void NormalizeLoraCategories(BindingList<LoraCategory> categories)
        {
            foreach (var c in categories)
            {
                if (c.Loras == null)
                {
                    c.Loras = new BindingList<Lora>();
                }

                foreach (var l in c.Loras)
                {
                    if (l.Parts == null)
                    {
                        l.Parts = new BindingList<PromptPart>();
                    }
                }
            }
        }

        public static Prompt? LoadLastPrompt()
        {
            if (!File.Exists(LASTPROMPTSETUPFILE))
            {
                return null;
            }

            string json = File.ReadAllText(LASTPROMPTSETUPFILE);

            var settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            // Preferred: new format (Prompt JSON)
            try
            {
                return JsonConvert.DeserializeObject<Prompt>(json, settings);
            }
            catch (JsonException)
            {
                // Fall through to legacy format.
            }

            // Legacy: InputSave JSON (PromptParts as structured PromptPart + NegativePrompt string)
            try
            {
                var legacy = JsonConvert.DeserializeObject<LegacyInputSave>(json, settings);
                if (legacy == null)
                {
                    return null;
                }

                return new Prompt
                {
                    PromptParts = legacy.PromptParts != null
                        ? legacy.PromptParts.Select(p => p.ToString()).ToArray()
                        : Array.Empty<string>(),
                    NegativePromptParts = !string.IsNullOrWhiteSpace(legacy.NegativePrompt)
                        ? legacy.NegativePrompt.Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToArray()
                        : Array.Empty<string>()
                };
            }
            catch (JsonException)
            {
                return null;
            }
             
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
