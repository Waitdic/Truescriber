using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using Grpc.Auth;
using Truescriber.BLL.Interfaces;

namespace Truescriber.BLL.Clients
{
    public class SpeechToTextClient : ISpeechClient
    {
        private const string CredentialPath = @"E:\gitlab\Truescriber-3bf1c969ac47.json";

        private static SpeechClient SpeechProperty()
        {
            var credential = GoogleCredential.FromFile(CredentialPath);
            var channel = new Grpc.Core.Channel(SpeechClient.DefaultEndpoint.Host, credential.ToChannelCredentials());
            var speech = SpeechClient.Create(channel);
            return speech;
        }

        public async Task<string> SyncRecognize(byte[] file)
        {
            var test = "";
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
                    test += alternative.Transcript;
                    foreach (var item in alternative.Words)
                    {
                        test += "   :Word - " + item.Word + "   :StartTime - " + item.StartTime + "   :FinishTime - " + item.EndTime + "\n";
                    }
                }
            }
            return test;
        }

        public async Task<string> AsyncRecognize(byte[] file)
        {
            var test = "";
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
                    test += alternative.Transcript;
                    foreach (var item in alternative.Words)
                    {
                        test += "   :Word - " + item.Word + "   :StartTime - " + item.StartTime + "   :FinishTime - " + item.EndTime ;
                    }
                }
            }
            return test;
        }
    }
}
