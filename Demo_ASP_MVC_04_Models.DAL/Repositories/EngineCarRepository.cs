using Demo_ASP_MVC_04_Models.DAL.Helpers;
using Demo_ASP_MVC_04_Models.DAL.Interfaces;
using Demo_ASP_MVC_04_Models.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ASP_MVC_04_Models.DAL.Repositories
{
    public class EngineCarRepository : RepositoryBase<int, EngineCar>, IEngineCarRepository
    {
        public EngineCarRepository(IDbConnection connection)
            : base(connection, "Engine_Car", "Engine_Car_Id")
        { }

        protected override EngineCar Mapper(IDataRecord record)
        {
            return new EngineCar
            {
                EngineCarId = (int)record[TableId],
                Name = (string)record["Name"],
                PowerType = (string)record["Power_Type"]
            };
        }

        public override int Add(EngineCar entity)
        {
            IDbCommand command = _connection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = $"INSERT INTO [{TableName}] ([Name], [Power_Type])" +
                                  $" OUTPUT [inserted].[{TableId}]" +
                                  $" VALUES (@Name, @PowerType)";

            command.CreateParameterWithValue("Name", entity.Name);
            command.CreateParameterWithValue("PowerType", entity.PowerType);

            _connection.Open();
            int id = (int)command.ExecuteScalar();
            _connection.Close();

            return id;
        }

        public override bool Update(int id, EngineCar entity)
        {
            IDbCommand command = _connection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = $"UPDATE [{TableName}]" +
                                  $" SET [Name] = @Name" +
                                  $" , [Power_Type] = @PowerType " +
                                  $" WHERE [{TableId}] = @Id";

            command.CreateParameterWithValue("Name", entity.Name);
            command.CreateParameterWithValue("PowerType", entity.PowerType);
            command.CreateParameterWithValue("Id", id);


            _connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            _connection.Close();

            return rowAffected == 1;
        }
    }
}
