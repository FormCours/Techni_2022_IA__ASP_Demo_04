using Demo_ASP_MVC_04_Models.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ASP_MVC_04_Models.DAL.Interfaces
{
    public interface IModelCarRepository : IRepositoryBase<int, ModelCar>
    {
        ModelCar GetWithEngines(int modelCarId);
        bool AddEngine(int modelCarId, EngineCar engine);
        bool RemoveEngine(int modelCarId, EngineCar engine);
    }
}
