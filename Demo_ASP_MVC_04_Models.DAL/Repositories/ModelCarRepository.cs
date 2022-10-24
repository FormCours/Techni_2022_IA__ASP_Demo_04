using Demo_ASP_MVC_04_Models.DAL.Helpers;
using Demo_ASP_MVC_04_Models.DAL.Interfaces;
using Demo_ASP_MVC_04_Models.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        public ModelCar GetWithEngines(int modelCarId)
        {
            // Récuperation du model
            ModelCar? modelCar = GetById(modelCarId);

            // - Test de garde, si celui-ci est "null"
            if (modelCar is null)
            {
                return null;
            }

            // DataAdapter pour "Engine Car"
            IDbCommand engineCarSelect = _connection.CreateCommand();
            engineCarSelect.CommandType = CommandType.Text;
            engineCarSelect.CommandText = "SELECT ec.*" +
                " FROM [Engine_Car] ec" +
                "   INNER JOIN [Model_Engine_Car] mec ON ec.[Engine_Car_Id] = mec.[Engine_Car_Id]" +
                " WHERE mec.[Model_Car_Id] = @Id";

            IDbDataAdapter engineCarAdapter = DbProviderFactories
                .GetFactory((DbConnection)_connection)
                .CreateDataAdapter();
            engineCarAdapter.SelectCommand = engineCarSelect;

            // Création d'un DataSet
            DataSet dataSet = new DataSet();
            engineCarAdapter.Fill(dataSet);

            // Ajout des elements "Engine Car" dans l'objet "Model Car"
            List<EngineCar> engines = new List<EngineCar>();
            foreach (DataRow row in dataSet.Tables["Table"].Rows)
            {
                engines.Add(new EngineCar()
                {
                    EngineCarId = (int)row["Engine_Car_Id"],
                    Name = (string)row["Name"],
                    PowerType = (string)row["Power_Type"]
                });
            }
            modelCar.Engines = engines;

            return modelCar;
        }

        public bool AddEngine(int modelCarId, EngineCar engine)
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO [Model_Engine_Car] ([Model_Car_Id], [Engine_Car_Id])" +
                                    " VALUES (@Model_Car_Id, @Engine_Car_Id)";

            command.CreateParameterWithValue("Model_Car_Id", modelCarId);
            command.CreateParameterWithValue("Engine_Car_Id", engine.EngineCarId);

            _connection.Open();
            int nbRow = command.ExecuteNonQuery();
            _connection.Close();

            return nbRow == 1;
        }

        public bool RemoveEngine(int modelCarId, EngineCar engine)
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM [Model_Engine_Car]" +
                                    " WHERE [Model_Car_Id] = @Model_Car_Id" +
                                    " AND [Engine_Car_Id] = @Engine_Car_Id";

            command.CreateParameterWithValue("Model_Car_Id", modelCarId);
            command.CreateParameterWithValue("Engine_Car_Id", engine.EngineCarId);

            _connection.Open();
            int nbRow = command.ExecuteNonQuery();
            _connection.Close();

            return nbRow == 1;
        }
    }
}
