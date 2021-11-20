using UniversalIdentity.Domain.Entities;

namespace UniversalIdentity.Domain.Interfaces
{
    public interface IPessoaRepository : IBaseRepository<Pessoa>
    {
        void RefreshTotalAvaliacaoAndHorasTrabalahdas(int pessoaId);
    }
}
