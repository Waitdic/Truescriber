using System;
using Google.Api.Gax.Grpc;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using Grpc.Auth;
using Truescriber.BLL.Interfaces;

namespace Truescriber.BLL.Clients
{
    public class SpeechToTextClient 
    {
        public string SyncRecognizeShort(byte[] file)
        {
            //var credential = GoogleCredential.GetApplicationDefault();
            //var speech = SpeechClient.Create();
            /*var response = speech.Recognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 16000,
                LanguageCode = "en",
            }, RecognitionAudio.FromFile(filePath));*/

            //var speech = SpeechClient.Create();

            /*var scopes = new []{string.Empty};
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer {
                ClientSecrets = new ClientSecrets {
                    ClientId = "3bf1c969ac47327bc075d8103b910d02e4593ae4",
                    ClientSecret = "AIzaSyBzcR5Br4H2Zj0RMC469PG6WDdCflx6JqY"
                },
                Scopes = scopes
            });
            var token = new TokenResponse {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            var credential = new UserCredential(flow, null, token);
            var service = new CalendarService(new BaseClientService.Initializer {
                HttpClientInitializer = credential,
                ApplicationName = AppSettings.GoogleApiProjectName
            });*/

            const string credentialPath = @"E:\gitlab\Truescriber-3bf1c969ac47.json";
            var credential = GoogleCredential.FromFile(credentialPath);
            var channel = new Grpc.Core.Channel(SpeechClient.DefaultEndpoint.Host, credential.ToChannelCredentials());
            var speech = SpeechClient.Create(channel);
            var test = "";
            var response = speech.Recognize(new RecognitionConfig()
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
            
            /*var test = "";
            var speech = SpeechClient.Create();
            var config = new RecognitionConfig
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Flac,
                SampleRateHertz = 16000,
                LanguageCode = LanguageCodes.English.UnitedStates
            };
            var audio = RecognitionAudio.FromStorageUri("gs://cloud-samples-tests/speech/brooklyn.flac");
            var response = speech.Recognize(config, audio);
            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    test += alternative.Transcript;
                }
            }

            return test;*/
        }
    }
}
