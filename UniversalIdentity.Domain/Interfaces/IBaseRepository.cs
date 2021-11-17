using System.Collections.Generic;
using UniversalIdentity.Domain.Entities;

namespace UniversalIdentity.Domain.Interfaces
{
    /// <summary>
    /// Base para entidades
    /// </summary>
    /// <typeparam name="TEntity">Entidade herdada de BaseEntity</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Insere um novo registro
        /// </summary>
        /// <param name="obj">obj para inserção</param>
        void Insert(TEntity obj);

        /// <summary>
        /// Atualiza um registro
        /// </summary>
        /// <param name="obj">obj para atualização</param>
        void Update(TEntity obj);

        /// <summary>
        /// Remove um registro
        /// </summary>
        /// <param name="id">identificador do registro</param>
        void Delete(int id);

        /// <summary>
        /// Procura um registro com base no Id
        /// </summary>
        /// <param name="id">identificador do registro</param>
        /// <returns></returns>
        bool Exists(int id);

        /// <summary>
        /// Procura uma lista de registros
        /// </summary>
        /// <param name="pageNumber">Pagina atual</param>
        /// <param name="pageSize">Total de itens por pagina</param>
        /// <returns>Lista dos registros</returns>
        IList<TEntity> Select(int pageNumber, int pageSize);

        /// <summary>
        /// Procura um registro
        /// </summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns></returns>
        TEntity Select(int id);

        /// <summary>
        /// Total de itens
        /// </summary>
        /// <returns></returns>
        int TotalRecords();
    }
}
