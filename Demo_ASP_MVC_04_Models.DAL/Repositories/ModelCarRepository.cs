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
    public class ModelCarRepository : RepositoryBase<int, ModelCar>, IModelCarRepository
    {
        public ModelCarRepository(IDbConnection connection) 
            : base(connection, "Model_Car", "Model_Car_Id")
        { }

        protected override ModelCar Mapper(IDataRecord record)
        {
            return new ModelCar()
            {
                ModelCarId = (int)record[TableId],
                Name = (string)record["Name"],
                BodyStyle = record["Body_Style"] is DBNull ? null : (string)record["Body_Style"],
                YearRelease = record["Year"] is DBNull ? null : (int)record["Year"],
                BrandId = record["Brand_Id"] is DBNull ? null : (int)record["Brand_Id"]
            };
        }

        public override int Add(ModelCar entity)
        {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"INSERT INTO [{TableName}] ([Name], [Body_Style], [Year], [Brand_Id])" +
                $" OUTPUT [inserted].[{TableId}]" +
                $" VALUES (@Name, @BStyle, @Year, @BrandId)";

            cmd.CreateParameterWithValue("Name", entity.Name);
            cmd.CreateParameterWithValue("BStyle", entity.BodyStyle);
            cmd.CreateParameterWithValue("Year", entity.YearRelease);
            cmd.CreateParameterWithValue("BrandId", entity.BrandId);

            _connection.Open();
            int id = (int)cmd.ExecuteScalar();
            _connection.Close();

            return id;
        }

        public override bool Update(int id, ModelCar entity)
        {
            throw new NotImplementedException();
        }
    }
}
