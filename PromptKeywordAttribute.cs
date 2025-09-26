using System;

namespace StabSharp
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class PromptKeywordAttribute : Attribute
    {
        public string Marker
        {
            get; private set;
        }

        public PromptKeywordAttribute(string marker)
        {
            Marker = marker;
        }
    }
}
