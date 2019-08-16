using System.Threading.Tasks;
using Truescriber.BLL.Clients.SpeechToTextModels;

namespace Truescriber.BLL.Interfaces
{
    public interface ISpeechClient
    {
        Task<SpeechToTextViewModel> SyncRecognize(byte[] file);
        Task<SpeechToTextViewModel> AsyncRecognize(byte[] file);
    }
}
