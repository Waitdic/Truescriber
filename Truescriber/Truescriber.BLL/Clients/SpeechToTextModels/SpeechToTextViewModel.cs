using System.Collections.Generic;
using Google.Cloud.Speech.V1;

namespace Truescriber.BLL.Clients.SpeechToTextModels
{
    public class SpeechToTextViewModel
    {
        public string Text { get; set; }
        //TODO: redid List to array;
        //public List<WordInfo> WordInfo { get; set; }
        public WordInfo[] WordInfo { get; set; }
    }
}
