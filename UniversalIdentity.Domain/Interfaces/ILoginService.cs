using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Models;

namespace UniversalIdentity.Domain.Interfaces
{
    public interface ILoginService : IBaseService<Login>
    {
        Login GetWithIncludesByDocumentoNumeroAndSenha(string documentoNumero, string senha);

        Login GetWithIncludesByUsuarioId(int usuarioId);

        bool ExistsByEmail(string email);

        TokenResponseModel GerarTokenJwt(Login login);
    }
}
