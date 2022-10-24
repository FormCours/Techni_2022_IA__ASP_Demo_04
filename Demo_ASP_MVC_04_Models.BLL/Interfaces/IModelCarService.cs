using Demo_ASP_MVC_04_Models.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ASP_MVC_04_Models.BLL.Interfaces
{
    public interface IModelCarService
    {
        // Otenir les models avec leurs marques
        public IEnumerable<ModelCar> GetAll();

        // Obtenir un model avec sa marque et ses moteurs
        public ModelCar? GetFullDetail(int id);

        // Ajouter un model avec sa marque et ses moteurs
        public ModelCar Add(ModelCar modelCar, Brand? brand);
        public void AddEngines(ModelCar modelCar, IEnumerable<EngineCar> engines);
    }
}
