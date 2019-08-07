using Truescriber.BLL.EditModel;
using Truescriber.BLL.PageModel;
using Truescriber.BLL.UploadModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Truescriber.BLL.Interfaces
{
    public interface ITaskService
    {
        ProfileViewModel CreateProfile(int page, string id);
        UploadViewModel UploadFile(
            string id, 
            UploadViewModel item, 
            ModelStateDictionary modelState);
        bool GetFormatValid(string format);
        string GetFormatError();
        void EditTask(EditViewModel item);
        void DeleteTask(int id);
    }
}
