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
        private readonly SpeechToTextViewModel _model;

        public SpeechToTextClient()
        {
            _model = new SpeechToTextViewModel();
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
                    var count = alternative.Words.Count;
                    _model.WordInfo = new WordInfo[count];

                    for (var i = 0; i < count; i++)
                        _model.WordInfo[i] = alternative.Words[i];
                }
            }
            return _model;
        }

        public async Task<SpeechToTextViewModel> AsyncRecognize(byte[] file)
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
                    var count = alternative.Words.Count;
                    _model.WordInfo = new WordInfo[count];
                    for (var i = 0; i < count; i++)
                        _model.WordInfo[i] = alternative.Words[i];
                }
            }
            return _model;
        }
    }
}
