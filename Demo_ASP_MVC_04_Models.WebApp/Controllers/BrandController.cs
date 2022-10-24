using Demo_ASP_MVC_04_Models.BLL.Interfaces;
using Demo_ASP_MVC_04_Models.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Demo_ASP_MVC_04_Models.WebApp.Models.Mappers;
using Demo_ASP_MVC_04_Models.Domain.Entities;

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
            // Récuperation des données via le service
            IEnumerable<Brand> brands = _brandService.GetAll();

            // Mapper les données "Domaine" vers le ViewModel
            // - Créer une nouvelle liste
            List<BrandViewModel> brandsVM = new List<BrandViewModel>();
            // - On parcours les données pour les mapper
            foreach(Brand brand in brands)
            {
                brandsVM.Add(brand.ToViewModel());
            }

            // Equivalent en utilisant du LinQ
            IEnumerable<BrandViewModel> brandsVM2 = brands.Select(b => b.ToViewModel());

            // Renvoi de la vue avec les données
            return View(brandsVM);
        }

    }
}
