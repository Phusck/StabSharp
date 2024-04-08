using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StabSharp
{
    internal class Request
    {
        public RequestType RequestType { get; set; }
        public string Prompt { get; set; }

        public Request(RequestType type, string prompt)
        {
            RequestType = type;
            Prompt = prompt;
        }
    }

    public enum RequestType
    {
        TextToImage,
        ChangeModel
    }
}
