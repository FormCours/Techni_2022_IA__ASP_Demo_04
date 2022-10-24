using Demo_ASP_MVC_04_Models.Domain.Entities;

namespace Demo_ASP_MVC_04_Models.DAL.Interfaces
{
    public interface IBrandRepository
        : IRepositoryBase<int, Brand>
    {
        // Custom method
        Brand? GetByName(string name);
    }
}
