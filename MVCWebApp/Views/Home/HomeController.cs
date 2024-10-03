using Microsoft.AspNetCore.Mvc;

namespace MVCWebApp.Views.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Isso renderiza a view Index.cshtml dentro de Views/Home/
        }
    }
}
