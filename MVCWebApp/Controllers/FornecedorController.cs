using Microsoft.AspNetCore.Mvc;

namespace MVCWebApp.Controllers
{
    public class FornecedorController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
