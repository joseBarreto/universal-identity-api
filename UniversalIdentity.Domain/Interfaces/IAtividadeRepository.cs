using System.Collections.Generic;
using UniversalIdentity.Domain.Entities;

namespace UniversalIdentity.Domain.Interfaces
{
    public interface IAtividadeRepository : IBaseRepository<Atividade>
    {
        IList<Atividade> GetByPessoaIdWithIncludes(int pessoaId, int pageNumber, int pageSize, out int totalRecords);
    }
}
