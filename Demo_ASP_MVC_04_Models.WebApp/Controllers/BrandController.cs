using Demo_ASP_MVC_04_Models.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo_ASP_MVC_04_Models.WebApp.Controllers
{
    public class BrandController : Controller
    {

        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public IActionResult Index()
        {
            
        }

    }
}
