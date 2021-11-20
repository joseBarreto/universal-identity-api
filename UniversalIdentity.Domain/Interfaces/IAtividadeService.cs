using System.Collections.Generic;
using UniversalIdentity.Domain.Entities;

namespace UniversalIdentity.Domain.Interfaces
{
    public interface IAtividadeService : IBaseService<Atividade>
    {
        IList<Atividade> GetByPessoaIdWithIncludes(int pessoaId, int pageNumber, int pageSize, out int totalRecords);

    }
}
