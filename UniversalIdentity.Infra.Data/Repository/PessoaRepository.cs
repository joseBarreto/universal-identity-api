using System;
using System.Collections.Generic;
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

        public IList<Pessoa> GetByTermWithIncludes(string term, int excludePessoaId, int pageNumber, int pageSize, out int totalRecords)
        {
            totalRecords = _context.Set<Pessoa>().Count();
            term = term?.ToLowerInvariant();

            var pessoas = _context.Pessoa
                                    .Where(x =>
                                                x.Id != excludePessoaId &&
                                                (
                                                    string.IsNullOrEmpty(term) ||
                                                    (
                                                        x.Nome.ToLower().Contains(term) ||
                                                        x.DocumentoNumero.ToLower().Contains(term) ||
                                                        x.UniversalId.ToLower().Contains(term)
                                                    )
                                                )
                                            )
                                    .OrderBy(x => x.Id)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();
            return pessoas;
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
