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

        public Login GetWithIncludesByEmailAndSenha(string email, string senha) => _context.Login
                                                                      .Include(l => l.Pessoa)
                                                                      .FirstOrDefault(x => x.Email == email && x.Senha == senha);

        public Login GetWithIncludesByUsuarioId(int usuarioId) => _context.Login
                                                                      .Include(l => l.Pessoa)
                                                                      .FirstOrDefault(x => x.PessoaId == usuarioId);

        public bool ExistsByEmail(string email) => _context.Login.Any(x => x.Email.ToLower() == email.ToLower());
    }
}
