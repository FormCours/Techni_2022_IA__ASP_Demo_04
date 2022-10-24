using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ASP_MVC_04_Models.DAL.Helpers
{
    public static class RepositoryHelper
    {
        public static void CreateParameterWithValue(this IDbCommand command,string paramName, object? value)
        {
            IDbDataParameter paramId = command.CreateParameter();
            paramId.ParameterName = paramName;

            // paramId.Value = value is null ? DBNull.Value : value;
            paramId.Value = value ?? DBNull.Value;

            command.Parameters.Add(paramId);
        }
    }
}
