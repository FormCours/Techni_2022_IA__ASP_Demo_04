using Demo_ASP_MVC_04_Models.BLL.Interfaces;
using Demo_ASP_MVC_04_Models.DAL.Interfaces;
using Demo_ASP_MVC_04_Models.Domain.Constants;
using Demo_ASP_MVC_04_Models.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ASP_MVC_04_Models.BLL.Services
{
    public class BrandService : IBrandService
    {

        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        

        public bool Delete(int id)
        {
            return _brandRepository.Delete(id);
        }

        public IEnumerable<Brand> GetAll()
        {
            return _brandRepository.GetAll();
        }

        public int Add(Brand brand)
        {
            if (BanWords.Fr.Contains(brand.Name))
            {
                return 0;
            }

            return _brandRepository.Add(brand);
        }

        public Brand? GetById(int id)
        {
            return _brandRepository.GetById(id);
        }

        public bool Update(int id, Brand brand)
        {
            if (BanWords.Fr.Contains(brand.Name))
            {
                return false;
            }

            return _brandRepository.Update(id, brand);
        }

        
    }
}
