using Demo_ASP_MVC_04_Models.BLL.Interfaces;
using Demo_ASP_MVC_04_Models.DAL.Repositories;
using Demo_ASP_MVC_04_Models.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ASP_MVC_04_Models.BLL.Services
{
    public class ModelCarService : IModelCarService
    {
        private ModelCarRepository _modelCarRepository;
        private BrandRepository _brandRepository;
        private EngineCarRepository _engineCarRepository;

        public ModelCarService(ModelCarRepository modelCarRepository, BrandRepository brandRepository, EngineCarRepository engineCarRepository)
        {
            _modelCarRepository = modelCarRepository;
            _brandRepository = brandRepository;
            _engineCarRepository = engineCarRepository;
        }


        public IEnumerable<ModelCar> GetAll()
        {
            List<Brand> brands = _brandRepository.GetAll().ToList();

            foreach (ModelCar model in _modelCarRepository.GetAll())
            {
                model.Brand = brands.FirstOrDefault(b => b.BrandId == model.BrandId);
                yield return model;
            }
        }

        public ModelCar? GetFullDetail(int id)
        {
            ModelCar? model = _modelCarRepository.GetWithEngines(id);

            if (model is null)
            {
                return null;
            }

            if (model.BrandId != null)
            {
                model.Brand = _brandRepository.GetById((int)model.BrandId);
            }
            return model;
        }

        private int GetOrInsertBrand(Brand brand)
        {
            Brand? brandDB = _brandRepository.GetByName(brand.Name);

            if (brandDB != null)
            {
                return brandDB.BrandId;
            }
            return _brandRepository.Add(brand);
        }

        public ModelCar Add(ModelCar modelCar, Brand? brand)
        {
            int? brandId = null;

            if (brand != null && !string.IsNullOrWhiteSpace(brand.Name))
            {
                brandId = GetOrInsertBrand(brand);
            }

            modelCar.BrandId = brandId;
            int modelId = _modelCarRepository.Add(modelCar);

            return _modelCarRepository.GetById(modelId);
        }

        public void AddEngines(ModelCar modelCar, IEnumerable<EngineCar> engines)
        {
            List<EngineCar> allEngine = _engineCarRepository.GetAll().ToList();

            foreach (EngineCar e in engines)
            {
                int? newId = allEngine.SingleOrDefault(ae => ae.Name == e.Name)?.EngineCarId;

                if (newId is null)
                {
                    newId = _engineCarRepository.Add(e);
                }

                _modelCarRepository.AddEngine(modelCar.ModelCarId, (int)newId);
            }
        }
    }
}
