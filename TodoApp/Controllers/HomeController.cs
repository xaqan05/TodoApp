using Microsoft.AspNetCore.Mvc;
namespace TodoApp.Controllers
{
    public class HomeController : Controller
    {
        public string Hi(string name)
        {
            return $"Salam {name}";
        }

        public int Random()
        {
            return 7;
        }
    }
}
