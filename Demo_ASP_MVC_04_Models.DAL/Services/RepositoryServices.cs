using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ASP_MVC_04_Models.DAL.Repositories
{
    public static class RepositoryServices
    {
        public static void CreateParameterWithValue(this IDbCommand command,string paramName, object? value)
        {
            IDbDataParameter paramId = command.CreateParameter();
            paramId.ParameterName = paramName;
            paramId.Value = value;
            command.Parameters.Add(paramId);
        }
    }
}
