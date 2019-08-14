using System;
using System.Collections.Generic;
using System.Text;

namespace Truescriber.BLL.Interfaces
{
    public interface IClient
    {
        string SyncRecognizeShort(string filePath);
    }
}
