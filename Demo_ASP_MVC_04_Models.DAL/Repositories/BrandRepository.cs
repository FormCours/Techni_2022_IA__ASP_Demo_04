using Demo_ASP_MVC_04_Models.DAL.Helpers;
using Demo_ASP_MVC_04_Models.DAL.Interfaces;
using Demo_ASP_MVC_04_Models.Domain.Entities;
using System.Data;

namespace Demo_ASP_MVC_04_Models.DAL.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IDbConnection _connection;

        public BrandRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        private Brand Mapper(IDataRecord record)
        {
            return new Brand()
            {
                // BrandId = record.GetInt32(record.GetOrdinal("Brand_Id")),
                BrandId = (int)record["Brand_Id"],
                Name = (string)record["Name"],
                Country = (record["Country"] is DBNull) ? null : (string)record["Country"]
            };
        }

        public Brand? GetById(int id)
        {
            // Créer la commande SQL
            IDbCommand command = _connection.CreateCommand();

            // - Type de la commande
            command.CommandType = CommandType.Text;

            // - Contenu de la commande
            command.CommandText = "SELECT * FROM [Brand] WHERE [Brand_Id] = @Id";

            // - Ajout des parametres SQL
            command.CreateParameterWithValue("id", id);


            // Ouverture de la connexion
            _connection.Open();

            // Execution de la commande SQL
            Brand? brand = null;

            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    // Utilisation du mapper entre la DB et la DAL
                    brand = Mapper(reader);
                }
            }

            // Fermeture de la connexion
            _connection.Close();

            // Envoi des données
            return brand;
        }

        public IEnumerable<Brand> GetAll()
        {
            // Créer la commande SQL
            IDbCommand command = _connection.CreateCommand();

            // - Type de la commande
            command.CommandType = CommandType.Text;

            // - Contenu de la commande
            command.CommandText = "SELECT * FROM [Brand]";

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

        public int Add(Brand entity)
        {
            IDbCommand command = _connection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO [Brand] ([Name], [Country])" +
                                  "OUTPUT [inserted].[Brand_Id]" +
                                  "VALUES (@Name, @Country)";

            command.CreateParameterWithValue("Name", entity.Name);
            command.CreateParameterWithValue("Country", entity.Country);

            _connection.Open();
            int id = (int)command.ExecuteScalar();
            _connection.Close();

            return id;
        }

        public bool Update(int id, Brand entity)
        {
            IDbCommand command = _connection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE [Brand]" +
                                  "SET [Name] = @Name" +
                                  ", [Country] = @Country " +
                                  "WHERE [Brand_Id] = @Id";

            command.CreateParameterWithValue("Name", entity.Name);
            command.CreateParameterWithValue("Country", entity.Country);
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
            command.CommandText = "DELETE FROM [Brand] WHERE [Brand_Id] = @Id";

            command.CreateParameterWithValue("Id", id);

            _connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            _connection.Close();

            return rowAffected == 1;
        }
    }
}
