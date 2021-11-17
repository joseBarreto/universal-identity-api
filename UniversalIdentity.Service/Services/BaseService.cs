using System.Collections.Generic;
using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Interfaces;

namespace UniversalIdentity.Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public virtual TEntity Add(TEntity obj)
        {
            _baseRepository.Insert(obj);
            return obj;
        }

        public virtual void Delete(int id)
        {
            _baseRepository.Delete(id);
        }

        public virtual IList<TEntity> Get(int pageNumber, int pageSize, out int totalRecords)
        {
            totalRecords = _baseRepository.TotalRecords();
            return _baseRepository.Select(pageNumber, pageSize);
        }

        public virtual TEntity GetById(int id)
        {
            return _baseRepository.Select(id);
        }

        public virtual TEntity Update(TEntity obj)
        {
            _baseRepository.Update(obj);
            return obj;
        }

        public virtual bool Exists(int id)
        {
            return _baseRepository.Exists(id);
        }
    }
}
