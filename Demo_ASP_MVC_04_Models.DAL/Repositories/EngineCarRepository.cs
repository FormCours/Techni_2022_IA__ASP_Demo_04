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
    public class EngineCarRepository : IEngineCarRepository
    {
        private readonly IDbConnection _connection;

        public EngineCarRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        private EngineCar Mapper(IDataRecord record)
        {
            return new EngineCar
            {
                EngineCarId = (int)record["Engine_Car_Id"],
                Name = (string)record["Name"],
                PowerType = (string)record["Power_Type"]
            };
        }

        public EngineCar? GetById(int id)
        {
            IDbCommand cmd = _connection.CreateCommand();

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT * FROM [Engine_Car] WHERE [Engine_Car_Id] = @Id";

            cmd.CreateParameterWithValue("id", id);

            _connection.Open();

            EngineCar? engineCar = null;

            using (IDataReader reader = cmd.ExecuteReader())
            {

                if (reader.Read())
                {
                    engineCar = Mapper(reader);
                }

            }

            _connection.Close();

            return engineCar;
        }
        
        public IEnumerable<EngineCar> GetAll()
        {
            IDbCommand command = _connection.CreateCommand();

            // - Type de la commande
            command.CommandType = CommandType.Text;

            // - Contenu de la commande
            command.CommandText = "SELECT * FROM [Engine_Car]";

            // Ouverture de la connexion
            _connection.Open();

            // Execution de la commande SQL
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Utilisation du mapper entre la DB et la DAL
                    yield return Mapper(reader);
                }
            }

            // Fermeture de la connexion
            _connection.Close();
        }

        public int Add(EngineCar entity)
        {
            IDbCommand command = _connection.CreateCommand();

            command.CommandType = CommandType.Text;

            command.CommandText = "INSERT INTO [Engine_Car] ([Name], [Power_Type])" +
                                  "OUTPUT [inserted].[Engine_Car_Id]" +
                                  "VALUES (@Name, @PowerType)";

            command.CreateParameterWithValue("Name", entity.Name);
            command.CreateParameterWithValue("PowerType", entity.PowerType);

            _connection.Open();

            int id = (int)command.ExecuteScalar();

            _connection.Close();

            return id;
        }

        public bool Update(int id, EngineCar entity)
        {
            IDbCommand command = _connection.CreateCommand();

            command.CommandType = CommandType.Text;

            command.CommandText = "UPDATE [Engine_Car]" +
                                  "SET [Name] = @Name" +
                                  ", [Power_Type] = @PowerType " +
                                  "WHERE [Engine_Car_Id] = @Id";

            command.CreateParameterWithValue("Name", entity.Name);
            command.CreateParameterWithValue("PowerType", entity.PowerType);
            command.CreateParameterWithValue("Id", id);


            _connection.Open();

            int rowAffected = command.ExecuteNonQuery();

            _connection.Close();

            return rowAffected == 1;
        }

        public bool Delete(int id)
        {
            IDbCommand command = _connection.CreateCommand();

            command.CommandType = CommandType.Text;

            command.CommandText = "DELETE FROM [Engine_Car] WHERE [Engine_Car_Id] = @Id";

            command.CreateParameterWithValue("Id", id);

            _connection.Open();

            int rowAffected = command.ExecuteNonQuery();

            _connection.Close();

            return rowAffected == 1;
        }

        
    }
}
