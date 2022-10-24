using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ASP_MVC_04_Models.BLL.Interfaces;
public interface IServiceBase<TKey, TEntity>
{
    // Tout les services ne sont pas obligatoirement des CRUD ;)

    TEntity? GetById(TKey id);

    IEnumerable<TEntity> GetAll();

    TKey Add(TEntity entity);

    bool Update(int id, TEntity brand);

    bool Delete(int id);

}

