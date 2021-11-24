using Microsoft.EntityFrameworkCore;
using System.Linq;
using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Interfaces;
using UniversalIdentity.Infra.Data.Context;

namespace UniversalIdentity.Infra.Data.Repository
{
    public class LoginRepository : BaseRepository<Login>, ILoginRepository
    {
        public LoginRepository(UniversalIdentityContext context) : base(context)
        {
        }

        public Login GetWithIncludesByDocumentoNumeroAndSenha(string documentoNumero, string senha) => _context.Set<Login>()
                                                                      .Include(l => l.Pessoa)
                                                                      .FirstOrDefault(x => x.Pessoa.DocumentoNumero == documentoNumero && x.Senha == senha);

        public Login GetWithIncludesByUsuarioId(int usuarioId) => _context.Set<Login>()
                                                                      .Include(l => l.Pessoa)
                                                                      .FirstOrDefault(x => x.PessoaId == usuarioId);

        public bool ExistsByEmail(string email) => _context.Set<Login>().Any(x => !string.IsNullOrEmpty(x.Email) && x.Email.ToLower() == email.ToLower());
    }
}
