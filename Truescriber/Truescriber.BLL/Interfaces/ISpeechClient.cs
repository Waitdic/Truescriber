namespace Truescriber.BLL.Interfaces
{
    public interface ISpeechClient
    {
        string SyncRecognize(byte[] file);
        string AsyncRecognize(byte[] file);
    }
}
