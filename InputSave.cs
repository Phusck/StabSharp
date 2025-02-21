using System;
using System.Collections.ObjectModel;

namespace StabSharp
{
    [Serializable]
    internal struct InputSave
    {
        public ObservableCollection<PromptPart> PromptParts;
        public string NegativePrompt;
    }
}
