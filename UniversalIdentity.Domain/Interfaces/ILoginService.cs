using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Models;

namespace UniversalIdentity.Domain.Interfaces
{
    public interface ILoginService : IBaseService<Login>
    {
        Login GetWithIncludesByEmailAndSenha(string email, string senha);

        Login GetWithIncludesByUsuarioId(int usuarioId);

        bool ExistsByEmail(string email);

        TokenResponseModel GerarTokenJwt(Login login);
    }
}
