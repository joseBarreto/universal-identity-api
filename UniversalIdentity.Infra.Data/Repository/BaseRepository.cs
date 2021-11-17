using System.Collections.Generic;
using System.Linq;
using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Interfaces;
using UniversalIdentity.Infra.Data.Context;

namespace UniversalIdentity.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly UniversalIdentityContext _context;

        public BaseRepository(UniversalIdentityContext context)
        {
            _context = context;
        }

        public void Insert(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Set<TEntity>().Remove(Select(id));
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.Set<TEntity>().Any(x => x.Id == id);
        }

        public IList<TEntity> Select(int pageNumber, int pageSize)
        {
            return _context.Set<TEntity>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        }

        public TEntity Select(int id) => _context.Set<TEntity>().Find(id);

        public int TotalRecords() => _context.Set<TEntity>().Count();
    }
}
