using System.Threading.Tasks;

namespace Truescriber.BLL.Interfaces
{
    public interface ISpeechClient
    {
        Task<string> SyncRecognize(byte[] file);
        Task<string> AsyncRecognize(byte[] file);
    }
}
