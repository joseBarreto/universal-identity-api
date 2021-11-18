using UniversalIdentity.Domain.Entities;

namespace UniversalIdentity.Domain.Interfaces
{
    public interface ILoginRepository : IBaseRepository<Login>
    {
        Login GetWithIncludesByEmailAndSenha(string email, string senha);

        Login GetWithIncludesByUsuarioId(int usuarioId);

        bool ExistsByEmail(string email);
    }
}
