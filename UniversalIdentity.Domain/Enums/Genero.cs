using System.ComponentModel;

namespace UniversalIdentity.Domain.Enums
{
    public enum Genero
    {
        [Description("Não definido")]
        NaoDefinido = 0,

        [Description("Feminino")]
        Feminino = 1,

        [Description("Masculino")]
        Masculino = 2
    }
}
