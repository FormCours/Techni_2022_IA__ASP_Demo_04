using Demo_ASP_MVC_04_Models.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo_ASP_MVC_04_Models.WebApp.Controllers
{
    public class EngineCarController : Controller
    {

        private readonly IEngineCarService _engineCarService;

        public EngineCarController(IEngineCarService engineCarService)
        {
            _engineCarService = engineCarService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
