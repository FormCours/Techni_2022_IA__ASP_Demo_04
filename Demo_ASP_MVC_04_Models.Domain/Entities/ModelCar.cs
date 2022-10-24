using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ASP_MVC_04_Models.Domain.Entities
{
    public class ModelCar
    {
        public int ModelCarId { get; set; }
        public string Name { get; set; }
        public string? BodyStyle { get; set; }
        public int? YearRelease { get; set; }
        public int? BrandId { get; set; }


        // Propriétés de navigation
        public Brand? Brand { get; set; }
        public IEnumerable<EngineCar>? Engines {get; set; }
    }
}
