using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo_ASP_MVC_04_Models.WebApp.Models
{
    public class BrandViewModel
    {
        [ScaffoldColumn(false)]
        public int BrandId { get; set; }

        [DisplayName("Marque")]
        public string Name { get; set; }

        [DisplayName("Pays d'origine")]
        public string? Country { get; set; }
    }
}
