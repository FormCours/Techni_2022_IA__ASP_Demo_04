using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ASP_MVC_04_Models.DAL.Interfaces
{
    public interface IRepositoryBase<TId, TEntity>
    {
        TEntity? GetById(TId id);
        
        IEnumerable<TEntity> GetAll();

        TId Add(TEntity entity);

        bool Update(TId id, TEntity entity);

        bool Delete(TId id);
    }
}
