using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Truescriber.DAL.Entities;

namespace Truescriber.WEB.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public ViewResult Index() => View();

    }
}
