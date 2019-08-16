using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using Grpc.Auth;
using Truescriber.BLL.Clients.SpeechToTextModels;
using Truescriber.BLL.Interfaces;

namespace Truescriber.BLL.Clients
{
    public class SpeechToTextClient : ISpeechClient
    {
        private const string CredentialPath = @"E:\gitlab\Truescriber-3bf1c969ac47.json";
        private string _text;
        private readonly SpeechToTextViewModel model;

        public SpeechToTextClient()
        {
            model = new SpeechToTextViewModel();
        }

        private static SpeechClient SpeechProperty()
        {
            var credential = GoogleCredential.FromFile(CredentialPath);
            var channel = new Grpc.Core.Channel(SpeechClient.DefaultEndpoint.Host, credential.ToChannelCredentials());
            var speech = SpeechClient.Create(channel);
            return speech;
        }

        public async Task<SpeechToTextViewModel> SyncRecognize(byte[] file)
        {
            var response = await SpeechProperty().RecognizeAsync(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Flac,
                SampleRateHertz = 48000,
                LanguageCode = "en",
                EnableAutomaticPunctuation = true,
                EnableWordTimeOffsets = true
            }, RecognitionAudio.FromBytes(file));

            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    model.Text = alternative.Transcript;
                    var count = alternative.Words.Count;
                    model.WordInfo = new WordInfo[count];
                    //var i = 0;
                    /*foreach (var item in alternative.Words)
                    {
                        model.WordInfo.Add(item);
                        //i++;
                    }*/
                    for (var i = 0; i < count; i++)
                        model.WordInfo[i] = alternative.Words[i];
                }
            }
            return model;
        }

        public async Task<string> AsyncRecognize(byte[] file)
        {
            var longOperation = await SpeechProperty().LongRunningRecognizeAsync(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Flac,
                SampleRateHertz = 48000,
                LanguageCode = "en",
                EnableAutomaticPunctuation = true,
                EnableWordTimeOffsets = true
            }, RecognitionAudio.FromBytes(file));
            longOperation = longOperation.PollUntilCompleted();
            var response = longOperation.Result;

            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    _text += alternative.Transcript;
                    foreach (var item in alternative.Words)
                    {
                        _text += "   :Word - " + item.Word + "   :StartTime - " + item.StartTime + "   :FinishTime - " + item.EndTime ;
                    }
                }
            }
            return _text;
        }
    }
}
