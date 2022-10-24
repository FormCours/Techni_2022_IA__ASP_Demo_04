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
    public class EngineCarService : IEngineCarService
    {
        private readonly IEngineCarRepository _engineCarRepository;

        public EngineCarService(IEngineCarRepository engineCarRepository)
        {
            _engineCarRepository = engineCarRepository;
        }


        public int Add(EngineCar entity)
        {
            if (BanWords.Fr.Contains(entity.Name))
            {
                return 0;
            }

            return _engineCarRepository.Add(entity);
        }

        public bool Delete(int id)
        {
            return _engineCarRepository.Delete(id);
        }

        public IEnumerable<EngineCar> GetAll()
        {
            return _engineCarRepository.GetAll();
        }

        public EngineCar? GetById(int id)
        {
            return _engineCarRepository.GetById(id);
        }

        public bool Update(int id, EngineCar brand)
        {
            if (BanWords.Fr.Contains(brand.Name))
            {
                return false;
            }

            return _engineCarRepository.Update(id, brand);
        }
    }
}
