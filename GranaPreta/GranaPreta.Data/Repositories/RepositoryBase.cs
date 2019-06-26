using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dommel;
using GranaPreta.Domain;
using GranaPreta.Domain.Interfaces.Factory;

namespace GranaPreta.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly string ConnectionString;

        public RepositoryBase(IContextFactory factory)
        {
            ConnectionString = factory.GetContext();
        }

        public void Add(TEntity obj)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var entity = db.Insert(obj);
            }
        }

        public TEntity GetById(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Get<TEntity>(id);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.GetAll<TEntity>();
            }
        }

        public void Update(TEntity obj)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                db.Update(obj);
            }
        }

        public void Remove(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var entity = GetById(id);

                if (entity == null) throw new Exception("Registro não encontrado");

                db.Delete(entity);
            }
        }

        public void Dispose()
        {

        }
    }
}
