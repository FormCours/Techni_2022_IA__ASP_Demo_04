using Demo_ASP_MVC_04_Models.Domain.Entities;

namespace Demo_ASP_MVC_04_Models.WebApp.Models.Mappers
{
    public static class BrandMapper
    {
        public static BrandViewModel ToViewModel(this Brand brand)
        {
            return new BrandViewModel()
            {
                BrandId = brand.BrandId,
                Name = brand.Name,
                Country = brand.Country
            };
        }
    }
}
