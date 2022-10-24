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
    public abstract class RepositoryBase<TId, TEntity> : IRepositoryBase<TId, TEntity>
    {
        protected readonly IDbConnection _connection;

        public string TableName { get; init; }
        public string TableId { get; init; }

        public RepositoryBase(IDbConnection connection, string tableName, string tableId)
        {
            _connection = connection;
            TableName = tableName;
            TableId = tableId;
        }


        protected abstract TEntity Mapper(IDataRecord record);

        public IEnumerable<TEntity> GetAll()
        {
            // Créer la commande SQL
            IDbCommand command = _connection.CreateCommand();

            // - Type de la commande
            command.CommandType = CommandType.Text;

            // - Contenu de la commande
            command.CommandText = $"SELECT * FROM [{TableName}]";

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

        public TEntity? GetById(TId id)
        {
            // Créer la commande SQL
            IDbCommand command = _connection.CreateCommand();

            // - Type de la commande
            command.CommandType = CommandType.Text;

            // - Contenu de la commande
            command.CommandText = $"SELECT * FROM [{TableName}] WHERE [{TableId}] = @Id";

            // - Ajout des parametres SQL
            command.CreateParameterWithValue("Id", id);


            // Ouverture de la connexion
            _connection.Open();

            // Execution de la commande SQL
            TEntity? entity = default(TEntity?);

            using (IDataReader reader = command.ExecuteReader())
            {
                // Lecture de la premiere Row 
                if (reader.Read())
                {
                    // Utilisation du mapper entre la DB et la DAL
                    entity = Mapper(reader);
                }

                // Nouvelle lecture (Normalement, il n'y a plus rien à lire)
                if(reader.Read())
                {
                    // Envoi d'une erreur s'il y plus de resultat ! 
                    throw new Exception("Return more of one row! (╯°□°）╯︵ ┻━┻");
                }
            }

            // Fermeture de la connexion
            _connection.Close();

            // Envoi des données
            return entity;
        }

        public abstract TId Add(TEntity entity);

        public abstract bool Update(TId id, TEntity entity);

        public bool Delete(TId id)
        {
            IDbCommand command = _connection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = $"DELETE FROM [{TableName}] WHERE [{TableId}] = @Id";

            command.CreateParameterWithValue("Id", id);

            _connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            _connection.Close();

            return rowAffected == 1;
        }
    }
}
