using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Interfaces;
using UniversalIdentity.Infra.Data.Context;

namespace UniversalIdentity.Infra.Data.Repository
{
    public class AtividadeRepository : BaseRepository<Atividade>, IAtividadeRepository
    {
        public AtividadeRepository(UniversalIdentityContext context) : base(context)
        {
        }

        public IList<Atividade> GetByPessoaIdWithIncludes(int pessoaId, int pageNumber, int pageSize, out int totalRecords)
        {
            totalRecords = _context.Set<Atividade>().Count();

            var atividades = _context.Atividade
                                    .Where(x => x.PessoaId == pessoaId)
                                    .OrderBy(x => x.Id)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .Include(x => x.Autor)
                                    .ToList();
            return atividades;
        }
    }
}
