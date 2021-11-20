using System;
using System.Linq;
using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Interfaces;
using UniversalIdentity.Infra.Data.Context;

namespace UniversalIdentity.Infra.Data.Repository
{
    public class PessoaRepository : BaseRepository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(UniversalIdentityContext context) : base(context)
        {
        }

        public void RefreshTotalAvaliacaoAndHorasTrabalahdas(int pessoaId)
        {
            var result = _context.Set<Atividade>()
                .Where(x => x.PessoaId == pessoaId)
                .Select(x => new
                {
                    x.Avaliacao,
                    x.HorasTrabalhadas
                }).ToList();

            var totalHorasTrabalhadas = result.Sum(x => x.HorasTrabalhadas);
            var totalAvaliacao = result.Sum(x => x.Avaliacao);
            var mediaAvaliacao = totalAvaliacao / result.Count;

            var pessoa = this.Select(pessoaId);
            pessoa.TotalAvaliacao = mediaAvaliacao;
            pessoa.TotalHorasTrabalhadas = totalHorasTrabalhadas;
            pessoa.DataAtualizacao = DateTime.Now;

            this.Update(pessoa);
        }


    }
}
