using System.Collections.Generic;
using UniversalIdentity.Domain.Entities;

namespace UniversalIdentity.Domain.Interfaces
{
    public interface IPessoaRepository : IBaseRepository<Pessoa>
    {
        void RefreshTotalAvaliacaoAndHorasTrabalahdas(int pessoaId);
        IList<Pessoa> GetByTermWithIncludes(string term, int excludePessoaId, int pageNumber, int pageSize, out int totalRecords);
        bool ExistsByDocumentoNumero(string documentoNumero);
    }
}
