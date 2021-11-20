using System.Collections.Generic;
using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Interfaces;

namespace UniversalIdentity.Service.Services
{
    public class AtividadeService : BaseService<Atividade>, IAtividadeService
    {
        private readonly IAtividadeRepository _atividadeRepository;
        private readonly IPessoaRepository _pessoaRepository;

        public AtividadeService(IAtividadeRepository atividadeRepository, IPessoaRepository pessoaRepository) : base(atividadeRepository)
        {
            _atividadeRepository = atividadeRepository;
            _pessoaRepository = pessoaRepository;
        }

        public IList<Atividade> GetByPessoaIdWithIncludes(int pessoaId, int pageNumber, int pageSize, out int totalRecords)
        {
            return _atividadeRepository.GetByPessoaIdWithIncludes(pessoaId, pageNumber, pageSize, out totalRecords);
        }

        public override Atividade Add(Atividade obj)
        {
            var newId = base.Add(obj);
            _pessoaRepository.RefreshTotalAvaliacaoAndHorasTrabalahdas(obj.PessoaId);
            return newId;
        }
    }
}
