using Demo_ASP_MVC_04_Models.DAL.Helpers;
using Demo_ASP_MVC_04_Models.DAL.Interfaces;
using Demo_ASP_MVC_04_Models.Domain.Entities;
using System.Data;

namespace Demo_ASP_MVC_04_Models.DAL.Repositories
{
    public class BrandRepository : RepositoryBase<int, Brand>, IBrandRepository
    {
        public BrandRepository(IDbConnection connection) 
            : base(connection, "Brand", "Brand_Id")
        { }

        protected override Brand Mapper(IDataRecord record)
        {
            return new Brand()
            {
                // BrandId = record.GetInt32(record.GetOrdinal("Brand_Id")),
                BrandId = (int)record["Brand_Id"],
                Name = (string)record["Name"],
                Country = (record["Country"] is DBNull) ? null : (string)record["Country"]
            };
        }

        public override int Add(Brand entity)
        {
            IDbCommand command = _connection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO [Brand] ([Name], [Country])" +
                                  " OUTPUT [inserted].[Brand_Id]" +
                                  " VALUES (@Name, @Country)";

            command.CreateParameterWithValue("Name", entity.Name);
            command.CreateParameterWithValue("Country", entity.Country);

            _connection.Open();
            int id = (int)command.ExecuteScalar();
            _connection.Close();

            return id;
        }

        public override bool Update(int id, Brand entity)
        {
            IDbCommand command = _connection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE [Brand]" +
                                  " SET [Name] = @Name" +
                                  " , [Country] = @Country " +
                                  " WHERE [Brand_Id] = @Id";

            command.CreateParameterWithValue("Name", entity.Name);
            command.CreateParameterWithValue("Country", entity.Country);
            command.CreateParameterWithValue("Id", id);


            _connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            _connection.Close();

            return rowAffected == 1;
        }

        public Brand? GetByName(string name)
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT * FROM [{TableName}] WHERE [Name] = @Name";
            command.CreateParameterWithValue("Name", name);

            Brand? brand = null;

            _connection.Open();
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    brand = Mapper(reader);
                }
            }
            _connection.Close();

            return brand;
        }
    }
}
