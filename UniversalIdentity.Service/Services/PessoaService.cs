using System.Collections.Generic;
using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Interfaces;

namespace UniversalIdentity.Service.Services
{
    public class PessoaService : BaseService<Pessoa>, IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository) : base(pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public bool ExistsByDocumentoNumero(string documentoNumero)
        {
            return _pessoaRepository.ExistsByDocumentoNumero(documentoNumero);
        }

        public IList<Pessoa> GetByTermWithIncludes(string term, int excludePessoaId, int pageNumber, int pageSize, out int totalRecords)
        {
            return _pessoaRepository.GetByTermWithIncludes(term, excludePessoaId, pageNumber, pageSize, out totalRecords);

        }
    }
}
