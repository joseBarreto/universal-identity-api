using System.Collections.Generic;
using UniversalIdentity.Domain.Entities;

namespace UniversalIdentity.Domain.Interfaces
{
    public interface IPessoaService : IBaseService<Pessoa>
    {
        IList<Pessoa> GetByTermWithIncludes(string term, int excludePessoaId, int pageNumber, int pageSize, out int totalRecords);

    }
}
