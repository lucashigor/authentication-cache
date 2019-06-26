using System;
using System.Collections.Generic;
using GranaPreta.Application.Interfaces;
using GranaPreta.Domain;

namespace GranaPreta.Application
{
    public class AppServiceBase<TEntity> : IDisposable, IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;

        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public void Add(TEntity obj)
        {
            _serviceBase.Add(obj);
        }

        public TEntity GetById(int id)
        {
            return _serviceBase.GetById(id);
        }

        public System.Collections.Generic.IEnumerable<TEntity> GetAll()
        {
            return _serviceBase.GetAll();
        }

        public void Update(TEntity obj)
        {
            _serviceBase.Update(obj);
        }

        public void Remove(int id)
        {
            _serviceBase.Remove(id);
        }

        public void Dispose()
        {
            _serviceBase.Dispose();
        }
    }
}
